using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet_controller : MonoBehaviour
{
    private float timer = 0;
    public float speed;
    private Rigidbody2D rb;
    public GameObject explosion;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x > 1)
        {
            Destroy(this.gameObject);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        if (collision.gameObject.layer == 10)
        {
            GameObject.Destroy(this.gameObject);
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
            
            Destroy(this.gameObject);
        }


    }
   
}
