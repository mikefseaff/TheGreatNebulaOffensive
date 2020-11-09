using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SunManager : MonoBehaviour
{
    public float moveSpeed;
    public bool canMove;
    public bool checkPos;
    private float startPos;
    private float finalPos;
    Vector3 pos;
    // Start is called before the first frame update
    void Start()
    {
        checkPos = false;
        canMove = false;
        pos = transform.position;
        
    }

    // Update is called once per frame
    void Update()
    {
        movementLimit();
        if (canMove)
        {
            StartCoroutine("Move");


        }
    }

    void movementLimit()
    {

        if (checkPos)
        {
            startPos = transform.position.x;
            finalPos = startPos - .2f;
            checkPos = false;
            canMove = true;
        }
        else if (!checkPos && canMove)
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

        
        pos -= transform.right * Time.deltaTime * moveSpeed;
        transform.position = pos;
        yield return new WaitForSeconds(Time.deltaTime);



        if (!canMove)
        {
            yield break;
        }


    }
}
