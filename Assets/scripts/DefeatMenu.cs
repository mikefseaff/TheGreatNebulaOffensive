using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class DefeatMenu : MonoBehaviour
{
    public bool isDefeated;
    public GameObject defeatMenuCanvas;
    public GameObject imageBeginning;
    public GameObject imageMiddle;
    private float timer;
    public AudioSource backgroundMusic;
    // Start is called before the first frame update

    // Update is called once per frame
    void Update()
    {
        if (isDefeated)
        {
            //defeatMenuCanvas.SetActive(true);
            //Time.timeScale = 0f;
            backgroundMusic.Pause();
            //imageMiddle.SetActive(false);
            DefeatAnimation();
            enabled = false; 
            
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

    private void DefeatAnimation()
    {
        TrackStats.SharedInstance.TotalDeaths += 1;
        TrackStats.SharedInstance.Save();
        defeatMenuCanvas.SetActive(true);
        imageMiddle.SetActive(false);
        
        StartCoroutine(Fade(imageMiddle, imageBeginning));
    }
    public void Retry()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }
    public void Quit()
    {
        SceneManager.LoadScene("Main Menu");
    }

    IEnumerator Fade(GameObject imageMiddle, GameObject imageBeginning)
    {
        while (timer < 1.5f)
        {
            
           
            timer += .1f;
            yield return new WaitForSeconds(.25f);
            
        }
        if (timer >= 1.5f)
        {
            Debug.Log("fade test");
            imageMiddle.SetActive(true);
            imageBeginning.SetActive(false);
            StopCoroutine(Fade(imageMiddle, imageBeginning));
            Time.timeScale = 0f;
        }

    }



    }


