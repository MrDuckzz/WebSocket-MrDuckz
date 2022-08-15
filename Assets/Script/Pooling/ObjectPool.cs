using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectPool : MonoBehaviour
{
    GameObjectPooling pooler;
    public List<GameObject> gameObjs = new List<GameObject>();

    private void Start()
    {
        pooler = GameObjectPooling.instance;
    }

    public void onCick(string name)
    {
        gameObjs.Add(pooler.SpawnFromPool(name));
    }

    public void onClickReturn(string name)
    {
        if (gameObjs.Count >= 0)
        {
            GameObject obj = gameObjs[0];
            gameObjs.Remove(obj);
            pooler.ReturnObject(name, obj);
        }

    }
}
