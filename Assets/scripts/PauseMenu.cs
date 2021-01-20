using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class PauseMenu : MonoBehaviour
{
    public bool isPaused;

    public GameObject pauseMenuCanvas;

    public AudioSource backgroundMusic;

    public bool isPlaying;

    void Update()
    {
        if (isPaused)
        {
            pauseMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            backgroundMusic.Pause();
        }
        else if (!isPaused && isPlaying)
        {
            pauseMenuCanvas.SetActive(false);
            Time.timeScale = 1f;
            
           
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = !isPaused;
        }
    }
    public void Resume()
    {
        isPaused = false;
        isPlaying = true;
        backgroundMusic.Play();
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }
}
