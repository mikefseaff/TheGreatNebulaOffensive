using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_manager1 : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;
    public GameObject stars;
    public GameObject planet;
    public GameObject playerBullet;
    public float timer;
    public float transitionTime;

    public int waveOneCount;
    public int waveTwoCount;
    public int waveThreeCount;
    public int waveFourCount;

    private string tag1;
    private string tag2;
    private string tag3;
    private string tag4;

    private GameObject[] waveOne;
    private GameObject[] waveTwo;
    private GameObject[] waveThree;
    private GameObject[] waveFour;



    // Start is called before the first frame update
    void Start()
    {
        tag1 = "Wave1";
        tag2 = "Wave2";
        tag3 = "Wave3";
        tag4 = "Wave4";

        waveOne = GameObject.FindGameObjectsWithTag(tag1);
        waveTwo = GameObject.FindGameObjectsWithTag(tag2);
        waveThree = GameObject.FindGameObjectsWithTag(tag3);
        waveFour = GameObject.FindGameObjectsWithTag(tag4);

        //waveOneCount = waveOne.Length;
        // waveTwoCount = waveTwo.Length;
        //waveThreeCount = waveThree.Length;
        //waveFourCount = waveFour.Length;

        EnemyWaveFreeze(waveOne);
        EnemyWaveFreeze(waveTwo);
        EnemyWaveFreeze(waveThree);
        EnemyWaveFreeze(waveFour);

    }

    // Update is called once per frame
    void Update()
    {
        //add one to the end for the annimation couroutine pause

        //waveOneCount = GameObject.FindGameObjectsWithTag(tag1).Length;
        //waveTwoCount = GameObject.FindGameObjectsWithTag(tag2).Length;
        //waveThreeCount = GameObject.FindGameObjectsWithTag(tag3).Length;
        //waveFourCount = GameObject.FindGameObjectsWithTag(tag4).Length;
        waveOne = GameObject.FindGameObjectsWithTag(tag1);
        waveTwo = GameObject.FindGameObjectsWithTag(tag2);
        waveThree = GameObject.FindGameObjectsWithTag(tag3);
        waveFour = GameObject.FindGameObjectsWithTag(tag4);
        EnemyWaveStart(waveOne);
        if (EnemyWaveCountReduction(waveOne) && waveOneCount == 1)
        {

            //MAKE CORUTINE HERE MICHAEL!
            //yeild WaitForSecondsRealtime(2);
            //EnemyWaveStart(waveTwoCount, waveTwo);
            moon.GetComponent<MoonManager>().canMove = true;
            StartCoroutine(TransitionTimer(waveTwo));
            waveOneCount = -1;
            Debug.Log("wave1");
        }
        if (EnemyWaveCountReduction(waveTwo) && waveTwoCount == 1)
        {
            moon.GetComponent<MoonManager>().canMove = true;
            StartCoroutine(TransitionTimer(waveThree));
            waveTwoCount = -1;
            Debug.Log("wave2");
        }
        if (EnemyWaveCountReduction(waveThree) && waveThreeCount == 1)
        {
            moon.GetComponent<MoonManager>().canMove = true;
            StartCoroutine(TransitionTimer(waveFour));
            waveThreeCount = -1;
            Debug.Log("wave3");
        }
        if (EnemyWaveCountReduction(waveFour) && waveFourCount == 1)
        {
            enabled = false;
        }
    }



    void EnemyWaveStart(GameObject[] movingWaveList)
    {
        if (movingWaveList.Length > 0)
        {
            foreach (GameObject enemy in movingWaveList)
            {

                enemy.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
            }

        }

    }
    void EnemyWaveFreeze(GameObject[] waveList)
    {
        foreach (GameObject enemy in waveList)
        {
            enemy.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }

    }

    public bool MovementCheck(int waveCount)
    {
        if (waveCount == 0)
        {
            return false;
        }
        else
        {
            return true;
        }
    }
    private bool EnemyWaveCountReduction(GameObject[] waveArray)
    {
        if (waveArray.Length == 0)
        {
            Debug.Log("true");
            return true;
            
        }
        else
        {
            return false;
        }
        
    }

 
    IEnumerator TransitionTimer(GameObject[] movingWaveList)
    {

       while(moon.GetComponent<MoonManager>().canMove == true)
        {
            if (timer >= transitionTime)
            {
                if (EnemyWaveCountReduction(waveTwo) && waveTwoCount == -1)
                {
                    Vector3 tempPos = sun.transform.position;
                    moon.transform.position = tempPos;
                    StartCoroutine(TransitionTimer(waveThree));
                    waveTwoCount = -1;
                    Debug.Log("wave2");
                }
                //Debug.Log("if statement");
                moon.GetComponent<MoonManager>().canMove = false;
                EnemyWaveStart(movingWaveList);
                //StopCoroutine("TransitionTimer");
                timer = 0;
                yield break;

            }
            timer += .01f;
            //Debug.Log(timer);
            yield return new WaitForSecondsRealtime(.01f);
        }
        StopAllCoroutines();
       

            
        

    }

}
