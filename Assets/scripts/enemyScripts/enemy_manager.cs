using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_manager : MonoBehaviour
{
    private float timer1;
    private float maxTimer1;
    private float timer2;
    private float maxTimer2;
    private float timer3;
    private float maxTimer3;
    public GameObject enemy1;
    public GameObject enemy2;
    public GameObject enemy3;
    private GameObject player;
    //public GameObject enemy3;

    public float timerMin1 = 5f;
    public float timerMax1 = 12f;
    public float timerMin2 = 5f;
    public float timerMax2 = 12f;
    public float timerMin3 = 5f;
    public float timerMax3 = 12f;

    // Start is called before the first frame update
    void Start()
    {
        timer1 = 0;
        timer2 = 0;
        timer3 = 0;
        maxTimer1 = Random.Range(timerMin1, timerMax1);
        maxTimer2 = Random.Range(timerMin2, timerMax2);
        maxTimer3 = Random.Range(timerMin3, timerMax3);
        StartCoroutine("SpawnEnemy1Timer");
        StartCoroutine("SpawnEnemy2Timer");
        StartCoroutine("SpawnEnemy3Timer");
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.GetComponent<SpriteRenderer>().enabled == false)
        {
            StopAllCoroutines();
        }
        if(player.GetComponent<Collider2D>().enabled == false)
        {
            StopAllCoroutines();
        }
    }

    void SpawnEnemy1()
    {
        GameObject tempEnemy;
        float x = 1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, Random.Range(.7f, 1f), 0));
        spawnPoint.z = 0;

        // adjust x-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, .7f, dist)).y;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist)).y;
        Vector3 enemySize = enemy1.GetComponent<Renderer>().bounds.size;
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, bottomBorder + enemySize.x / 2, topBorder - enemySize.x / 2);
        tempEnemy = GameObject.Instantiate(enemy1, spawnPoint, new Quaternion(0, 0, 0, 0));
        tempEnemy.GetComponent<enemy_controller1>().speed = -6;
    }
    void SpawnEnemy2()
    {
        GameObject tempEnemy;
        float x = 1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, Random.Range(.3f, .7f), 0));
        spawnPoint.z = 0;

        // adjust x-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, .3f, dist)).y;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, .7f, dist)).y;
        Vector3 enemySize = enemy2.GetComponent<Renderer>().bounds.size;
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, bottomBorder + enemySize.x / 2, topBorder - enemySize.x / 2);
        tempEnemy = GameObject.Instantiate(enemy2, spawnPoint, new Quaternion(0, 0, 0, 0));
        tempEnemy.GetComponent<enemy_controller2>().frequency = Random.Range(5, 11);
        tempEnemy.GetComponent<enemy_controller2>().magnitude = Random.Range(.5f,1f);

    }
    void SpawnEnemy3()
    {
        float x = 1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, 0, 0));
        spawnPoint.z = 0;
        spawnPoint.y = 0;
        GameObject tmp = GameObject.Instantiate(enemy3, spawnPoint, new Quaternion(0, 0, 0, 0));
        tmp.GetComponent<enemy_controller3>().moveLocation = .9f;
    }
    

    IEnumerator SpawnEnemy1Timer()
    {
        while (true)
        {
            if (timer1 >= maxTimer1)
            {
                //spawn an enemy
                SpawnEnemy1();
                timer1 = 0;
                maxTimer1 = Random.Range(timerMin1, timerMax1);
            }

            timer1 += .01f;
            yield return new WaitForSeconds(.01f);
        }
    }
    IEnumerator SpawnEnemy2Timer()
    {
        while (true)
        {
            if (timer2 >= maxTimer2)
            {
                //spawn an enemy
                SpawnEnemy2();
                timer2 = 0;
                maxTimer2 = Random.Range(timerMin2, timerMax2);
            }

            timer2 += .01f;
            yield return new WaitForSeconds(.01f);
        }
    }
    IEnumerator SpawnEnemy3Timer()
    {
        while (true)
        {
            if (timer3 >= maxTimer3)
            {
                //spawn an enemy
                SpawnEnemy3();
                timer3 = 0;
                maxTimer3 = Random.Range(timerMin3, timerMax3);
                break;
            }

            timer3 += 1f;
            yield return new WaitForSeconds(1f);
        }
    }
}
