using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu2 : MonoBehaviour
{
    public bool isVictory;
    public GameObject victoryMenuCanvas;
    public AudioSource backgroundMusic;
    public GameObject player;
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
            TrackStats.SharedInstance.NumLevelTwoCompleted += 1;
            TrackStats.SharedInstance.EnemiesDestroyed += 1;
            TrackStats.SharedInstance.Save();
            Debug.Log("WON");
            enabled = false;
        }
        else
        {
            victoryMenuCanvas.SetActive(false);

        }
        if (GameObject.FindWithTag("Enemy3") == null)
        {
            isVictory = true;
        }
    }

    public void Quit()
    {
        TrackStats.SharedInstance.Save();
        SceneManager.LoadScene("Main Menu");
    }
}
