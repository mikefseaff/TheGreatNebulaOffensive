using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCannonController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject target;
    public GameObject spawnPoint;
    void Start()
    {
        InvokeRepeating("SpawnBullet", 0,.3f);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    void SpawnBullet()
    {
        GameObject bullet = SprayCannonBulletPool.SharedInstance.GetPooledBullet();
        if (bullet != null)
        {
            Vector3 spawnPointTmp = spawnPoint.transform.position;
            bullet.transform.position = spawnPointTmp;
            bullet.SetActive(true);
            bullet.GetComponent<enemy_bullet_controller>().targetToMoveTo = target.transform.position;
            bullet.GetComponent<enemy_bullet_controller>().moveToTarget = true;
           

        }

    }
}
