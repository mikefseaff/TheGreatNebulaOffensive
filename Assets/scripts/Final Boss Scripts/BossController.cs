using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int phaseNumber;
    private Vector2 startingPos;

    void Start()
    {
        startingPos = this.transform.position;
        rotateShip(45, 22.13f, 3.68f);
    }

    // Update is called once per frame
    private void rotateShip(int degrees,float x, float y)
    {
        transform.Rotate(new Vector3(0, 0, degrees));
        transform.position = new Vector2(startingPos.x, y);
        StartCoroutine(MoveIn(degrees,x));
    }

    IEnumerator MoveIn(int degrees,float xPos)
    {
        
        while (true)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(-3,0);

            if (this.transform.position.x <= xPos)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(2f);
        StartCoroutine(MoveOut(degrees));
    }

    IEnumerator MoveOut(int degrees)
    {

        while (true)
        {
            this.GetComponent<Rigidbody2D>().velocity = new Vector2(3, 0);

            if (this.transform.position.x >= startingPos.x)
            {
                this.GetComponent<Rigidbody2D>().velocity = new Vector2(0, 0);
                transform.Rotate(new Vector3(0, 0, degrees*-1));
                break;
            }


            yield return new WaitForSeconds(0.01f);
        }
        yield return new WaitForSeconds(1f);
        rotateShip(105, 19.54f, -6.67f);
    }

    //phase one Rotate 45 degress x = 22.13 y = 3.68
    //phase two rotate 105 x = 19.54 y = -6.67
    //phase three rotate -105 x = 19.54 y = 6.67
    //phase four rotate -45 x = 22   y = -3.68
}
