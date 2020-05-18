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
    }

    // Update is called once per frame
    void Update()
    {
    }

    void SpawnEnemy1()
    {
        float x = 1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, Random.Range(0, 1f), 0));
        spawnPoint.z = 0;

        // adjust x-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, .75f, dist)).y;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, 1, dist)).y;
        Vector3 enemySize = enemy1.GetComponent<Renderer>().bounds.size;
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, bottomBorder + enemySize.x / 2, topBorder - enemySize.x / 2);
        GameObject.Instantiate(enemy1, spawnPoint, new Quaternion(0, 0, 0, 0));
    }
    void SpawnEnemy2()
    {
        float x = 1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, Random.Range(0, 1f), 0));
        spawnPoint.z = 0;

        // adjust x-axis position
        float dist = (this.transform.position - Camera.main.transform.position).z;
        float bottomBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, .2f, dist)).y;
        float topBorder = Camera.main.ViewportToWorldPoint(new Vector3(1, .7f, dist)).y;
        Vector3 enemySize = enemy2.GetComponent<Renderer>().bounds.size;
        spawnPoint.y = Mathf.Clamp(spawnPoint.y, bottomBorder + enemySize.x / 2, topBorder - enemySize.x / 2);
        GameObject.Instantiate(enemy2, spawnPoint, new Quaternion(0, 0, 0, 0));
    }
    void SpawnEnemy3()
    {
        float x = 1.25f;
        Vector3 spawnPoint = Camera.main.ViewportToWorldPoint(new Vector3(x, 0, 0));
        spawnPoint.z = 0;
        spawnPoint.y = 0;
        GameObject.Instantiate(enemy3, spawnPoint, new Quaternion(0, 0, 0, 0));
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

            timer1 += 1f;
            yield return new WaitForSeconds(1f);
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

            timer2 += 1f;
            yield return new WaitForSeconds(1f);
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
