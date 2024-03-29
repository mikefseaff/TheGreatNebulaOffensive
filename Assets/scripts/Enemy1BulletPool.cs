﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy1BulletPool : MonoBehaviour
{
    public static Enemy1BulletPool SharedInstance;
    public List<GameObject> pooledBullets;
    public GameObject bulletToPool;
    public int amountToPool = 20;

    private void Awake()
    {
        SharedInstance = this;
        pooledBullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(bulletToPool);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
        }
    }

    private void Start()
    {
        
    }
    
    public GameObject GetPooledBullet()
    {
        for(int i = 0; i < amountToPool; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }
}
