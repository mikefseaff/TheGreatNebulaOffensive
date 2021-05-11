using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SprayCannonBulletPool : MonoBehaviour
{
    public static SprayCannonBulletPool SharedInstance;
    public List<GameObject> pooledBullets;
    public GameObject bulletToPool;
    public int amountToPool = 20;

    private void Awake()
    {
        SharedInstance = this;
    }

    private void Start()
    {
        pooledBullets = new List<GameObject>();
        GameObject tmp;
        for (int i = 0; i < amountToPool; i++)
        {
            tmp = Instantiate(bulletToPool);
            tmp.SetActive(false);
            pooledBullets.Add(tmp);
        }
    }

    public GameObject GetPooledBullet()
    {
        for (int i = 0; i < amountToPool; i++)
        {
            if (!pooledBullets[i].activeInHierarchy)
            {
                return pooledBullets[i];
            }
        }
        return null;
    }
}
