using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller3 : MonoBehaviour
{
    public float speedX;
    public float speedY;
   
    private Rigidbody2D rb;
 
    private float timerBullet;
    private float maxTimerBullet;
    public GameObject bullet;

    public float timerMin = 5f;
    public float timerMax = 25f;
    public bool canfireBullets = true;

    private bool goingUp = true;


    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speedX, 0);
        timerBullet = 0;
        maxTimerBullet = Random.Range(timerMin, timerMax);

        if (canfireBullets)
        {
            StartCoroutine("FireBullet");
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x < 0)
        {
            Destroy(this.gameObject);
        }
        if (Camera.main.WorldToViewportPoint(transform.position).x <= .9)
        {
           
                rb.velocity = new Vector2(0, speedY);
                IsGoingUp();
                if (goingUp)
                {
                    Debug.Log("true");
                    MoveUp();
                }
                else if (goingUp == false)
                {
                    Debug.Log("false");
                    MoveDown();
                }
        }
        
    }

    void IsGoingUp()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).y >= .7 || Camera.main.WorldToViewportPoint(transform.position).y == 0)
        {
            goingUp = false;
        }
        else if (Camera.main.WorldToViewportPoint(transform.position).y <= .3)
        {
            goingUp = true; 
        }
    }
    void MoveUp()
    {
        rb.velocity = new Vector2(0, speedY);
    }


    void MoveDown()
    {
        rb.velocity = new Vector2(0, (speedY*-1));
 
    }


    void SpawnBullet()
    {
        Vector3 spawnPoint = transform.position;
        spawnPoint.y -= (bullet.GetComponent<Renderer>().bounds.size.y / 2) + (GetComponent<Renderer>().bounds.size.y / 2);
        GameObject.Instantiate(bullet, spawnPoint, transform.rotation);

    }

    IEnumerator FireBullet()
    {
        while (true)
        {
            if (timerBullet >= maxTimerBullet)
            {
                //spawn an enemy
                SpawnBullet();
                timerBullet = 0;
                maxTimerBullet = Random.Range(timerMin, timerMax);
            }

            timerBullet += .01f;
            yield return new WaitForSeconds(.01f);
        }

    }
}
