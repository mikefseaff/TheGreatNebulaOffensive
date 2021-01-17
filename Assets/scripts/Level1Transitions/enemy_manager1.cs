using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class enemy_manager1 : MonoBehaviour
{
    public GameObject sun;
    public GameObject moon;
    public GameObject stars;
    public GameObject planet;
    public GameObject player;
    public Vector3 playerStartPos;
    public bool startBackgroundTransition;
    public float timer;
    public float transitionTime;

    public int waveOneCount;
    public int waveTwoCount;
    public int waveThreeCount;
    public int waveFourCount;
    public int waveFiveCount;

    private string tag1;
    private string tag2;
    private string tag3;
    private string tag4;
    private string tag5;

    private GameObject[] waveOne;
    private GameObject[] waveTwo;
    private GameObject[] waveThree;
    private GameObject[] waveFour;
    private GameObject[] waveFive;



    // Start is called before the first frame update
    void Start()
    {
        tag1 = "Wave1";
        tag2 = "Wave2";
        tag3 = "Wave3";
        tag4 = "Wave4";
        tag5 = "Wave5";

        waveOne = GameObject.FindGameObjectsWithTag(tag1);
        waveTwo = GameObject.FindGameObjectsWithTag(tag2);
        waveThree = GameObject.FindGameObjectsWithTag(tag3);
        waveFour = GameObject.FindGameObjectsWithTag(tag4);
        waveFive = GameObject.FindGameObjectsWithTag(tag5);


        EnemyWaveFreeze(waveOne);
        EnemyWaveFreeze(waveTwo);
        EnemyWaveFreeze(waveThree);
        EnemyWaveFreeze(waveFour);
        EnemyWaveFreeze(waveFive);

        playerStartPos = player.transform.position;
        startBackgroundTransition = false;

    }

    // Update is called once per frame
    void Update()
    {

        waveOne = GameObject.FindGameObjectsWithTag(tag1);
        waveTwo = GameObject.FindGameObjectsWithTag(tag2);
        waveThree = GameObject.FindGameObjectsWithTag(tag3);
        waveFour = GameObject.FindGameObjectsWithTag(tag4);
        waveFive = GameObject.FindGameObjectsWithTag(tag5);
        EnemyWaveStart(waveOne);
        if (EnemyWaveCountReduction(waveOne) && waveOneCount == 1)
        {
            PlayerTransition(playerStartPos);
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveTwo);
                waveOneCount = -1;

            }
        }
        if (EnemyWaveCountReduction(waveTwo) && waveTwoCount == 1)
        {
            PlayerTransition(playerStartPos);
            planet.GetComponent<PlanetManager>().canDarken = true;
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveThree);
                waveTwoCount = -1;

            }
           
        }
        if (EnemyWaveCountReduction(waveThree) && waveThreeCount == 1)
        {
            PlayerTransition(playerStartPos);
            planet.GetComponent<PlanetManager>().canLighten = true;
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveFour);
                waveThreeCount = -1;

            }

        }
        if (EnemyWaveCountReduction(waveFour) && waveFourCount == 1)
        {
            PlayerTransition(playerStartPos);
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveFive);
                waveFourCount = -1;

            }
        }
        if (EnemyWaveCountReduction(waveFive) && waveFiveCount == 1)
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
                player.GetComponent<player_controller>().enabled = true;
                
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
            
            return true;
            
        }
        else
        {
            return false;
        }
        
    }

    void BackgroundTransition(GameObject[] movingWaveList)
    {
            sun.GetComponent<SunManager>().checkPos = true;
            moon.GetComponent<MoonManager>().checkPos = true;
            planet.GetComponent<PlanetManager>().canRotate = true;
            stars.GetComponent<StarManager>().canTranslate = true;
            player.GetComponent<Animator>().SetBool("isBlastingOff", true);
            StartCoroutine(TransitionTimer(movingWaveList));


    }

    void PlayerTransition(Vector3 startingPlayerPos)
    {
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll; //Fixes velocity bug when moving while transition happens
        player.GetComponent<player_controller>().enabled = false;
        startBackgroundTransition = false;
        player.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
        player.transform.position = Vector3.MoveTowards(player.transform.position, startingPlayerPos, 5*Time.deltaTime);
        if(player.transform.position == startingPlayerPos)
        {
            startBackgroundTransition = true;
        }
    }

    //Purpose:  
    //Input:
    //Output: 
    IEnumerator TransitionTimer(GameObject[] movingWaveList)
    {

       while(timer <= transitionTime)
        {
            //Debug.Log("less than");

            timer += .001f;
            yield return new WaitForSecondsRealtime(.001f);
        }
        if (timer >= transitionTime)
        {
            Debug.Log("transtitiontimer");
            player.GetComponent<Animator>().SetBool("isBlastingOff", false);
            EnemyWaveStart(movingWaveList);
            timer = 0;
            yield break;

        }

    }
}
