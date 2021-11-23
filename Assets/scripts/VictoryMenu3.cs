using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu3 : MonoBehaviour
{
    public bool isVictory;
    
    public AudioSource backgroundMusic;
    public string LevelToLoad;
    public delegate void Fade();
    public static event Fade FadeOutStart;
    public GameObject PauseMenu;
    public GameObject PauseMenuController;
    private void OnEnable()
    {

        BossController.End += Victory;
        FadeOut.LoadLevel += LoadNewLevel;


    }

    private void OnDisable()
    {

        BossController.End -= Victory;
        //FadeOut.LoadLevel -= LoadNewLevel;


    }
    public void FadeEvent()
    {
        if (FadeOutStart != null)
            FadeOutStart();
    }

    // Update is called once per frame
    void Victory()
    {
        FadeEvent();
        LevelToLoad = "Thanks For Playing";
        //Time.timeScale = 0f;
        backgroundMusic.Pause();
    }

    public void Quit()
    {

        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }

    public void LoadNewLevel()
    {
        FadeOut.LoadLevel -= LoadNewLevel;
        SceneManager.LoadScene(LevelToLoad);
    }
}
