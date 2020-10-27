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
    private float timer;
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
        if (EnemyManager1.GetComponent<enemy_manager1>().waveOneCount == 0 && EnemyManager1.GetComponent<enemy_manager1>().waveTwoCount == 0 
            && EnemyManager1.GetComponent<enemy_manager1>().waveThreeCount == 0 && EnemyManager1.GetComponent<enemy_manager1>().waveFourCount == 0)
        {
            StartCoroutine("VictoryDelay");
        }
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
    IEnumerator VictoryDelay()
    {
            if (timer == .8f)
            {
                isVictory = true;
            }
            if (timer >= .8f)
             {
                StopAllCoroutines();
             }

            timer += .01f;
            yield return new WaitForSeconds(.01f);
    }
}
