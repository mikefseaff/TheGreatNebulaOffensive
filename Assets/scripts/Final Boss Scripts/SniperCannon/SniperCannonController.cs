using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCannonController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject player;
    public GameObject spawnPoint;
    private bool canShoot;
    public int phase;
    public int health;
    private float timer;
    public GameObject explosion;
    void Start()
    {
        
        gameObject.GetComponent<Animator>().enabled = false;
        StartCoroutine(Die(this.gameObject));
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

    private void OnEnable()
    {

        BossController.Shoot += IsPhase;
    }

    private void OnDisable()
    {

        BossController.Shoot -= IsPhase;
    }

    private void IsPhase()
    {
        if (phase == PhaseController.SharedInstance.UniversalPhaseNumber)
        {
           
            gameObject.GetComponent<Animator>().enabled = true;
            
        }
        else
        {
            gameObject.GetComponent<Animator>().enabled = false;
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

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bullet_player")
        {
            health -= 1;
            this.gameObject.GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f, 1);
            StartCoroutine("HitDelay");
            
        }
        if (health == 0)
        {
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(this.transform.localScale.x*4.5f, this.transform.localScale.y*4.5f);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
            
        }

    }

    IEnumerator Die(GameObject Enemy)
    {
        while (true)
        {

            if (Enemy.gameObject.GetComponent<SpriteRenderer>().enabled == false)
            {
                Debug.Log("hi");
                if (timer >= .8f)
                {
                    Debug.Log("bye");
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
