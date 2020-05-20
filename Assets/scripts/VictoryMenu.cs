using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VictoryMenu : MonoBehaviour
{
    public bool isVictory;
    public GameObject victoryMenuCanvas;
    public AudioSource backgroundMusic;
    // Start is called before the first frame update


    // Update is called once per frame
    void Update()
    {
        if (isVictory)
        {
            victoryMenuCanvas.SetActive(true);
            Time.timeScale = 0f;
            backgroundMusic.Pause();
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
    public void Quit()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1);
    }
}
