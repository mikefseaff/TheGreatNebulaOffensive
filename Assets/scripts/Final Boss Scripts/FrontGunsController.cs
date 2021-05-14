using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FrontGunsController : SheildGeneratorController
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        //Debug.Log(transform.childCount);
        if (transform.childCount == 1)
        {
            PhaseController.SharedInstance.UniversalPhaseNumber += 1;
            Debug.Log(transform.childCount);
            Destroy(this);
        }
    }
}
