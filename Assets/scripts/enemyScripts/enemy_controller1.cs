using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_controller1 : MonoBehaviour
{
    public float speed;
    private Rigidbody2D rb;

    private float timerBullet;
    private float maxTimerBullet;
    public GameObject bullet;
    private GameObject player;

    public float timerMin = 5f;
    public float timerMax = 25f;
    public bool canfireBullets = true;
    public bool canMove = true;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        rb.velocity = new Vector2(speed, 0);

        timerBullet = 0;
        maxTimerBullet = Random.Range(timerMin, timerMax);

        if (canfireBullets)
        {
            StartCoroutine("FireBullet");
        }
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (Camera.main.WorldToViewportPoint(transform.position).x <= 0)
        {
            rb.velocity = new Vector2(-speed, 0);
        }
        if (Camera.main.WorldToViewportPoint(transform.position).x >= 1)
        {
            rb.velocity = new Vector2(speed, 0);
        }
        if (player.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            StopAllCoroutines();
        }
       
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
