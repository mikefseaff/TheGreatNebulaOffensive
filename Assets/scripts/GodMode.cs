using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GodMode : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown("p"))
        {
            setGodMode();
        }
    }

    public void setGodMode()
    {
        if(player.GetComponent<CircleCollider2D>().isTrigger == false)
        {
            player.GetComponent<CircleCollider2D>().isTrigger = true;
            Debug.Log("GODMODE ON");
        }
        else if (player.GetComponent<CircleCollider2D>().isTrigger == true)
        {
            player.GetComponent<CircleCollider2D>().isTrigger = false;
            Debug.Log("GODMODE OFF");
        }
        
    }
}
