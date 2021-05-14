using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCannonRotater : MonoBehaviour
{
    private Rigidbody2D rb;
    private int wayToRotate;
    public int phase;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        

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
        if (phase == PhaseController.SharedInstance.UniversalPhaseNumber)
        {
            if (this.transform.position.y < 0)
            {
                wayToRotate = -1;
            }
            else
            {
                wayToRotate = 1;
            }
            StartCoroutine("Sweep180");
        }
        else
        {
            StopCoroutine("Sweep180");
        }

    }

    IEnumerator Sweep180()
    {
        while (true)
        {
     
            transform.Rotate(0, 0, .6f*wayToRotate);
            //Debug.Log(transform.rotation.z);
            
            
            if (this.transform.rotation.z >= .7 || this.transform.rotation.z <= -.7)
            {
         
                wayToRotate *= -1;
            }

            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
