using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossAttack : MonoBehaviour
{
    public float timeSkill_1 = 0f;
    private Animator animator;
    private bool isAttacking = false;

    private void Awake()
    {
        animator = GetComponent<Animator>();
    }

    private void FixedUpdate()
    {
        timeSkill_1 += Time.fixedDeltaTime;
        if (timeSkill_1 > 3f)
        {
            timeSkill_1 = 0f;
            Attack();
        }
    }

    void Attack()
    {
        animator.SetTrigger("attack");
        isAttacking = true;
        StartCoroutine(WaitForAttackToEnd());
    }

    private IEnumerator WaitForAttackToEnd()
    {
        // Chờ cho đến khi animation tấn công kết thúc
        yield return new WaitWhile(() =>
            animator.GetCurrentAnimatorStateInfo(0).IsName("Attack") &&
            animator.GetCurrentAnimatorStateInfo(0).normalizedTime < 0.7f
        );

        // Đợi thêm 1 giây sau khi animation kết thúc
        yield return new WaitForSeconds(0.7f);

        // Sau khi animation kết thúc và đợi 1 giây, tạo lửa
        SpawnFireball.Instance.SpawnObject("Fireball_6", transform.position);
        isAttacking = false;
    }
}
