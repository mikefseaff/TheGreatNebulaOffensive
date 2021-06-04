using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CheeseController : MonoBehaviour
{
    public GameObject Laser;
    public GameObject Laser_FadeOut;
    public GameObject Laser_ChargeUp;
    private float timer;
    public bool canFire;
    void Start()
    {
        canFire = true;
    }

    // Update is called once per frame
    //private void OnEnable()
    //{
    //    CheeseManager.laser += startAntiCheese;
    //}

    //private void OnDisable()
    //{
    //    CheeseManager.laser -= startAntiCheese;
    //}

    public void startAntiCheese()
    {
        if(canFire)
        StartCoroutine("AntiCheese");
    }
    IEnumerator AntiCheese()
    {
        canFire = false;
        Laser_ChargeUp.SetActive(true);
        yield return new WaitForSeconds(2f);
        Laser_ChargeUp.SetActive(false);
        Laser.SetActive(true);
        yield return new WaitForSeconds(2f);
        Laser.SetActive(false);
        Laser_FadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        Laser_FadeOut.SetActive(false);
        canFire = true;
    }
        
}
