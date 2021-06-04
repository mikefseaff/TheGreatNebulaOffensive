using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangerEnemySpawnPoint : MonoBehaviour
{
    public GameObject player;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        this.transform.position = new Vector3(this.transform.position.x, player.transform.position.y, this.transform.position.z);
    }
}
