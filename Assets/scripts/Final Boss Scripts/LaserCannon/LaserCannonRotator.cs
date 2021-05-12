using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserCannonRotator : MonoBehaviour
{
    private Rigidbody2D rb;
    public GameObject rotatePoint;
    public int wayToRotate;
    public bool readyToFire;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        AttackOne();
        if (this.transform.position.y < 0)
        {
            wayToRotate = -1;
        }
        else
        {
            wayToRotate = 1;
        }
    }

    public void AttackOne()
    {
        StartCoroutine(AttackSetUp(rotatePoint));
    }

    IEnumerator AttackSetUp(GameObject point)
    {
        Vector3 relativePos = point.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        //transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        
        while (true)
        {
            transform.Rotate(0, 0, rotation.z*-1);
            
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

    IEnumerator AttackOneSweep()
    {
        
        while (true)
        {

            transform.Rotate(0, 0, -.6f * wayToRotate);
            //Debug.Log(transform.rotation.z);


            if (this.transform.rotation.z >= .7 || this.transform.rotation.z <= -.7)
            {

                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
    }
}
