using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu3 : MonoBehaviour
{
    public bool isVictory;
    public GameObject victoryMenuCanvas;
    public AudioSource backgroundMusic;

    private void OnEnable()
    {

        BossController.End += Victory;
        

    }

    private void OnDisable()
    {

        BossController.End -= Victory;
        

    }

    // Update is called once per frame
   void Victory()
    {
        victoryMenuCanvas.SetActive(true);
        Time.timeScale = 0f;
        backgroundMusic.Pause();
    }

    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
