using UnityEngine;
using System.Collections.Generic;
using System.Collections;
public class PlayerPoolingRev : MonoBehaviour
{
    // Start is called once before the first execution of Update after the MonoBehaviour is created
   [SerializeField] List<GameObject> poolBullet;
    [SerializeField] GameObject BulletPrefab;

    [SerializeField] int amount;
    void Start()
    {
        StartPoolBullet();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void StartPoolBullet()
    {
        for(int i = 0; i< amount; i++)
        {
            var obj = Instantiate(BulletPrefab,transform);
            obj.SetActive(false);
            poolBullet.Add(obj);
        }
    }

    public GameObject GetBulletPool()
    {
        for(int i =0; i< amount; i++)
        {
            if (!poolBullet[i].activeInHierarchy)
            {
                return poolBullet[i];   
            }
        }

        return null;
    }
}
