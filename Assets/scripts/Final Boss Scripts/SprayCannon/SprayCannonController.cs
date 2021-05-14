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

    private void OnEnable()
    {
        
        BossController.Shoot += IsPhase;
        StartCoroutine(Die(this.gameObject));
    }

    private void OnDisable()
    {
        
        BossController.Shoot -= IsPhase;
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

    void SpawnBullet()
    {
        if (transform.parent.gameObject.activeInHierarchy)
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
            this.gameObject.GetComponent<SpriteRenderer>().enabled = false;
            GameObject boom = GameObject.Instantiate(explosion, this.transform.position, new Quaternion(0, 0, 0, 0));
            boom.transform.localScale = new Vector3(this.transform.localScale.x * 4.5f, this.transform.localScale.y * 4.5f);
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
