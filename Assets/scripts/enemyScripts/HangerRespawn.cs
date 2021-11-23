using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class HangerRespawn : MonoBehaviour
{
    public GameObject SpawnPoint;

    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if(SceneManager.GetActiveScene().name == "level 3")
        {
            if (this.transform.position.x >= SpawnPoint.transform.position.x)
            {
                gameObject.SetActive(false);
            }
        }
        else
        {
            enabled = false;
        }
       
    }
}
