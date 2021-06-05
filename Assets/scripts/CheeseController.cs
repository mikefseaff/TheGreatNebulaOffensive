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
        StartCoroutine("RepeatAntiCheese");
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
        
        StartCoroutine("AntiCheese");
    }

    IEnumerator RepeatAntiCheese()
    {
        while (true)
        {
            float randTime = Random.Range(8f, 14f);
            Debug.Log(randTime);
            yield return new WaitForSeconds(randTime);
            startAntiCheese();
        }
       
    }
    IEnumerator AntiCheese()
    {
        
        Laser_ChargeUp.SetActive(true);
        yield return new WaitForSeconds(3f);
        Laser_ChargeUp.SetActive(false);
        Laser.SetActive(true);
        yield return new WaitForSeconds(1f);
        Laser.SetActive(false);
        Laser_FadeOut.SetActive(true);
        yield return new WaitForSeconds(1f);
        Laser_FadeOut.SetActive(false);
        
    }
        
}
