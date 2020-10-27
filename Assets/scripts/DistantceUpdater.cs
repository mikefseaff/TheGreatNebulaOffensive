using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DistantceUpdater : MonoBehaviour
{
    public Text textbox;
    public float time;
    // Start is called before the first frame update
    void Start()
    {
        textbox = textbox.GetComponent<Text>();
        StartCoroutine(DistanceUpdate());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    IEnumerator DistanceUpdate()
    {
        while (true)
        { 
            if (time > 0)
            {
                
                textbox.text = "Distance to Mother Ship : " + time*100000 + " miles";
            }
            if (time <= 0)
            {
                textbox.text = "Distance to Mother Ship : 0 Light Years";
                StopCoroutine(DistanceUpdate());
            }
            time -= .01f;
            yield return new WaitForSeconds(.01f);
        }
        
    }
}
