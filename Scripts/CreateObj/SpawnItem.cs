using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnItem : Spawn
{
    protected static SpawnItem instance;
    public static SpawnItem Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.Log("Only 1 SpawnItem is allowed to exist.");
            return;
        }
        instance = this;
    }
}
