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
    public Canvas DebriefCanvas;
    public GameObject PauseMenu;
    public GameObject PauseMenuController;
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
        DebriefCanvas.GetComponent<Animator>().SetBool("faded", true);
        DebriefCanvas.GetComponent<CanvasGroup>().blocksRaycasts = false;
        DebriefCanvas.GetComponent<CanvasGroup>().interactable = false;
        Time.timeScale = 1f;
        inLevelUI.SetActive(true);
        Invoke("hideDebriefmenu", 1);
        PauseMenu.SetActive(true);
        PauseMenuController.SetActive(true);
        backgroundMusic.Play();
    }

    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
        Time.timeScale = 1f;
    }
    public void hideDebriefmenu()
    {
        debriefMenu.SetActive(false);
    }
}
