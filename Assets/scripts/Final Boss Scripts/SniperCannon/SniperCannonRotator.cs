using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SniperCannonRotator : MonoBehaviour
{
    public GameObject player;
    private Rigidbody2D rb;
    void Start()
    {
        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Vector3 relativePos = player.transform.position - transform.position;

        // the second argument, upwards, defaults to Vector3.up
        Quaternion rotation = Quaternion.LookRotation(relativePos, Vector3.up);
        transform.rotation = new Quaternion(0, 0, rotation.z, rotation.w);
    }
}
