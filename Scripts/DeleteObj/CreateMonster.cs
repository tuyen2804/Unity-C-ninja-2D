using System.Collections.Generic;
using UnityEngine;

public class CreateMonster : MonoBehaviour
{
    [SerializeField] private List<Transform> pointSpawn = new List<Transform>();
    [SerializeField] private float timeSpawn = 2f;
    [SerializeField] private float timeCount = 0f;
    [SerializeField] private float timeCount1 = 0f;
    private static CreateMonster instance;
    private bool spawnBoss = true;
    public static CreateMonster Instance => instance;

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
        timeCount1 += Time.fixedDeltaTime;
        if (timeCount > timeSpawn)
        {
            timeCount = 0f;
            SpawnMon();
        }

        // Check if it's time to spawn the boss
        if (spawnBoss && timeCount1 > 15f)
        {
            spawnBoss = false;
            SpawnBoss();
        }
    }

    private void SpawnMon()
    {
        if (pointSpawn.Count == 0)
        {
            Debug.LogWarning("No spawn points available.");
            return;
        }

        int randomIndex = Random.Range(0, pointSpawn.Count);
        string monsterName = pointSpawn[randomIndex].name == "Point_2" ? "Monster_2" : "Monster_1";

        Transform monster = SpawnMonster.Instance.SpawnObject(monsterName, pointSpawn[randomIndex].position);
        if (monster != null)
        {
            monster.gameObject.SetActive(true);
        }
    }

    private void SpawnBoss()
    {
        // Find the spawn point named "Point_5"
        Transform bossSpawnPoint = pointSpawn.Find(point => point.name == "Point_5");

        if (bossSpawnPoint == null)
        {
            Debug.LogWarning("Boss spawn point 'Point_5' not found.");
            return;
        }

        // Spawn boss at 'Point_5'
        string bossName = "Boss";
        Transform boss = SpawnMonster.Instance.SpawnObject(bossName, bossSpawnPoint.position);

        if (boss != null)
        {
            boss.gameObject.SetActive(true);
        }
    }
}
