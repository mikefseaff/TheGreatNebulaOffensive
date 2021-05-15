using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    public GameObject OptionsPanel;
    public GameObject StatsPanel;
    public Text statsText;
    public Slider volume;
    public GameObject LevelSelect;
    public GameObject LevelSelectMenu;
    public GameObject LaunchLevel1;
    public GameObject LaunchLevel2;
    public GameObject LaunchLevel3;

    


    private void Start()
    {
        writeStats();
        AudioListener.volume = TrackStats.SharedInstance.AudioLevel;
        volume.value = TrackStats.SharedInstance.AudioLevel;
        if(TrackStats.SharedInstance.NumLevelOneCompleted > 0)
        {
            LevelSelect.SetActive(true);
        }
        Time.timeScale = 1f;
    }
    public void StartGame()
    {
        if(TrackStats.SharedInstance.NumLevelTwoCompleted > 0)
        {
            SceneManager.LoadScene("level 3");
        }
        else if(TrackStats.SharedInstance.NumLevelOneCompleted > 0)
        {
            SceneManager.LoadScene("level 2");
        }
        else
        {
            SceneManager.LoadScene("level 1");
        }
    }

    public void QuitGame()
    {
        Debug.Log("game quit");
        TrackStats.SharedInstance.Save();
        Application.Quit();
    }

    public void Options()
    {
        OptionsPanel.SetActive(true);
    }

    public void Stats()
    {
        if(StatsPanel.activeInHierarchy == true)
        {
            StatsPanel.SetActive(false);
        }
        else
        {
            StatsPanel.SetActive(true);
        }
    }

    public void Menu()
    {
        OptionsPanel.SetActive(false);
    }

    public void setVolume()
    {
        TrackStats.SharedInstance.AudioLevel = volume.value;
    }

    public void selectLevel()
    {
        if (LevelSelectMenu.activeInHierarchy == true)
        {
            LevelSelectMenu.SetActive(false);
        }
        else
        {
            LevelSelectMenu.SetActive(true);
            if (TrackStats.SharedInstance.NumLevelOneCompleted == 0)
            {
                LaunchLevel1.SetActive(false);
            }
            if (TrackStats.SharedInstance.NumLevelTwoCompleted == 0)
            {
                LaunchLevel2.SetActive(false);
            }
            if (TrackStats.SharedInstance.NumLevelThreeCompleted == 0)
            {
                LaunchLevel3.SetActive(false);
            }


        }
    }

    public void LoadLevel1()
    {
        SceneManager.LoadScene("level 1");
    }

    public void LoadLevel2()
    {
        SceneManager.LoadScene("level 2");
    }

    public void LoadLevel3()
    {
        SceneManager.LoadScene("level 2");
    }

    public void writeStats()
    {
        statsText.text = "Enemies Destroyed: " + TrackStats.SharedInstance.EnemiesDestroyed + '\n' + '\n' +

                    "Bullets Fired: " + TrackStats.SharedInstance.BulletsFired + '\n' + '\n' +

                   "Number of Special Abilities Used: " + TrackStats.SharedInstance.TotalSpecialAbilitiesUsed + '\n' + '\n' +

                    "Total Deaths: " + TrackStats.SharedInstance.TotalDeaths + '\n' + '\n' +

                   "Times Level One Completed: " + TrackStats.SharedInstance.NumLevelOneCompleted + '\n' + '\n' +

                   "Times Level Two Completed: " + TrackStats.SharedInstance.NumLevelTwoCompleted + '\n'+ '\n' +

                   "Times Level Three Completed: " + TrackStats.SharedInstance.NumLevelThreeCompleted + '\n' + '\n';
    }
}
