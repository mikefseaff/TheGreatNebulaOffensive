using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlController_front : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 scaleChange;

    private void Start()
    {
        scaleChange = new Vector3(0.0003f, 0.0003f, 0.0003f);
    }
    void Update()
    {
        transform.Rotate(0, 0, .035f);
        if(transform.localScale.x >= 3f)
        {
            scaleChange = -scaleChange;
        }
        else if(transform.localScale.x <= 2.6f )
        {
            scaleChange = -scaleChange;
        }
        transform.localScale += scaleChange;

    }
}
