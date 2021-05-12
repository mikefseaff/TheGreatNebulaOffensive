using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCannonController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public GameObject spawnPoint;
    private bool canShoot;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(this.GetComponent<SpriteRenderer>().sprite.name == "sniper cannon0047" && canShoot)
        {
            SpawnBullet();
            canShoot = false;
        }
        if(this.GetComponent<SpriteRenderer>().sprite.name != "sniper cannon0047")
        {
            canShoot = true;
        }
    }

    void SpawnBullet()
    {
        GameObject bullet = SniperCannonBulletPool.SharedInstance.GetPooledBullet();
        if (bullet != null)
        {
            Vector3 spawnPointTmp = spawnPoint.transform.position;
            bullet.transform.position = spawnPointTmp;
            bullet.SetActive(true);
            bullet.GetComponent<enemy_bullet_controller>().targetToMoveTo = player.transform.position;
            bullet.GetComponent<enemy_bullet_controller>().moveToTarget = true;
            bullet.GetComponent<enemy_bullet_controller>().isSniperBullet = true;
            Vector3 relativePos = player.transform.position - transform.position;
           if(transform.parent.gameObject.transform.rotation.z > 0)
            {
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.right);
                bullet.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            }
            else
            {
                Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.left);
                bullet.transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
            }
   
            
            


        }

    }
}
