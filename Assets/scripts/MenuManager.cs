using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuManager : MonoBehaviour
{
    public void StartGame()
    {
        SceneManager.LoadScene("level 1");
    }

    public void QuitGame()
    {
        Debug.Log("game quit");
        Application.Quit();
    }
}
