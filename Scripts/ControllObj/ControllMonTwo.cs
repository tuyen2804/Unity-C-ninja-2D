using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMonTwo : MonoBehaviour
{
    public Animator animator;
    public float damage = 0.1f;
    protected static ControllMonTwo instance;
    public static ControllMonTwo Instance => instance;

    public bool hasAttacked = false; // Cờ để theo dõi xem quái đã tấn công chưa

    protected void Awake()
    {
        animator = GetComponent<Animator>();
        if (animator == null)
        {
            Debug.Log("Animator component is missing on the player object.");
        }
        if (instance != null)
        {
            Debug.Log("Only 1 ControllMonTwo is allowed to exist.");
            return;
        }
        instance = this;
    }

    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("monster_2Attack");
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (!hasAttacked && collision.CompareTag("Player"))
        {
            hasAttacked = true; // Đánh dấu rằng quái đã tấn công
            Attack();
            HP hp = collision.GetComponentInChildren<HP>();
            if (hp != null)
            {
                hp.ControllHp(damage);
            }
        }
    }
}
