using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFireTwo : MonoBehaviour
{

    public float distance;
    public float speedDelete = 30f;
    protected static DeleteFireTwo instance;
    public static DeleteFireTwo Instance => instance;

    protected void Awake()
    {
        if (instance != null)
        {
            Debug.LogWarning("Only 1 DeleteFireTwo is allowed to exist.");
            return;
        }
        instance = this;
    }
    private void Update()
    {
        distance = Vector3.Distance(transform.parent.position, Camera.main.transform.position);
        if (distance <= speedDelete) return;
        Delete();
    }
    void Delete()
    {
        SpawnFireball.Instance.Despawn(transform.parent);
    }
}
