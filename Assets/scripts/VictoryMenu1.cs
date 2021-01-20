using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu1 : MonoBehaviour
{
    public bool isVictory;
    public GameObject victoryMenuCanvas;
    public AudioSource backgroundMusic;
    public GameObject player;
    public GameObject EnemyManager1;
    public float timer;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (isVictory)
        {
            victoryMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            backgroundMusic.Pause();
            player.GetComponent<Collider2D>().enabled = false;
        }
        else
        {
            victoryMenuCanvas.SetActive(false);

        }
        if (EnemyManager1.GetComponent<enemy_manager1>().waveFiveACount == -1 && EnemyManager1.GetComponent<enemy_manager1>().waveFiveBCount == -1)
        {
            StartCoroutine("VictoryDelay");
        }
    }

    public void Continue()
    {
        SceneManager.LoadScene("Level 2");
    }
    IEnumerator VictoryDelay()
    {
        while (timer <= .8f)
        {
            //Debug.Log("less than");

            timer += .001f;
            yield return new WaitForSeconds(.001f);
        }
        if (timer > .8f)
        {
            isVictory = true;
            StopAllCoroutines();
        }
            
    }
}
