using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrackStats : MonoBehaviour
{

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
