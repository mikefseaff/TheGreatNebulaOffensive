﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : MonoBehaviour
{
    public Animator anim;
    private int moonStageValue;

    public bool canMove;
    public Rigidbody2D rb;
    public float moveSpeed = 5f;

    //frequency of the enemies sin movement
    public float frequency = 2000f;


    // magnitude of the enemies sin movement
    public float magnitude = 5f;

    Vector3 pos, localScale;
    public float timer;
    private float localTime;
    public float zPos;
    public bool checkPos;
    private float startPos;
    private float finalPos;
    // Start is called before the first frame update
    void Start()
    {

        checkPos = false;
        canMove = false;
        pos = transform.position;
        zPos = transform.position.z;

        localScale = transform.localScale;
        anim.GetComponent<Animator>();
        anim.enabled = false;
        anim.SetInteger("moonStage", -1);
        //StartCoroutine("move");
    }

    // Update is called once per frame
    void Update()
    {
        movementLimit();
        if(canMove)
        {
            StartCoroutine("Move");

            
        }
        if (!canMove)
        {
           //StopCoroutine("Move");
        }


    }
    IEnumerator Rotate()
    {
        timer += .01f;
        Vector2 vel = rb.velocity;
        for (int i = 0; i < 180; i++)
        {
            transform.Rotate(0, 0, 1);
            rb.velocity = Quaternion.Euler(1, 1, i) * vel;
            yield return new WaitForSeconds(0.01f);
        }
    }

    void movementLimit()
    {
    
        if (checkPos)
        {
            startPos = transform.position.x;
            finalPos = startPos - 4.25f;
            checkPos = false;
            canMove = true;
            anim.enabled = true;
            moonStageValue++;
            anim.SetInteger("moonStage", moonStageValue);
        }
        else if(!checkPos && canMove)
        {
            if (transform.position.x <= finalPos)
            {
                canMove = false;
            }
            else
            {
                canMove = true;
            }
        }

    }
    IEnumerator Move()
    {
      
            localTime += Time.deltaTime;
            pos -= transform.right * Time.deltaTime * moveSpeed;
            transform.position = pos + transform.up * Mathf.Sin(localTime * frequency) * magnitude;
            yield return new WaitForSeconds(Time.deltaTime);

        
        
        if (!canMove)
        {
            yield break;
        }

        
    }

}
