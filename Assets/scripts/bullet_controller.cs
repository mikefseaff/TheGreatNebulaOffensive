using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    private float timer = 0;
    public float speedX;
    public float speedY;
    public Rigidbody2D rb;
    public GameObject explosion;
    public static string collidedTag;
    private GameObject player;


    // Start is called before the first frame update
    void Start()
    {
        
        rb.velocity = new Vector2(speedX, speedY);
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
       
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1 || Camera.main.WorldToViewportPoint(transform.position).x < 0 || 
            Camera.main.WorldToViewportPoint(transform.position).y > 1 || Camera.main.WorldToViewportPoint(transform.position).y < 0)
        {
            gameObject.SetActive(false);

        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        int returnCount = 0;
        
        if (collision.gameObject.layer == 10)
        {


            collidedTag = collision.gameObject.tag;
            returnCount += 1;
            player.GetComponent<player_controller>().currentSpecialCharge += 1;

            
            Debug.Log(collidedTag);
            gameObject.SetActive(false);
            GameObject.Destroy(collision.gameObject);
            GameObject boom = GameObject.Instantiate(explosion, collision.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(collision.transform.localScale.x, collision.transform.localScale.y);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
           

;            //scoreText.GetComponent<score_controller>().score += 10;
            //scoreText.GetComponent<score_controller>().UpdateScore();
        }
        if (collision.gameObject.layer == 12)
        {

            gameObject.SetActive(false);
        }


    }
   
}
