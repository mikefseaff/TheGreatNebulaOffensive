/*
*   Name: Michael Efseaff
*   ID: 2343166
*   Email: Efseaff@chapman.edu
*   Class: CPSC245-01
*   Final proj
*   This is my own work. I did not cheat on this assignment
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class background_controller : MonoBehaviour
{
    //speed the background scrolls
    public float ScrollSpeed;
    //renderer components
    private Renderer renderer;
    //amount to offset
    private Vector2 savedOffset;

    // Start is called before the first frame update
    void Start()
    {
        renderer = GetComponent<Renderer>();
    }

    // Update is called once per frame
    void Update()
    {
        //scrolls the background continuously
        float x = Mathf.Repeat(Time.time * ScrollSpeed, 1);
        Vector2 offset = new Vector2(x, 0);
        renderer.sharedMaterial.SetTextureOffset("_MainTex", offset);
    }
}
