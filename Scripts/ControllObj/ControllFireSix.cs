using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllFireSix : MonoBehaviour
{
    public float speed = 15f;
    public float damage = 0.3f;
    
    private void Update()
    {
        transform.parent.Translate(Vector3.left*speed*Time.deltaTime);
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if ( collision.CompareTag("Player"))
        {
            HP hp = collision.GetComponentInChildren<HP>();
            if (hp != null)
            {
                hp.ControllHp(damage);
            }
        }
    }
}
