using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannonRotator : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject rotatePoint;
    public int wayToRotate;
    public bool readyToAttack;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        if (this.transform.position.y < 0)
        {
            wayToRotate = -1;
        }
        else
        {
            wayToRotate = 1;
        }
    }

    private void OnEnable()
    {
        LaserGunManager.Attack += BeginAttack;
    }

    private void OnDisable()
    {
        LaserGunManager.Attack -= BeginAttack;
    }

    public void AttackOne()
    {
        StartCoroutine(AttackSetUp(rotatePoint));
    }

    public void AttackTwo()
    {
        StartCoroutine("AttackTwoSetUp");
    }

    public void BeginAttack()
    {
        if(LaserGunManager.SharedInstance.AttackNum == 1)
        {
            AttackOne();
        }
        if(LaserGunManager.SharedInstance.AttackNum == 2)
        {
            AttackTwo();
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
}
