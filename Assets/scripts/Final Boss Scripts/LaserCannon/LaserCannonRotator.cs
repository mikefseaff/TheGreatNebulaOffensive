using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannonRotator : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject rotatePoint;
    public int wayToRotate;
    public bool readyToAttack;
    public int phase;
    public int health;
    private float timer;
    public GameObject explosion;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        StartCoroutine(Die(this.gameObject));

    }

    private void OnEnable()
    {
        LaserGunManager.Attack += BeginAttack;
        BossController.Shoot += IsPhase;
    }

    private void OnDisable()
    {
        LaserGunManager.Attack -= BeginAttack;
        BossController.Shoot -= IsPhase;
    }

    public void AttackOne()
    {
        StartCoroutine(AttackSetUp(rotatePoint));
    }

    public void AttackTwo()
    {
        StartCoroutine("AttackTwoSetUp");
    }

    public void IsPhase()
    {
        if(phase == PhaseController.SharedInstance.UniversalPhaseNumber)
        {
            if (this.transform.position.y < 0)
            {
                wayToRotate = -1;
            }
            else
            {
                wayToRotate = 1;
            }
            readyToAttack = true;
        }
    }

    public void BeginAttack()
    {
        if (readyToAttack)
        {
            if (LaserGunManager.SharedInstance.AttackNum == 1)
            {
                AttackOne();
            }
            if (LaserGunManager.SharedInstance.AttackNum == 2)
            {
                AttackTwo();
            }
        }
        
    }

    IEnumerator AttackSetUp(GameObject point)
    {
        Vector3 relativePos = point.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        
        while (true)
        {
            transform.Rotate(0, 0, rotation.z*-3);
            
            if(transform.rotation.z <= rotation.w || (transform.rotation.z >= (rotation.w*-1) && wayToRotate == 1))
            {
               
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
        //add invoke delay here for beam charge up
        yield return new WaitForSeconds(2f);
        StartCoroutine("AttackOneSweep");

    }

    IEnumerator AttackTwoSetUp()
    {
        while (true)
        {

            transform.Rotate(0, 0, -.3f * wayToRotate);
            //Debug.Log(transform.rotation.z);


            if (this.transform.rotation.z >= .7 || this.transform.rotation.z <= -.7)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90f * wayToRotate);
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(AttackTwoSweep(rotatePoint));
    }

    IEnumerator AttackOneSweep()
    {
        
        while (true)
        {

            transform.Rotate(0, 0, -.6f * wayToRotate);
            //Debug.Log(transform.rotation.z);


            if (this.transform.rotation.z >= .7 || this.transform.rotation.z <= -.7)
            {
                transform.rotation = Quaternion.Euler(0, 0, -90f*wayToRotate);
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        StartCoroutine("ResetRotationOne");
    }


    IEnumerator AttackTwoSweep(GameObject point)
    {
        Vector3 tmpPoint = new Vector3(point.transform.position.x + 5.5f, point.transform.position.y, point.transform.position.z);
        Vector3 relativePos = tmpPoint - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);

        while (true)
        {
            transform.Rotate(0, 0, rotation.z * -8);

            if ((transform.rotation.z <= rotation.w && wayToRotate == -1) || (transform.rotation.z >= (rotation.w*-1) && wayToRotate == 1))
            {

                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
        //add invoke delay here for beam charge up
        yield return new WaitForSeconds(2f);
        StartCoroutine("ResetRotationTwo");
    }

    IEnumerator ResetRotationOne()
    {

        while (true)
        {

            transform.Rotate(0, 0, .6f*wayToRotate);
            //Debug.Log(transform.rotation.z);

            Debug.Log(transform.rotation.z);
            if ((this.transform.rotation.z <= 0 && wayToRotate == -1) || (transform.rotation.z >= 0 && wayToRotate == 1))
            {
                
                //transform.rotation = Quaternion.Euler(0, 0, -90f * wayToRotate);
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
    }

    IEnumerator ResetRotationTwo()
    {

        while (true)
        {

            transform.Rotate(0, 0, -.6f * wayToRotate);
            //Debug.Log(transform.rotation.z);

            Debug.Log(transform.rotation.z);
            if ((this.transform.rotation.z >= 0 && wayToRotate == -1) || (transform.rotation.z <= 0 && wayToRotate == 1))
            {

                //transform.rotation = Quaternion.Euler(0, 0, -90f * wayToRotate);
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {

        if (collision.gameObject.tag == "bullet_player")
        {
            health -= 1;
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(.7f, .7f, .7f, 1);
            StartCoroutine("HitDelay");

        }
        if (health == 0)
        {
            this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().enabled = false;
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
        this.transform.GetChild(0).gameObject.GetComponent<SpriteRenderer>().color = new Color(1, 1, 1, 1);

    }
}
