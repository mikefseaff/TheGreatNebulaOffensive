using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCannonRotater : MonoBehaviour
{
    private Rigidbody2D rb;
    private int wayToRotate;
    void Start()
    {
        rb = this.gameObject.GetComponent<Rigidbody2D>();
        if(this.transform.position.y < 0)
        {
            wayToRotate = -1;
        }
        else
        {
            wayToRotate = 1;
        }
        StartCoroutine("Sweep180");

    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator Sweep180()
    {
        while (true)
        {
     
            transform.Rotate(0, 0, .6f*wayToRotate);
            
            
            if (this.transform.rotation.z >= .7 || this.transform.rotation.z <= -.7)
            {
         
                wayToRotate *= -1;
            }

            
            yield return new WaitForSeconds(0.01f);
        }
    }
}
