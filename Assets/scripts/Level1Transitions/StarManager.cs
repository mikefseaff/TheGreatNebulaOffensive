using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarManager : MonoBehaviour
{
    private Animator anim;
    public bool canTranslate;
    public float transitionTime;
    public float timer;
    void Start()
    {
        anim = GetComponent<Animator>();
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
               // anim.CrossFade("Stars_Moving(level1)", .5f);
                anim.SetBool("canTransition", true); 
                //starsMoving.SetActive(true);
                //anim.Play("Stars_Moving(level1)");
            }
            else
            {

                
                anim.SetBool("canTransition", false);
                //anim.CrossFade("stars_twinkle(level1)", .5f);
                //anim.Play("stars_twinkle(level1)");
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
