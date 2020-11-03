using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoonManager : MonoBehaviour
{
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
    // Start is called before the first frame update
    void Start()
    {
        canMove = false;
        pos = transform.position;
        

        localScale = transform.localScale;
        Debug.Log(transform.localPosition);
        //StartCoroutine("move");
    }

    // Update is called once per frame
    void Update()
    {
        if(canMove)
        {
            StartCoroutine("Move");
            //Time.time *
            //pos -= transform.right * Time.deltaTime * moveSpeed;
            //transform.position = pos + transform.up * Mathf.Sin(Time.time * frequency) * magnitude;

            //Debug.Log(Time.time + "real");
            
        }
        if (!canMove)
        {
           StopCoroutine("Move");
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
    IEnumerator Move()
    {
      
        
        localTime += Time.deltaTime;
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos + transform.up * Mathf.Sin(localTime * frequency) * magnitude;
        //Debug.Log(localTime);
        yield return new WaitForSeconds(Time.deltaTime);
        
        
        if (!canMove)
        {
            yield break;
        }

        
    }

}
