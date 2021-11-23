using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;


public class VictoryMenu1 : MonoBehaviour
{
    public bool isVictory;
    public GameObject victoryMenuCanvas;
    public Canvas VictoryCanvas;
    public AudioSource backgroundMusic;
    public GameObject player;
    public GameObject EnemyManager1;
    public float timer;
    public Text PostBattleStats;
    public string LevelToLoad;
    // Start is called before the first frame update
    public delegate void Fade();
    public static event Fade FadeOutStart;
    public GameObject PauseMenu;
    public GameObject PauseMenuController;
    private void Awake()
    {
        FadeOut.LoadLevel += LoadNewLevel;
    }
    //private void OnDisable()
    //{
    //    FadeOut.LoadLevel -= LoadNewLevel;
    //}
    // Update is called once per frame
    void Update()
    {
        if (isVictory)
        {
            victoryMenuCanvas.SetActive(true);
            VictoryCanvas.GetComponent<Animator>().SetBool("faded", false);
            Invoke("stopTime", .5f);
            //Time.timeScale = 0f;
            backgroundMusic.Pause();
            player.GetComponent<Collider2D>().enabled = false;
            TrackStats.SharedInstance.NumLevelOneCompleted += 1;
            TrackStats.SharedInstance.Save();
            writeStats();
            PauseMenu.SetActive(false);
            PauseMenuController.SetActive(false);
            enabled = false;
        }
        else
        {
            victoryMenuCanvas.SetActive(false);

        }
        if (EnemyManager1.GetComponent<enemy_manager1>().waveFiveACount == -1 && EnemyManager1.GetComponent<enemy_manager1>().waveFiveBCount == -1)
        {
            StartCoroutine("VictoryDelay");
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
        LevelToLoad = "level 2";
    }
    public void Menu()
    {
        SceneManager.LoadScene("Main Menu");
    }
    public void LoadNewLevel()
    {
        Debug.Log("LOADNEWLEVEL");
        FadeOut.LoadLevel -= LoadNewLevel;
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

    IEnumerator VictoryDelay()
    {
        while (timer <= .8f)
        {
            //Debug.Log("less than");

            timer += .001f;
            yield return new WaitForSeconds(.001f);
        }
        if (timer > .8f)
        {
            isVictory = true;
            StopAllCoroutines();
        }
            
    }
}
