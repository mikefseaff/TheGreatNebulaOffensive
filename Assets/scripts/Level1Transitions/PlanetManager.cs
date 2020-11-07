using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlanetManager : MonoBehaviour
{
    public float transitionTime;
    public float timer;
    public Rigidbody2D rb;
    public GameObject waveManager;
    public bool canRotate;

    // Start is called before the first frame update
    void Start()
    {
        Debug.Log(rb.velocity);
        //transitionTime = waveManager.GetComponent<enemy_manager1>().transitionTime;
        StartCoroutine("Rotate");
    }

    // Update is called once per frame
    void Update()
    {
/*        if (canRotate)
        {
            
        }
        else if (!canRotate)
        {
            StopCoroutine("Rotate");
        }*/
    }

    IEnumerator Rotate()
    {
        while (timer <= transitionTime)
        {
            //Debug.Log("rotate");
            if (canRotate)
            {
                // not rotating again after 1st wave is beaten
                Debug.Log(timer);
                Vector2 vel = rb.velocity;
                transform.Rotate(0, 0, .025f);
                rb.velocity = Quaternion.Euler(1, 1, .0025f) * vel;
                



            }
            else if (!canRotate)
            {
                Debug.Log("!canRotate");
                timer = 0;
                canRotate = false;
            }
            timer += .01f;
            yield return new WaitForSeconds(0.01f);

        }
        Debug.Log("timer>= transitionTime");
        canRotate = false;
        timer = 0;
        StartCoroutine("Rotate");
        yield return new WaitForSeconds(0.01f);



    }
}
