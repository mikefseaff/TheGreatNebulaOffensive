using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class VictoryMenu2 : MonoBehaviour
{
    public bool isVictory;
    public GameObject victoryMenuCanvas;
    public Canvas VictoryCanvas;
    public AudioSource backgroundMusic;
    public GameObject player;
    // Start is called before the first frame update
    public Text PostBattleStats;
    public string LevelToLoad;
    public delegate void Fade();
    public static event Fade FadeOutStart;
    public GameObject PauseMenu;
    public GameObject PauseMenuController;

    // Update is called once per frame
    private void Awake()
    {
        FadeOut.LoadLevel += LoadNewLevel;
    }
    private void OnDisable()
    {
        FadeOut.LoadLevel -= LoadNewLevel;
    }
    void Update()
    {
        if (isVictory)
        {
            TrackStats.SharedInstance.NumLevelTwoCompleted += 1;
            TrackStats.SharedInstance.EnemiesDestroyed += 1;
            TrackStats.SharedInstance.Save();
            writeStats();
            victoryMenuCanvas.SetActive(true);
            VictoryCanvas.GetComponent<Animator>().SetBool("faded", false);
            Invoke("stopTime", .5f);
            //Time.timeScale = 0f;
            backgroundMusic.Pause();
            player.GetComponent<Collider2D>().enabled = false;
            
            PauseMenu.SetActive(false);
            PauseMenuController.SetActive(false);
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

    private void stopTime()
    {
        Time.timeScale = 0;
    }
    public void FadeEvent()
    {
        if (FadeOutStart != null)
            FadeOutStart();
    }

    public void Continue()
    {
        FadeEvent();
        LevelToLoad = "level 3";
    }
    public void Menu()
    {
        FadeEvent();
        SceneManager.LoadScene("Main Menu");
    }
    public void LoadNewLevel()
    {
        SceneManager.LoadScene(LevelToLoad);
    }
    public void writeStats()
    {
        PostBattleStats.text = "Enemies Destroyed: " + TrackStats.SharedInstance.EnemiesDestroyed + '\n' + '\n' +

                    "Bullets Fired: " + TrackStats.SharedInstance.BulletsFired + '\n' + '\n' +

                   "Number of Special Abilities Used: " + TrackStats.SharedInstance.TotalSpecialAbilitiesUsed + '\n' + '\n' +

                    "Total Deaths: " + TrackStats.SharedInstance.TotalDeaths + '\n' + '\n' +

                   "Times Level One Completed: " + TrackStats.SharedInstance.NumLevelOneCompleted + '\n' + '\n' +

                   "Times Level Two Completed: " + TrackStats.SharedInstance.NumLevelTwoCompleted + '\n' + '\n' +

                   "Times Level Three Completed: " + TrackStats.SharedInstance.NumLevelThreeCompleted + '\n' + '\n';
    }
}
