using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FadeOut : MonoBehaviour
{
    public CanvasGroup CanvsGroup;
    // Start is called before the first frame update
    public delegate void Load();
    public static event Load LoadLevel;
    void Awake()
    {
        
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            MenuManager.FadeOutStart += StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "level 1")
        {
            VictoryMenu1.FadeOutStart += StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "level 2")
        {
            VictoryMenu2.FadeOutStart += StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "level 3")
        {
            VictoryMenu3.FadeOutStart += StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "Thanks For Playing")
        {
            BackToMenu.FadeOutStart += StartFadeProcess;
        }

        StartCoroutine(fadeIn());
    }
    public void LoadEvent()
    {
        if (LoadLevel != null)
            LoadLevel();
    }
    private void OnDisable()
    {
        
        if (SceneManager.GetActiveScene().name == "Main Menu")
        {
            MenuManager.FadeOutStart -= StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "level 1")
        {
            VictoryMenu1.FadeOutStart -= StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "level 2")
        {
            VictoryMenu2.FadeOutStart -= StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "level 3")
        {
            VictoryMenu3.FadeOutStart -= StartFadeProcess;
        }
        if (SceneManager.GetActiveScene().name == "Thanks For Playing")
        {
            BackToMenu.FadeOutStart -= StartFadeProcess;
        }
    }


    // Update is called once per frame
    void Update()
    {
        
    }

    private void StartFadeProcess()
    {
        StartCoroutine(fadeDelay());
    }
    private IEnumerator fadeDelay()
    {
        CanvsGroup.blocksRaycasts = true;
        while (CanvsGroup.alpha < 1)
        {
            CanvsGroup.alpha += .01f;
            Debug.Log("corutine");
            yield return new WaitForSecondsRealtime(.01f);
            
        }
        FadeOutAndLoadScene();
        
    }

    private IEnumerator fadeIn()
    {
        while (CanvsGroup.alpha > 0)
        {
            CanvsGroup.alpha -= .01f;
            //Debug.Log("corutine");
            yield return new WaitForSecondsRealtime(.01f);

        }
        CanvsGroup.blocksRaycasts = false;


    }
    private void FadeOutAndLoadScene()
    {
        LoadEvent();

    }
}
