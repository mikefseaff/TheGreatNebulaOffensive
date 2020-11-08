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
    public bool canDarken;
    public bool canLighten;
    private float rgbValue;
    private Color32 spriteColor;

    // Start is called before the first frame update
    void Start()
    {
        
        //transitionTime = waveManager.GetComponent<enemy_manager1>().transitionTime;
        StartCoroutine("Rotate");
        rgbValue = 1f;
    }

    // Update is called once per frame
    void Update()
    {

    }

    IEnumerator Rotate()
    {
        while (timer <= transitionTime)
        {
            
            if (canRotate)
            {
                // not rotating again after 1st wave is beaten
                
                Vector2 vel = rb.velocity;
                transform.Rotate(0, 0, .025f);
                rb.velocity = Quaternion.Euler(1, 1, .0025f) * vel;
                if (canDarken)
                {
                    rgbValue -= .001f;
                    spriteColor = new Color(rgbValue, rgbValue, rgbValue, 225);
                    this.GetComponent<SpriteRenderer>().color = spriteColor;
                }
                if (canLighten)
                {
                    rgbValue += .001f;
                    spriteColor = new Color(rgbValue, rgbValue, rgbValue, 225);
                    this.GetComponent<SpriteRenderer>().color = spriteColor;
                }




            }
            else if (!canRotate)
            {
                canDarken = false;
                canLighten = false;
                timer = 0;
                canRotate = false;
            }
            timer += .01f;
            yield return new WaitForSeconds(0.01f);

        }
       
        canRotate = false;
        timer = 0;
        StartCoroutine("Rotate");
        yield return new WaitForSeconds(0.01f);



    }
}
