using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCannonRotator : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    public int phase;
    public bool canRotate;

  
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
        canRotate = false;
       
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
        Debug.Log("work please");

        if(phase == PhaseController.SharedInstance.UniversalPhaseNumber)
        {
            canRotate = true;
        }
        else
        {
            canRotate = false;
        }
       
    }

    // Update is called once per frame
    void Update()
    {
        if (canRotate)
        {
            Vector3 relativePos = player.transform.position - transform.position;

            // the second argument, upwards, defaults to Vector3.up
            Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
            transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
        }
       
    }
}
