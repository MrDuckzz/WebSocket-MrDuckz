using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameObjectPooling : MonoBehaviour
{
    [System.Serializable]
    public class Pool
    {
        public string type;
        public int amount;
        public GameObject prefab;
    }

    public static GameObjectPooling instance;

    public List<Pool> pooledObjects;
    public Dictionary<string, Queue<GameObject>> poolDictionary;
    // private int amountToPool = 10;
    public GameObject objTrans;

    // [SerializeField] private GameObject MonsterPrefab;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }

        CreateObject();
    }

    private void CreateObject()
    {
        poolDictionary = new Dictionary<string, Queue<GameObject>>();

        foreach (var pool in pooledObjects)
        {
            Queue<GameObject> objectPool = new Queue<GameObject>();

            for (int i = 0; i < pool.amount; i++)
            {
                GameObject obj = Instantiate(pool.prefab);
                obj.transform.SetParent(objTrans.transform);
                obj.SetActive(false);
                objectPool.Enqueue(obj);
            }

            poolDictionary.Add(pool.type, objectPool);
        }
    }

    public GameObject GetPooledObject(string name)
    {
        foreach (var pool in pooledObjects)
        {
            for (int i = 0; i < pool.amount; i++)
            {
                if (!pool.prefab.activeInHierarchy)
                {
                    return pool.prefab;
                }
            }
        }
        return null;
    }

    public GameObject SpawnFromPool(string type, Transform p = null)
    {
        Debug.Log(poolDictionary[type].Count);
        if (!poolDictionary.ContainsKey(type))
        {
            Debug.LogWarning($"Pool with tag {type} doesn't excist.");
            return null;
        }
        if (poolDictionary[type].Count == 0)
        {
            // objectToSpawn.SetActive(false);
            CreateObject();
        }
        GameObject objectToSpawn = poolDictionary[type].Dequeue();
        objectToSpawn.transform.SetParent(p);
        Debug.Log(poolDictionary.ContainsKey(type));



        objectToSpawn.SetActive(true);

        // poolDictionary[type].Enqueue(objectToSpawn);

        return objectToSpawn;
    }

    public void ReturnObject(string type, GameObject obj)
    {
        if (poolDictionary.ContainsKey(type))
        {
            obj.SetActive(false);
            obj.transform.SetParent(objTrans.transform);
            poolDictionary[type].Enqueue(obj);
        }
        else
        {
            Debug.LogWarning("Error!");
        }

    }
}


