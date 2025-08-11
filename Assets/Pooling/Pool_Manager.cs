using UnityEngine;
using System.Collections.Generic;

public class Pool_Manager : MonoBehaviour
{
    [SerializeField] GameObject poolObject;
    [SerializeField] List<GameObject> poolList;

    [SerializeField] int amount;

    void Start()
    {
        poolList = new List<GameObject>();
        StartPooling();
    }

    public void StartPooling()
    {
        for (int i = 0; i < amount; i++)
        {
            var obj = Instantiate(poolObject, transform);
            obj.SetActive(false);
            poolList.Add(obj);
        }
    }

    public GameObject GetPoolObject()
    {
        for (int i = 0; i < amount; i++)
        {
            if (!poolList[i].activeInHierarchy)
            {
                return poolList[i];
            }
        }

        return null;
    }
}