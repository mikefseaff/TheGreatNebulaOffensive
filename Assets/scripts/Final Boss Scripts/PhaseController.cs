using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PhaseController : MonoBehaviour
{
    public static PhaseController SharedInstance;
    public int UniversalPhaseNumber;

    public delegate void StartPhase();
    public static event StartPhase Phase;

    private void Awake()
    {
        SharedInstance = this;
        UniversalPhaseNumber = 0;
    }

    void Start()
    {
        UniversalPhaseNumber += 1;
        Invoke("PhaseEvent", 2);
    }

    // Update is called once per frame
    public void PhaseEvent()
    {

        if (Phase != null)
            Phase();
    } 
}

//phases 1-4 arms
//phase 5 4 lasers on body 
//6 hanger
//7 core
