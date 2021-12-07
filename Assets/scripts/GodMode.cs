using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodMode : MonoBehaviour
{
    public GameObject player;
    public Text enabledText;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void setGodMode()
    {
        if(player.GetComponent<CircleCollider2D>().isTrigger == false)
        {
            player.GetComponent<CircleCollider2D>().isTrigger = true;
            enabledText.text = "ON";
        }
        else if (player.GetComponent<CircleCollider2D>().isTrigger == true)
        {
            player.GetComponent<CircleCollider2D>().isTrigger = false;
            enabledText.text = "OFF";
        }
        
    }
}
