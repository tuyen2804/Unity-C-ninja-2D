using UnityEngine;

public class SpawnMonster : Spawn
{
    protected static SpawnMonster instance;
    public static SpawnMonster Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.Log("Only 1 SpawnMonster is allowed to exist.");
            return;
        }
        instance = this;
    }
}
