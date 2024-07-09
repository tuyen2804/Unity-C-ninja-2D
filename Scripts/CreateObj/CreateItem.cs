using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CreateItem : MonoBehaviour
{
    [SerializeField] private List<Transform> pointSpawn = new List<Transform>();
    [SerializeField] private float timeSpawn = 12f;
    [SerializeField] private float timeCount = 0f;
    private static CreateItem instance;
    public static CreateItem Instance => instance;

    private void Awake()
    {
        LoadComponents();
        if (instance == null)
        {
            instance = this;
        }
    }

    private void LoadComponents()
    {
        foreach (Transform t in transform)
        {
            pointSpawn.Add(t);
        }
    }

    private void FixedUpdate()
    {
        timeCount += Time.fixedDeltaTime;
        if (timeCount > timeSpawn)
        {
            timeCount = 0f;
            Spawnitem();
        }
    }
    public string GetRandomItem()
    {
        string[] items = { "Item_1", "Item_2", "Item_3" };
        int randomIndex = UnityEngine.Random.Range(0, items.Length);
        return items[randomIndex];
    }

    private Transform Spawnitem()
    {
        if (pointSpawn.Count == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return null;
        }

        int randomIndex = Random.Range(0, pointSpawn.Count);
        string itemName = GetRandomItem();

        Transform item = SpawnItem.Instance.SpawnObject(itemName, pointSpawn[randomIndex].position);
        if (item != null)
        {
            item.gameObject.SetActive(true);
        }
        return item;
    }
}
