using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller3 : MonoBehaviour
{
    public float speedX;
    public float speedY;
    public float paceSpeed;
    private Rigidbody2D rb;
    private Vector3 pos;
    private float timerBullet;
    private float maxTimerBullet;
    public GameObject bullet;

    public float timerMin = 5f;
    public float timerMax = 25f;
    public bool canfireBullets = true;

    private bool goingUp = true;


    private Vector3 pos1 = new Vector3(7, -7, 0);
    private Vector3 pos2 = new Vector3(7, 7, 0);
    public float speed = 1f;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speedX, 0);
        pos = Camera.main.WorldToViewportPoint(transform.position);
        Debug.Log(pos);
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
            transform.position = Vector3.Lerp(pos1, pos2, Mathf.PingPong(Time.time * speed, 1f));
            speedX = 0;
        //    rb.velocity = new Vector2(speedX, speedY);
        //    pos.z = 0;
        //    IsGoingUp();
        //    if (goingUp)
        //    {
        //        Debug.Log("true");
        //        MoveUp();
        //    }
        //    else if (goingUp == false)
        //    {
        //        Debug.Log("false");
        //        MoveDown();
        //    }
        }
        
    }

    //void IsGoingUp()
    //{
    //    if (Camera.main.WorldToViewportPoint(transform.position).y >= .7)
    //    {
    //        goingUp = false;
    //    }
    //    else if (Camera.main.WorldToViewportPoint(transform.position).y <= -.7)
    //    {
    //        goingUp = true; 
    //    }
    //}
    //void MoveUp()
    //{
    //    pos += .1f * transform.up;
    //    Debug.Log(pos);
    //    transform.position = pos;

    //}


    //void MoveDown()
    //{
    //    pos -= .1f * transform.up;
    //    Debug.Log(pos);
    //    transform.position = pos;

    //}


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
