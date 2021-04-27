using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwirlController_mid : MonoBehaviour
{
    public Rigidbody2D rb;
    private Vector3 scaleChange;
    private void Start()
    {
        scaleChange = new Vector3(0.00015f, 0.00015f, 0.00015f);
    }
    void Update()
    {
        transform.Rotate(0, 0, .025f);
        if (transform.localScale.x >= 3f)
        {
            scaleChange = -scaleChange;
        }
        else if (transform.localScale.x <= 2.7f)
        {
            scaleChange = -scaleChange;
        }
        transform.localScale += scaleChange;
    }
}
