using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    private float speed = 3.5f; // Tốc độ di chuyển của nhân vật
    private float jumpForce = 6.5f; // Lực nhảy của nhân vật
    private int jumpCount = 0; // Đếm số lần nhảy
    private int maxJumpCount = 2; // Số lần nhảy tối đa
    private float skillDistance = 5f; // Khoảng cách dịch chuyển khi sử dụng kỹ năng
    public Rigidbody2D rb; // Thành phần Rigidbody2D
    public Animator animator; // Thành phần Animator
    public Collider2D playerCollider; // Thành phần Collider2D
    private float manaFire1 = 0.1f;
    private float manaFire2 = 0.3f;
    private bool isAttacking = false; // Biến cờ để kiểm tra trạng thái tấn công
    private float zKeyHoldTime = 0f; // Timer to track how long the Z key is held down
    private float zKeyHoldDuration = 3f; // Duration in seconds to hold the Z key to trigger SpawnFire2
    public Transform effectSkill;
    private float mKeyHoldTime = 0f; // Timer to track how long the mouse key is held down
    private float mKeyHoldDuration = 3f; // Duration in seconds to hold the mouse key to trigger SpawnFire3

    void Awake()
    {
        // Lấy các thành phần cần thiết
        rb = GetComponent<Rigidbody2D>();
        animator = GetComponent<Animator>();
        playerCollider = GetComponent<Collider2D>();

        // Kiểm tra xem Rigidbody2D và Animator có tồn tại không
        if (rb == null)
        {
            Debug.Log("Rigidbody2D component is missing on the player object.");
        }
        if (animator == null)
        {
            Debug.Log("Animator component is missing on the player object.");
        }
        if (playerCollider == null)
        {
            Debug.Log("Collider2D component is missing on the player object.");
        }
    }
    void Update()
    {
        // Di chuyển nhân vật theo trục x với tốc độ cố định
        transform.parent.Translate(Vector3.right * speed * Time.deltaTime);

        // Kiểm tra nếu nhấn phím lên thì nhảy
        if (Input.GetKeyDown(KeyCode.UpArrow) && jumpCount < maxJumpCount)
        {
            Jump();
        }


        // Kiểm tra nếu nhấn phím cách thì sử dụng kỹ năng
        if (Input.GetKeyDown(KeyCode.Space))
        {
            UseSkill();
        }
        // Kiểm tra giữ chuột
        if (Input.GetMouseButton(0))
        {
            mKeyHoldTime += Time.deltaTime;
            if (mKeyHoldTime >= mKeyHoldDuration)
            {
                effectSkill.gameObject.SetActive(true);
            }

        }

        // Kiểm tra nếu thả chuột
        if (Input.GetMouseButtonUp(0))
        {
            if (mKeyHoldTime < mKeyHoldDuration)
            {
                Attack();
            }
            else
            {
                effectSkill.gameObject.SetActive(false);
                SpawnFire3();
            }
            mKeyHoldTime = 0f; // Reset the timer after releasing the key
        }

        // Kiểm tra nếu giữ phím Z
        if (Input.GetKey(KeyCode.Z))
        {
            zKeyHoldTime += Time.deltaTime;
            if (zKeyHoldTime >= zKeyHoldDuration)
            {
                effectSkill.gameObject.SetActive(true);
            }

        }

        // Kiểm tra nếu thả phím Z
        if (Input.GetKeyUp(KeyCode.Z))
        {
            if (zKeyHoldTime < zKeyHoldDuration)
            {
                SpawnFire();
            }
            else
            {
                effectSkill.gameObject.SetActive(false);
                SpawnFire2();
            }
            zKeyHoldTime = 0f; // Reset the timer after releasing the key
        }
    }


    void Jump()
    {
        if (rb != null)
        {
            rb.velocity = new Vector2(rb.velocity.x, jumpForce);
            jumpCount++;
            if (animator != null)
            {
                animator.SetTrigger("ninjaJump");
            }
        }
    }
    void SpawnFire3()
    {
        if (animator != null)
        {
            animator.SetTrigger("ninjaAttack");
            isAttacking = true;
            Mana mn = GetComponent<Mana>();
            if (mn != null)
            {
                if (mn.mana >= 0.3f)
                {
                    SpawnFireball.Instance.SpawnObject("Fireball_5", transform.position);
                    mn.ControllMana(manaFire2);
                    CheckForMonsterCollision();
                    StartCoroutine(AttackDuration(animator.GetCurrentAnimatorStateInfo(0).length));
                }
            }
        }

    }
    void SpawnFire2()
    {
        if (animator != null)
        {
            animator.SetTrigger("ninjaAttack");
            isAttacking = true;
            Mana mn = GetComponent<Mana>();
            if (mn != null)
            {
                if (mn.mana >= 0.3f)
                {
                    SpawnFireball.Instance.SpawnObject("Fireball_3", transform.position);
                    mn.ControllMana(manaFire2);
                    CheckForMonsterCollision();
                    StartCoroutine(AttackDuration(animator.GetCurrentAnimatorStateInfo(0).length));
                }
            }
        }

    }
    void SpawnFire()
    {
        if (animator != null)
        {
            animator.SetTrigger("ninjaAttack");
            isAttacking = true;
            Mana mn = GetComponent<Mana>();
            if (mn != null)
            {
                if (mn.mana >= 0.1f)
                {
                    SpawnFireball.Instance.SpawnObject("Fireball_1", transform.position);
                    mn.ControllMana(manaFire1);
                    CheckForMonsterCollision();
                    StartCoroutine(AttackDuration(animator.GetCurrentAnimatorStateInfo(0).length));
                }    
            }
        }
    
    }
    void Attack()
    {
        if (animator != null)
        {
            animator.SetTrigger("ninjaAttack");
            isAttacking = true;
            CheckForMonsterCollision();
            StartCoroutine(AttackDuration(animator.GetCurrentAnimatorStateInfo(0).length));
        }
    }

    IEnumerator AttackDuration(float duration)
    {
        yield return new WaitForSeconds(duration);
        isAttacking = false;
    }

    void UseSkill()
    {
        if (animator != null)
        {
            animator.SetTrigger("ninjaSkill");
        }

        // Dịch chuyển nhân vật
        Vector3 newPosition = transform.parent.position + Vector3.right * skillDistance;
        transform.parent.position = newPosition;

        // Check for collision with monsters after skill movement
        CheckForMonsterCollision();
    }

    void CheckForMonsterCollision()
    {
        // Kiểm tra va chạm với quái vật
        Collider2D[] colliders = Physics2D.OverlapBoxAll(transform.position, playerCollider.bounds.size, 0f);
        foreach (Collider2D collider in colliders)
        {
            if (collider.CompareTag("Monster") && isAttacking)
            {
                DeleteMonster deleteMonster = collider.GetComponentInChildren<DeleteMonster>();
                if (deleteMonster != null)
                {
                    deleteMonster.DeleteObj();
                }
            }
        }
    }

    // Kiểm tra va chạm để xác định nhân vật đã tiếp đất
    void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Ground"))
        {
            jumpCount = 0; // Reset số lần nhảy khi nhân vật tiếp đất
        }
    }

    // Kiểm tra va chạm để xóa quái vật khi tấn công
    void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Monster") && isAttacking)
        {
            DeleteMonster deleteMonster = other.GetComponentInChildren<DeleteMonster>();
            if (deleteMonster != null)
            {
                deleteMonster.DeleteObj();
            }
        }
    }
}
