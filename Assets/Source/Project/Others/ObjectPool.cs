using System;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    public static ObjectPool Instance { get; private set; }

    private Dictionary<string, Queue<GameObject>> pools;  

    [Serializable]
    public struct InitialPool
    {
        public GameObject prefab;
        public int total;
    }

    public List<InitialPool> initialPools;

    void Awake()
    {
        if (Instance)
            Destroy(gameObject);
        else
            Instance = this;
    }

    void Start()
    {
        StartPool();
    }

    private void StartPool()
    {
        pools = new Dictionary<string, Queue<GameObject>>();

        foreach (InitialPool initialPool in initialPools)
        {
            Queue<GameObject> objects = new Queue<GameObject>();
            for (int i = 0; i < initialPool.total; i++)
            {
                var obj = Instantiate(initialPool.prefab, transform);
                obj.name = initialPool.prefab.name;
                obj.SetActive(false);
                objects.Enqueue(obj);
            }
            pools.Add(initialPool.prefab.name, objects);
        }
    }

    public GameObject GetObject(GameObject prefab)
    {
        GameObject obj = null;
        if (pools.ContainsKey(prefab.name))
        {
            if (pools[prefab.name].Count > 0)
            {
                obj = pools[prefab.name].Dequeue();
            }
            else
            {
                obj = Instantiate(prefab, transform);
                obj.name = prefab.name;
            }
                
        }
        else
        {
            pools.Add(prefab.name, new Queue<GameObject>());
            obj = Instantiate(prefab, transform);
            obj.name = prefab.name;
        }

        obj.SetActive(true);
        return obj;
    }

    public void ReturnObject(GameObject obj)
    {

        if (pools.ContainsKey(obj.name))
        {
            obj.SetActive(false);
            pools[obj.name].Enqueue(obj);
        }
            
    }
}
