using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UITypeWriter : MonoBehaviour
{
    Text txt;
    string story;
    //public AudioSource typeAudio;
    void Start()
    {
        txt = GetComponent<Text>();
        story = txt.text;
        txt.text = "";

        // TODO: add optional delay when to start
        StartCoroutine("PlayText");
        
    }

    IEnumerator PlayText()
    {
        foreach (char c in story)
        {
            txt.text += c;
            //typeAudio.Play();
            yield return new WaitForSecondsRealtime(0.02f);
        }
    }
    
}
