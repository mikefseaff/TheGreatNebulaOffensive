using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerController : SheildGeneratorController
{
    public GameObject spawnPoint;
    private float timer;
    public GameObject Enemy3;
  
    void Start()
    {
        gameObject.GetComponent<Animator>().enabled = false;
        StartCoroutine(Die(this.gameObject));
        //Enemy3.SetActive(false);

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {

        BossController.Shoot += IsPhase;
        BossController.LeftOver += StopInvoke;
        
    }

    private void OnDisable()
    {
        

        BossController.Shoot -= IsPhase;
        BossController.LeftOver -= StopInvoke;
        

    }


    public void IsPhase()
    {
        if (PhaseController.SharedInstance.UniversalPhaseNumber == 5)
        {
            InvokeRepeating("SpawnEnemy", 1, 2f);
        }
        else if(PhaseController.SharedInstance.UniversalPhaseNumber == 6)
        {
            
            gameObject.GetComponent<Animator>().enabled = true;
            InvokeRepeating("SpawnEnemy", 1.75f, .2f);
            Vector3 tmpSpawnPoint = new Vector3(spawnPoint.transform.position.x,0f, spawnPoint.transform.position.z);
            tmpSpawnPoint.x += .65f;
            GameObject tmp = GameObject.Instantiate(Enemy3, tmpSpawnPoint, new Quaternion(0, 0, 0, 0));
            tmp.GetComponent<enemy_controller3>().moveLocation = .7f;
            tmp.GetComponent<enemy_controller3>().maxTimerDelay = 1;
            
        }
      

    }

    private void StopInvoke()
    {
        CancelInvoke();
    }

    private void SpawnEnemy()
    {
        GameObject enemy = Enemy2Pool.SharedInstance.GetPooledEnemy();
        if (enemy != null)
        {
            Vector3 spawnPointTmp = new Vector3(spawnPoint.transform.position.x + Random.Range(-2,2), spawnPoint.transform.position.y + Random.Range(-2f,2f), spawnPoint.transform.position.z);
            enemy.transform.position = new Vector3(spawnPointTmp.x, spawnPointTmp.y, spawnPointTmp.z);
            enemy.SetActive(true);
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.GetComponent<enemy_controller2>().moveSpeed*-1,0);
            enemy.GetComponent<enemy_controller2>().pos = spawnPointTmp;
            enemy.GetComponent<enemy_controller2>().timerMin = .75f;
            enemy.GetComponent<enemy_controller2>().timerMax = 1.5f;


        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bullet_player")
        {
            health -= 1;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f, 1);
            StartCoroutine("HitDelay");

        }
        if (health <= 0)
        {
           
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(this.transform.root.transform.localScale.x, this.transform.root.transform.localScale.y);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
            PhaseController.SharedInstance.UniversalPhaseNumber += 1;
            Debug.Log(PhaseController.SharedInstance.UniversalPhaseNumber);
            DestroyedEvent();
            //GameObject tmpEnemy3 = GameObject.FindGameObjectWithTag("Enemy3");
            //tmpEnemy3.GetComponent<enemy_controller3>().selfDestroy();
           
            Destroy(this.gameObject);

        }

    }

    IEnumerator Die(GameObject Enemy)
    {
        while (true)
        {

            if (Enemy.gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                
                if (timer >= .8f)
                {
                    
                    GameObject.Destroy(Enemy);
                }
                timer += .8f;
                yield return new WaitForSeconds(.8f);
            }
            yield return null;

        }



    }
    IEnumerator HitDelay()
    {
        yield return new WaitForSeconds(.2f);
        this.gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }


}
