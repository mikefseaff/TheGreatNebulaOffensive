using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class PauseMenu : MonoBehaviour
{
    public bool isPaused;

    public Canvas pauseMenuCanvas;

    public AudioSource backgroundMusic;

    public bool isPlaying;

    public Slider volume;

    private void Start()
    {
        AudioListener.volume = TrackStats.SharedInstance.AudioLevel;
        volume.value = TrackStats.SharedInstance.AudioLevel;
    }
    void Update()
    {
        if (isPaused)
        {
            pauseMenuCanvas.GetComponent<Animator>().SetBool("faded",false);
            pauseMenuCanvas.GetComponent<CanvasGroup>().interactable = true;
            pauseMenuCanvas.GetComponent<CanvasGroup>().blocksRaycasts = true;
            Invoke("stopTime", .12f);
            //Time.timeScale = 0f;
            backgroundMusic.Pause();
        }
        else if (!isPaused && isPlaying)
        {
            pauseMenuCanvas.GetComponent<Animator>().SetBool("faded", true);
            pauseMenuCanvas.GetComponent<CanvasGroup>().interactable = false;
            pauseMenuCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
            Time.timeScale = 0f;
            resumeTime();
            
           
            
        }

        if (Input.GetKeyDown(KeyCode.Escape) && !isPaused)
        {
            isPaused = !isPaused;
        }
    }
    private void stopTime()
    {
        Time.timeScale = 0;
    }
    private void resumeTime()
    {
        Time.timeScale = 1;
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
        TrackStats.SharedInstance.Save();
    }

    public void setVolume()
    {
        TrackStats.SharedInstance.AudioLevel = volume.value;
        
    }
}
