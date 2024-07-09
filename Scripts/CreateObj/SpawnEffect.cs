using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnEffect : Spawn
{
    protected static SpawnEffect instance;
    public static SpawnEffect Instance => instance;

    protected override void Awake()
    {
        base.Awake();
        if (instance != null)
        {
            Debug.Log("Only 1 SpawnEffect is allowed to exist.");
            return;
        }
        instance = this;
    }
}
