using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeleteFireThree : MonoBehaviour
{
    [SerializeField] public float distance;
    [SerializeField] public float speedDelete = 30f;
    protected static DeleteFireThree instance;
    public static DeleteFireThree Instance => instance;

    protected void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Only 1 DeleteFireThree is allowed to exist.");
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
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster"))
        {
            DeleteMonster deleteMonster = other.GetComponentInChildren<DeleteMonster>();
            if (deleteMonster != null)
            {
                deleteMonster.DeleteObj();
            }
        }
    }
}
