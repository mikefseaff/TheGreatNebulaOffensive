using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_bullet_controller : MonoBehaviour
{
    private float timer;
    public float speedX;
    public float speedY;
    private Rigidbody2D rb;
    public GameObject explosion;
    private GameObject player;
    // Start is called before the first frame update
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speedX, speedY);
        StartCoroutine(Die(player.gameObject));
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).y < 0 || Camera.main.WorldToViewportPoint(transform.position).x < 0 || Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            gameObject.SetActive(false);
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
