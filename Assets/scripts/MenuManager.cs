/*
*   Name: Michael Efseaff
*   ID: 2343166
*   Email: Efseaff@chapman.edu
*   Class: CPSC245-01
*   Final proj
*   This is my own work. I did not cheat on this assignment
*/
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuManager : MonoBehaviour
{
    //Canvas elements to be controlled by the manager
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

    //"play" button that loads the level based on completion of previous levels
    public void StartGame()
    {
        TrackStats.SharedInstance.Save();
        if (TrackStats.SharedInstance.NumLevelTwoCompleted > 0)
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

    //exits and saves the game
    public void QuitGame()
    {
        Debug.Log("game quit");
        TrackStats.SharedInstance.Save();
        Application.Quit();
    }

    //opens the options menu
    public void Options()
    {
        OptionsPanel.SetActive(true);
    }

    //shows the stats panel
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

    //closes the options menu and returns to the main menu
    public void Menu()
    {
        OptionsPanel.SetActive(false);
    }

    //gets slider value and sets volume
    public void setVolume()
    {
        TrackStats.SharedInstance.AudioLevel = volume.value;
    }

    //openes the level select screen and shows launch buttons is player has beaten the level
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

    //level load buttons
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
        SceneManager.LoadScene("level 3");
    }

    //writes the stats to the stats page
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
