using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseManager : MonoBehaviour
{
    //public delegate void StartLaser();
    //public static event StartLaser laser;
    public GameObject laserObject;

 
    //public void LaserEvent()
    //{

    //    if (laser != null)
    //        laser();
    //}
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.layer == 8)
        {
            laserObject.GetComponent<CheeseController>().startAntiCheese();

        }
    }
}
