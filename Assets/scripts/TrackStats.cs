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

public class TrackStats : MonoBehaviour
{
    //stat variables and public instance for changing stat values from other objects
    public static TrackStats SharedInstance;
    public int EnemiesDestroyed;
    public int BulletsFired;
    public int TotalSpecialAbilitiesUsed;
    public int TotalDeaths;
    public int NumLevelOneCompleted;
    public int NumLevelTwoCompleted;
    public int NumLevelThreeCompleted;
    public float AudioLevel;

    private void Awake()
    {
        //sets default audio level
        SharedInstance = this;
        if (!PlayerPrefs.HasKey("AudioLevel"))
        {

            PlayerPrefs.SetFloat("AudioLevel", .5f);
        }
        Load();
    }

    private void Update()
    {
        AudioListener.volume = AudioLevel;
    }

    //saves data to player prefs
    public void Save()
    {

        PlayerPrefs.SetInt("EnemiesDestroyed", EnemiesDestroyed);
        PlayerPrefs.SetInt("BulletsFired", BulletsFired);
        PlayerPrefs.SetInt("TotalSpecialAbilitiesUsed", TotalSpecialAbilitiesUsed);
        PlayerPrefs.SetInt("TotalDeaths", TotalDeaths);
        PlayerPrefs.SetInt("NumLevelOneCompleted", NumLevelOneCompleted);
        PlayerPrefs.SetInt("NumLevelTwoCompleted", NumLevelTwoCompleted);
        PlayerPrefs.SetInt("NumLevelThreeCompleted", NumLevelThreeCompleted);
        PlayerPrefs.SetFloat("AudioLevel", AudioLevel);
        

    }
    //loads data from player prefs 
    private void Load()
    {
        if (PlayerPrefs.HasKey("EnemiesDestroyed"))
        {
            EnemiesDestroyed = PlayerPrefs.GetInt("EnemiesDestroyed");
            BulletsFired = PlayerPrefs.GetInt("BulletsFired");
            TotalSpecialAbilitiesUsed = PlayerPrefs.GetInt("TotalSpecialAbilitiesUsed");
            TotalDeaths = PlayerPrefs.GetInt("TotalDeaths");
            NumLevelOneCompleted = PlayerPrefs.GetInt("NumLevelOneCompleted");
            NumLevelTwoCompleted = PlayerPrefs.GetInt("NumLevelTwoCompleted");
            NumLevelThreeCompleted = PlayerPrefs.GetInt("NumLevelThreeCompleted");
            AudioLevel = PlayerPrefs.GetFloat("AudioLevel");

        }
    }
}
