using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlController_Back : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 scaleChange;
    private void Start()
    {
        scaleChange = new Vector3(0.0001f, 0.0001f, 0.0001f);
    }
    void Update()
    {
        transform.Rotate(0, 0, .015f);
        //if (transform.localScale.x >= 3f)
        //{
        //    scaleChange = -scaleChange;
        //}
        //else if (transform.localScale.x <= 2.8f)
        //{
        //    scaleChange = -scaleChange;
        //}
        //transform.localScale += scaleChange;
    }
}
