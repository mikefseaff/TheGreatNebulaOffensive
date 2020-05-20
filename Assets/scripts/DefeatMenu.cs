using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DefeatMenu : MonoBehaviour
{
    public bool isDefeated;
    public GameObject defeatMenuCanvas;
    public AudioSource backgroundMusic;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (isDefeated)
        {
            defeatMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            backgroundMusic.Pause();
        }
        else
        {
            defeatMenuCanvas.SetActive(false);
   
        }
        if (GameObject.FindWithTag("Player") == null)
        {
            isDefeated = true;
        }
    }
    public void Retry()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}

