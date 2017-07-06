using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class ObjectPooler
{
    public static ObjectPooler Instance;
    private List<GameObject> objectPool;
    private GameObject prefab;
    private int size;

    public ObjectPooler(GameObject _prefab, int _size)
    {
        if(Instance != null)
        {
            return;
        }

        Instance = this;
        objectPool = new List<GameObject>();
        prefab = _prefab;
        size = _size;

        GrowPool();
    }

    private void GrowPool()
    {
        for (int i = 0; i < size; i++)
        {
            GameObject temp = GameObject.Instantiate(prefab);
            temp.SetActive(false);
            objectPool.Add(temp);
        }
    }

    public GameObject GetObjectFromPool()
    {
        for (int i = 0; i < objectPool.Count; i++)
        {
            if(!objectPool[i].activeInHierarchy)
            {
                return objectPool[i];
            }
        }

        GrowPool();

        return GetObjectFromPool();
    }

    public void ReturnObjectToPool(GameObject _activeObject)
    {
        _activeObject.SetActive(false);
    }
}
