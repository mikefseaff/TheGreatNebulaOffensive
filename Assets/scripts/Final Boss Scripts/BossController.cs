using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossController : MonoBehaviour
{
    public int phaseNumber;
    private Vector2 startingPos;

    public delegate void StartShooting();
    public static event StartShooting Shoot;
    //need to subcribe to generator event that will be called when the generator is destroyed which will call the move out coroutine which will then call the next phase
    void Start()
    {
        startingPos = this.transform.position;
        phaseNumber = 1;
    }

    private void OnEnable()
    {
        PhaseController.Phase += determineRotation;
        SheildGeneratorController.Destroyed += EndPhaseMovement;
    }

    private void OnDisable()
    {
        PhaseController.Phase -= determineRotation;
        SheildGeneratorController.Destroyed -= EndPhaseMovement;
    }

    public void ShootingEvent()
    {

        if (Shoot != null)
            Shoot();
    }

    // Update is called once per frame
    private void rotateShip(int degrees,float x, float y)
    {
        transform.Rotate(new Vector3(0, 0, degrees));
        transform.position = new Vector2(startingPos.x, y);
        StartCoroutine(MoveIn(degrees,x));
    }

    private void determineRotation()
    {
        switch (PhaseController.SharedInstance.UniversalPhaseNumber)
        {
            case 1:
                rotateShip(45, 22.13f, 3.68f);
                break;
            case 2:
                rotateShip(105, 19.54f, -6.67f);
                break;
            case 3:
                rotateShip(-105, 19.54f, 6.67f);
                break;
            case 4:
                rotateShip(-45, 22f, -3.68f);
                break;
            default:
                rotateShip(0, startingPos.x, startingPos.y);
                break;
        }
    }

    private void EndPhaseMovement()
    {
        switch (PhaseController.SharedInstance.UniversalPhaseNumber)
        {
            case 1:
                StartCoroutine(MoveOut(45));
                break;
            case 2:
                StartCoroutine(MoveOut(105));
                break;
            case 3:
                StartCoroutine(MoveOut(-105));
                break;
            case 4:
                StartCoroutine(MoveOut(-45));
                break;
            default:
                rotateShip(0, startingPos.x, startingPos.y);
                break;
        }
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
        yield return new WaitForSeconds(.5f);
        ShootingEvent();
        
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
        PhaseController.SharedInstance.UniversalPhaseNumber++;
        PhaseController.SharedInstance.PhaseEvent();
      
    }

    //phase one Rotate 45 degress x = 22.13 y = 3.68
    //phase two rotate 105 x = 19.54 y = -6.67
    //phase three rotate -105 x = 19.54 y = 6.67
    //phase four rotate -45 x = 22   y = -3.68


    //send out event that if its the correct phase will take all the hidden objects on an arm out. when the generator is destroyed 
    //incriment phase number on shared instance object which will auto call the next phase
}
