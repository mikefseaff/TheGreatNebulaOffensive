using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StarsManager : MonoBehaviour
{
    private Animator anim;
    public bool canTranslate;
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
 
            //Debug.Log("rotate");
       if (canTranslate)
        {
            Debug.Log("true");
            anim.SetBool("isTranstioning", true);
            yield return new WaitForSeconds(3.3f);
        }
       else if (!canTranslate)
        {
            anim.SetBool("isTranstioning", false);
        }
        
        StartCoroutine("Transition");
        yield return new WaitForSeconds(0.01f);



    }
}
