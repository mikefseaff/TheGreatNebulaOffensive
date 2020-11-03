using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_manager1 : MonoBehaviour
{
    public GameObject moon;
    public GameObject stars;
    public GameObject planet;
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

        EnemyWaveFreeze(waveOne);
        EnemyWaveFreeze(waveTwo);
        EnemyWaveFreeze(waveThree);
        EnemyWaveFreeze(waveFour);

    }

    // Update is called once per frame
    void Update()
    {
        //add one to the end for the annimation couroutine pause
        waveOneCount = GameObject.FindGameObjectsWithTag(tag1).Length;
        waveTwoCount = GameObject.FindGameObjectsWithTag(tag2).Length;
        waveThreeCount = GameObject.FindGameObjectsWithTag(tag3).Length;
        waveFourCount = GameObject.FindGameObjectsWithTag(tag4).Length;
        EnemyWaveStart(waveOneCount, waveOne);
        if (waveOneCount == 0)
            {

            //MAKE CORUTINE HERE MICHAEL!
            //yeild WaitForSecondsRealtime(2);
            //EnemyWaveStart(waveTwoCount, waveTwo);
            StartCoroutine(TransitionTimer(waveTwoCount, waveTwo));
            waveOneCount += 1;
            }
        if (waveTwoCount == 0)
          {
                EnemyWaveStart(waveThreeCount, waveThree);
          }   
        if (waveThreeCount == 0)
          {
            EnemyWaveStart(waveFourCount, waveFour);
          }
        if (waveFourCount == 0)
           {
              enabled = false;
           }
    }   
        
    

    void EnemyWaveStart(int movingWave, GameObject[] movingWaveList)
    {
        if (movingWave > 0)
        {
            
            foreach (GameObject enemy in movingWaveList)
            {
                
                enemy.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None ;
            }
        }
    }
    void EnemyWaveFreeze(GameObject[] waveList)
    {
        foreach (GameObject enemy in waveList )
        {
            enemy.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
        }
        
    }
    IEnumerator TransitionTimer(int movingWave, GameObject[] movingWaveList)
    {
        moon.GetComponent<MoonManager>().canMove = true;
        if (timer >= transitionTime)
        {
            Debug.Log("if statement");
            moon.GetComponent<MoonManager>().canMove = false;
            EnemyWaveStart(movingWave, movingWaveList);
            timer = 0;
            StopCoroutine("TransitionTimer");
        }

            timer += .01f;
        Debug.Log(timer);
            yield return new WaitForSecondsRealtime(.01f);
        

    }

}
