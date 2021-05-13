using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LaserGunManager : MonoBehaviour
{
    
    public static LaserGunManager SharedInstance;
    public int AttackNum;

    public delegate void StartAttack();
    public static event StartAttack Attack;
    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        InvokeRepeating("AttackEvent", 0, 15);
    }

    // Update is called once per frame
    private void AttackEvent()
    {
        AttackNum = Random.Range(1, 3);
        if (Attack != null)
            Attack();
        
    }
}
