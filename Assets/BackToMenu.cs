using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class BackToMenu : MonoBehaviour
{
    public delegate void Fade();
    public static event Fade FadeOutStart;
    public string LevelToLoad;
    // Start is called before the first frame update
    void Start()
    {
        
    }
    private void Awake()
    {
        FadeOut.LoadLevel += LoadNewLevel;
    }
    private void OnDisable()
    {
        FadeOut.LoadLevel -= LoadNewLevel;
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void FadeEvent()
    {
        if (FadeOutStart != null)
            FadeOutStart();
    }
    public void Menu()
    {
        FadeEvent();
        LevelToLoad = "Main Menu";
    }
    public void LoadNewLevel()
    {
        Debug.Log("Level Loaded");
        SceneManager.LoadScene(LevelToLoad);
    }
}
