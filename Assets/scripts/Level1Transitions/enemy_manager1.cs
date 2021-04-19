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

    public int waveOneACount;
    public int waveTwoACount;
    public int waveThreeACount;
    public int waveFourACount;
    public int waveFiveACount;

    public int waveTwoBCount;
    public int waveThreeBCount;
    public int waveFourBCount;
    public int waveFiveBCount;

    private string tag1A;
    private string tag2A;
    private string tag3A;
    private string tag4A;
    private string tag5A;

    private string tag2B;
    private string tag3B;
    private string tag4B;
    private string tag5B;


    private GameObject[] waveOneA;
    private GameObject[] waveTwoA;
    private GameObject[] waveThreeA;
    private GameObject[] waveFourA;
    private GameObject[] waveFiveA;


    private GameObject[] waveTwoB;
    private GameObject[] waveThreeB;
    private GameObject[] waveFourB;
    private GameObject[] waveFiveB;



    // Start is called before the first frame update
    void Start()
    {
        tag1A = "Wave1A";
        tag2A = "Wave2A";
        tag3A = "Wave3A";
        tag4A = "Wave4A";
        tag5A = "Wave5A";

        tag2B = "Wave2B";
        tag3B = "Wave3B";
        tag4B = "Wave4B";
        tag5B = "Wave5B";

        waveOneA = GameObject.FindGameObjectsWithTag(tag1A);
        waveTwoA = GameObject.FindGameObjectsWithTag(tag2A);
        waveThreeA = GameObject.FindGameObjectsWithTag(tag3A);
        waveFourA = GameObject.FindGameObjectsWithTag(tag4A);
        waveFiveA = GameObject.FindGameObjectsWithTag(tag5A);

        waveTwoB = GameObject.FindGameObjectsWithTag(tag2B);
        waveThreeB = GameObject.FindGameObjectsWithTag(tag3B);
        waveFourB = GameObject.FindGameObjectsWithTag(tag4B);
        waveFiveB = GameObject.FindGameObjectsWithTag(tag5B);

        EnemyWaveFreezeA(waveOneA);
        EnemyWaveFreezeA(waveTwoA);
        EnemyWaveFreezeA(waveThreeA);
        EnemyWaveFreezeA(waveFourA);
        EnemyWaveFreezeA(waveFiveA);

        EnemyWaveFreezeB(waveTwoB);
        EnemyWaveFreezeB(waveThreeB);
        EnemyWaveFreezeB(waveFourB);
        EnemyWaveFreezeB(waveFiveB);

        playerStartPos = player.transform.position;
        startBackgroundTransition = false;

        EnemyWaveStartA(waveOneA);

    }

    // Update is called once per frame
    void Update()
    {

        waveOneA = GameObject.FindGameObjectsWithTag(tag1A);
        waveTwoA = GameObject.FindGameObjectsWithTag(tag2A);
        waveThreeA = GameObject.FindGameObjectsWithTag(tag3A);
        waveFourA = GameObject.FindGameObjectsWithTag(tag4A);
        waveFiveA = GameObject.FindGameObjectsWithTag(tag5A);

        waveTwoB = GameObject.FindGameObjectsWithTag(tag2B);
        waveThreeB = GameObject.FindGameObjectsWithTag(tag3B);
        waveFourB = GameObject.FindGameObjectsWithTag(tag4B);
        waveFiveB = GameObject.FindGameObjectsWithTag(tag5B);
        
        if (EnemyWaveCountReduction(waveOneA) && waveOneACount == 1)
        {
            PlayerTransition(playerStartPos);
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveTwoA, waveTwoB);
                waveOneACount = -1;

            }
        }
        if (EnemyWaveCountReduction(waveTwoA) && waveTwoACount == 1 && EnemyWaveCountReduction(waveTwoB) && waveTwoBCount == 1)
        {
            PlayerTransition(playerStartPos);
            planet.GetComponent<PlanetManager>().canDarken = true;
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveThreeA, waveThreeB);
                waveTwoACount = -1;
                waveTwoBCount = -1;

            }
           
        }
        if (EnemyWaveCountReduction(waveThreeA) && waveThreeACount == 1 && EnemyWaveCountReduction(waveThreeB) && waveThreeBCount == 1)
        {
            PlayerTransition(playerStartPos);
            planet.GetComponent<PlanetManager>().canLighten = true;
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveFourA, waveFourB);
                waveThreeACount = -1;
                waveThreeBCount = -1;

            }

        }
        if (EnemyWaveCountReduction(waveFourA) && waveFourACount == 1 && EnemyWaveCountReduction(waveFourB) && waveFourBCount == 1)
        {
            PlayerTransition(playerStartPos);
            if (startBackgroundTransition)
            {

                BackgroundTransition(waveFiveA, waveFiveB);
                waveFourACount = -1;
                waveFourBCount = -1;

            }
        }
        if (EnemyWaveCountReduction(waveFiveA) && waveFiveACount == 1 && EnemyWaveCountReduction(waveFiveB) && waveFiveBCount == 1)
        {
            waveFiveACount = -1;
            waveFiveBCount = -1;
            enabled = false;
        }
    }



    void EnemyWaveStartA(GameObject[] movingWaveListA)
    {
        if (movingWaveListA.Length > 0)
        {
            foreach (GameObject enemy in movingWaveListA)
            {

                enemy.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.None;
                enemy.gameObject.GetComponent<enemy_controller1>().canfireBullets = true;
                enemy.gameObject.GetComponent<enemy_controller1>().StartCoroutine("FireBullet");





            }
            
            player.GetComponent<player_controller>().enabled = true;

        }

    }

    void EnemyWaveStartB(GameObject[] movingWaveListA)
    {
        if (movingWaveListA.Length > 0)
        {
            foreach (GameObject enemy in movingWaveListA)
            {

                enemy.gameObject.GetComponent<enemy_controller2>().canMove = true;
                enemy.gameObject.GetComponent<enemy_controller2>().canfireBullets = true;
                enemy.gameObject.GetComponent<enemy_controller2>().StartCoroutine("FireBullet");



            }

        }

    }
    void EnemyWaveFreezeA(GameObject[] waveList)
    {
        foreach (GameObject enemy in waveList)
        {
            enemy.gameObject.GetComponent<Rigidbody2D>().constraints = RigidbodyConstraints2D.FreezeAll;
            //enemy.gameObject.GetComponent<enemy_controller1>().canfireBullets = false;
            
        }

    }

    void EnemyWaveFreezeB(GameObject[] waveList)
    {
        foreach (GameObject enemy in waveList)
        {
            enemy.gameObject.GetComponent<enemy_controller2>().canMove = false;
            //enemy.gameObject.GetComponent<enemy_controller2>().canfireBullets = false;
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

    void BackgroundTransition(GameObject[] movingWaveListA, GameObject[] movingWaveListB)
    {
            sun.GetComponent<SunManager>().checkPos = true;
            moon.GetComponent<MoonManager>().checkPos = true;
            planet.GetComponent<PlanetManager>().canRotate = true;
            stars.GetComponent<StarManager>().canTranslate = true;
            player.GetComponent<Animator>().SetBool("isBlastingOff", true);
            StartCoroutine(TransitionTimer(movingWaveListA, movingWaveListB));
            


    }

    void PlayerTransition(Vector3 startingPlayerPos)
    {
        player.GetComponent<CircleCollider2D>().enabled = false;
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
    IEnumerator TransitionTimer(GameObject[] movingWaveListA, GameObject[] movingWaveListB)
    {

       while(timer <= transitionTime)
        {
            //Debug.Log("less than");

            timer += .001f;
            yield return new WaitForSeconds(.001f);
        }
        if (timer >= transitionTime)
        {
            Debug.Log("transtitiontimer");
            player.GetComponent<Animator>().SetBool("isBlastingOff", false);
            player.GetComponent<CircleCollider2D>().enabled = true;
            EnemyWaveStartA(movingWaveListA);
            EnemyWaveStartB(movingWaveListB);
            timer = 0;
            yield break;

        }

    }
}
