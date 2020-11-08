using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    private Animator anim;

    public GameObject starsTwinkle;
    public GameObject starsMoving;
    public bool canTranslate;
    public float transitionTime;
    public float timer;
    void Start()
    {
        anim = starsMoving.GetComponent<Animator>();
        starsTwinkle.GetComponent<SpriteRenderer>().enabled = true;
        starsMoving.GetComponent<SpriteRenderer>().enabled = false;
        canTranslate = false;
        StartCoroutine("Transition");
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Transition()
    {
        while (timer <= transitionTime)
        {

            if (canTranslate)
            {
  
                //starsMoving.SetActive(true);
                starsMoving.GetComponent<SpriteRenderer>().enabled = true;
                starsTwinkle.GetComponent<SpriteRenderer>().enabled = false;
                anim.Play("Stars_Moving(level1)");  
            }
            else
            {

                /*starsMoving.SetActive(false);
                starsTwinkle.SetActive(true);*/
                starsTwinkle.GetComponent<SpriteRenderer>().enabled = true;
                starsMoving.GetComponent<SpriteRenderer>().enabled = false;
                //anim.SetBool("canTranslate", false);
                anim.Play("Stars_Moving(level1)", -1, 0);
                canTranslate = false;
                timer = 0;
            }
            timer += .01f;
            yield return new WaitForSeconds(0.01f);

        }

        timer = 0;
        canTranslate = false;
        StartCoroutine("Transition");
        yield break;
    }
}
