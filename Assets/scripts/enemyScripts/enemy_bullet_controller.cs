using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class enemy_bullet_controller : MonoBehaviour
{
    private float timer;
    public float speedX;
    public float speedY;
    private Rigidbody2D rb;
    public GameObject explosion;
    private GameObject player;
    public bool moveToTarget;
    public Vector2 targetToMoveTo;
    public bool isSniperBullet;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        if(SceneManager.GetActiveScene().buildIndex != 3)
        rb.velocity = new Vector2(speedX, speedY);
        StartCoroutine(Die(player.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0 || Camera.main.WorldToViewportPoint(transform.position).y > 1 || Camera.main.WorldToViewportPoint(transform.position).x < 0 || Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            gameObject.SetActive(false);
        }
        if (moveToTarget)
        {
            moveBullet();
        }
        if (this.transform.position.x <= targetToMoveTo.x && isSniperBullet)
        {
            sniperSpecialCase();
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player")
        {
            
            gameObject.SetActive(false);
            //collision.gameObject.SetActive(false);
            collision.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject boom = GameObject.Instantiate(explosion, collision.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(collision.transform.localScale.x, collision.transform.localScale.y);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);

            

        }
    }

    private void sniperSpecialCase()
    {
        gameObject.SetActive(false);
        //collision.gameObject.SetActive(false);
        GameObject boom = GameObject.Instantiate(explosion, transform.position, new Quaternion(0, 0, 0, 0));
        boom.transform.localScale = new Vector3(transform.localScale.x, transform.localScale.y);
        float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
        Destroy(boom.gameObject, animationTime);
    }

    public void moveBullet()
    {
        transform.position = Vector2.MoveTowards(transform.position, targetToMoveTo, (speedX*-1)*Time.deltaTime);
    }
    IEnumerator Die(GameObject player)
    {
        while (true)
        {
            
            if (player.gameObject.GetComponent<SpriteRenderer>().enabled == false){
                Debug.Log("hi");
                if (timer >= .8f)
                {
                    Debug.Log("bye");
                    GameObject.Destroy(player);
                }
                timer += .8f;
                yield return new WaitForSeconds(.8f);
            }
            yield return null;

        }
       

        
    }
}
