using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelStartManager : MonoBehaviour
{
    public bool isDebriefing;
    public AudioSource backgroundMusic;
    public GameObject inLevelUI;
    public GameObject debriefMenu;
    void Start()
    {
        Time.timeScale = 0f;
        backgroundMusic.Pause();
        inLevelUI.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Launch()
    {
        Time.timeScale = 1f;
        inLevelUI.SetActive(true);
        debriefMenu.SetActive(false);
        backgroundMusic.Play();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
}
