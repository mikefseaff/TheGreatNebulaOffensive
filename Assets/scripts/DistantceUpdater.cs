using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistantceUpdater : MonoBehaviour
{
    public Text textbox;
    public float time;
    public int controlTime;
    private int originalTime;
    public delegate void SpawnTime();
    public static event SpawnTime SpawnTimeReduction;
    // Start is called before the first frame update
    void Start()
    {
        textbox = textbox.GetComponent<Text>();
        controlTime = (int)time;
        originalTime = controlTime;
        StartCoroutine(DistanceUpdate());
        StartCoroutine(TimeControl());
        
    }


    // Update is called once per frame
    void Update()
    {
        
    }
    public void SpawnTimeEvent()
    {
        if (SpawnTimeReduction != null)
            SpawnTimeReduction();
    }
    IEnumerator DistanceUpdate()
    {
        while (true)
        { 
            if (time > 0)
            {
                
                textbox.text = "Distance to Nebula Exit : " + time*100000 + " Parsecs";
            }
            if (time <= 0)
            {
                textbox.text = "Distance to Nebula Exit : 0 Parsecs";
                StopCoroutine(DistanceUpdate());
                break;
            }
            time -= .01f;
            yield return new WaitForSeconds(.01f);
        }
        
    }
    IEnumerator TimeControl()
    {
        int timeQuarters = 1; 
        while (true)
        {
            if (controlTime == originalTime - (15*timeQuarters))
            {

                SpawnTimeEvent();
                Debug.Log("Reducing spawn time");
                timeQuarters += 1;
            }
            if (controlTime <= 0)
            {
                
                StopCoroutine(TimeControl());
                break;
            }
            controlTime -= 1;
            yield return new WaitForSeconds(1f);
        }

    }
}
