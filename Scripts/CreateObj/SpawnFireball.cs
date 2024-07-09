using System.Collections.Generic;
using UnityEngine;

public class SpawnFireball : Spawn
{
    protected static SpawnFireball instance;
    public static SpawnFireball Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null && instance != this)
        {
            Debug.LogWarning("Only 1 SpawnFireball is allowed to exist.");
            Destroy(gameObject);
            return;
        }
        instance = this;
    }
}
