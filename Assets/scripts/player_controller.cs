using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_controller : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    public GameObject bullet;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        BoundMovement();

        Shoot();

    }

    void Shoot()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            GameObject.Instantiate(bullet, transform.position, transform.rotation);
        }
    }

    void Move()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");

        float moveX = x * speed;
        float moveY = y * speed;

        rb.velocity = new Vector2(moveX, moveY);
    }

    void BoundMovement()
    {
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float leftBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).x;
        float rightBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 0, dist)).x;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 0, dist)).y;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(0, 1, dist)).y;

        Vector3 playerSize = GetComponent<Renderer>().bounds.size;

        this.transform.position = new Vector3(
            Mathf.Clamp(this.transform.position.x, leftBorder + playerSize.x / 2, rightBorder - playerSize.x / 2),
            Mathf.Clamp(this.transform.position.y, topBorder + playerSize.y / 2, bottomBorder - playerSize.y / 2),
            this.transform.position.z
        );


    }
    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "Enemy1" || collision.gameObject.tag == "Enemy2")
        {
            GameObject.Destroy(this.gameObject);
            GameObject.Destroy(collision.gameObject);
            //scoreText.GetComponent<score_controller>().score += 10;
            //scoreText.GetComponent<score_controller>().UpdateScore();
        }
        if (collision.gameObject.tag == "Enemy3")
        {

            Destroy(this.gameObject);
        }


    }
}
