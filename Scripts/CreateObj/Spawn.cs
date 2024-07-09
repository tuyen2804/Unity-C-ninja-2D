using System.Collections.Generic;
using UnityEngine;

public class Spawn : MonoBehaviour
{
    [SerializeField] private List<Transform> prefabs = new List<Transform>();
    [SerializeField] private Transform holder;
    [SerializeField] private List<Transform> poolObject = new List<Transform>();

    protected virtual void Awake()
    {
        LoadComponents();
        CleanUpPool();
    }

    protected  void LoadComponents()
    {
        LoadPrefabs();
        LoadHolder();
    }

    protected virtual void LoadPrefabs()
    {
        Transform prefabsParent = transform.Find("Prefabs");
        if (prefabsParent == null) return;

        foreach (Transform child in prefabsParent)
        {
            child.gameObject.SetActive(false);
            prefabs.Add(child);
        }
    }

    protected virtual void LoadHolder()
    {
        if (holder == null)
        {
            holder = transform.Find("Holder");
        }
    }

    public virtual Transform SpawnObject(string nameobj, Vector3 position)
    {
        Transform prefab = GetPrefabByName(nameobj);
        if (prefab == null) return null;

        Transform obj = GetObjectFromPool(prefab);
        if (obj == null) return null;

        obj.gameObject.SetActive(true);
        obj.SetParent(holder);
        obj.position = position;
        return obj;
    }

    public virtual void Despawn(Transform obj)
    {
        if (obj == null) return;
        obj.gameObject.SetActive(false);
        poolObject.Add(obj);
    }

    private Transform GetObjectFromPool(Transform prefab)
    {
        for (int i = 0; i < poolObject.Count; i++)
        {
            if (poolObject[i] == null)
            {
                poolObject.RemoveAt(i);
                i--;
                continue;
            }

            if (poolObject[i].name == prefab.name)
            {
                Transform pooledObj = poolObject[i];
                poolObject.RemoveAt(i);
                return pooledObj;
            }
        }

        Transform newObj = Instantiate(prefab);
        newObj.name = prefab.name;
        return newObj;
    }

    private Transform GetPrefabByName(string name)
    {
        foreach (Transform prefab in prefabs)
        {
            if (prefab != null && prefab.name == name) return prefab;
        }
        return null;
    }

    private void CleanUpPool()
    {
        poolObject.RemoveAll(item => item == null);
    }
}
