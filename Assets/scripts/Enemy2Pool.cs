﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy2Pool : MonoBehaviour
{
    public static Enemy2Pool SharedInstance;
    public List<GameObject> pooledEnemies;
    public GameObject enimesToPool;
    public int amountToPool = 10;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledEnemies = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(enimesToPool);
            tmp.SetActive(false);
            pooledEnemies.Add(tmp);
        }
    }

    public GameObject GetPooledEnemy()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledEnemies[i].activeInHierarchy)
            {
                return pooledEnemies[i];
            }
        }
        return null;
    }
}
