using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerController : MonoBehaviour
{
    public GameObject spawnPoint;
    void Start()
    {
        gameObject.GetComponent<Animator>().enabled = false;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnEnable()
    {

        BossController.Shoot += IsPhase;
        
    }

    private void OnDisable()
    {

        BossController.Shoot -= IsPhase;
        
    }


    public void IsPhase()
    {
        if (PhaseController.SharedInstance.UniversalPhaseNumber == 5)
        {
            InvokeRepeating("SpawnEnemy", 1, 2f);
        }
        else if(PhaseController.SharedInstance.UniversalPhaseNumber == 6)
        {
            CancelInvoke();
            gameObject.GetComponent<Animator>().enabled = true;
            InvokeRepeating("SpawnEnemy", 1, .7f);
        }
      

    }

    private void SpawnEnemy()
    {
        GameObject enemy = Enemy2Pool.SharedInstance.GetPooledEnemy();
        if (enemy != null)
        {
            Vector3 spawnPointTmp = new Vector3(spawnPoint.transform.position.x + Random.Range(-2,2), spawnPoint.transform.position.y + Random.Range(-4.5f,4.5f), spawnPoint.transform.position.z);
            enemy.transform.position = new Vector3(spawnPointTmp.x, spawnPointTmp.y, spawnPointTmp.z);
            enemy.SetActive(true);
            enemy.GetComponent<Rigidbody2D>().velocity = new Vector2(enemy.GetComponent<enemy_controller2>().moveSpeed*-1,0);
            enemy.GetComponent<enemy_controller2>().pos = spawnPointTmp;


        }
    }
}
