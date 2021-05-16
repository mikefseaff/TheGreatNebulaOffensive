using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCannonController : MonoBehaviour
{
    public GameObject bullet;
    public GameObject target;
    public GameObject spawnPoint;
    public int phase;
    public int health;
    private float timer;
    public GameObject explosion;
    private bool canShoot = true;

    private void OnEnable()
    {
        
        BossController.Shoot += IsPhase;
        BossController.LeftOver += IsLeftOver;
        StartCoroutine(Die(this.gameObject));
    }

    private void OnDisable()
    {
        
        BossController.Shoot -= IsPhase;
        BossController.LeftOver -= IsLeftOver;
    }

    public void IsPhase()
    {
        if (phase == PhaseController.SharedInstance.UniversalPhaseNumber)
        {
            InvokeRepeating("SpawnBullet", 0, .3f);
        }
        else
        {
            CancelInvoke();
        }
       
    }

    public void IsLeftOver()
    {
        if(phase == PhaseController.SharedInstance.UniversalPhaseNumber)
        {
            canShoot = false;  
            health = 0;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            foreach (Collider2D c in this.gameObject.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(this.transform.root.transform.localScale.x, this.transform.root.transform.localScale.y);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
        }
    }

    void SpawnBullet()
    {
        if (transform.parent.gameObject.activeInHierarchy && canShoot)
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
            GameObject player = GameObject.FindGameObjectWithTag("Player");
            player.GetComponent<player_controller>().currentSpecialCharge += 5;
            canShoot = false;
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            foreach (Collider2D c in this.gameObject.GetComponents<Collider2D>())
            {
                c.enabled = false;
            }
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(this.transform.root.transform.localScale.x, this.transform.root.transform.localScale.y);
            float animationTime = boom.GetComponent<Animator>().runtimeAnimatorController.animationClips[0].length;
            Destroy(boom.gameObject, animationTime);
            Destroy(this.transform.parent.gameObject);

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
