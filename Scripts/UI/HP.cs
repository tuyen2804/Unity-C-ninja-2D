using System;
using System.Collections;
using System.Collections.Generic;
using System.Threading;
using UnityEngine;
using UnityEngine.UI;

public class HP : MonoBehaviour
{
    public float hp = 1f;
    public Image hpImage;
    public Rigidbody2D rb;
    public Collider2D col;
    public GameObject pauseGame;
    protected static HP instance;
    public static HP Instance => instance;

    protected void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Only 1 HP is allowed to exist.");
            return;
        }
        instance = this;
        rb = GetComponent<Rigidbody2D>();
        col = GetComponent<Collider2D>();
        if (rb == null)
        {
            Debug.LogError("Rigidbody2D component is missing on the player object.");
        }
        if (col == null)
        {
            Debug.LogError("Collider2D component is missing on the player object.");
        }
    }
    public void ControllHp(float damage)
    {
        hp -= damage;
        hpImage.fillAmount = hp;

        if (hp <= 0)
        {
            GameOver();
        }
    }

    private void GameOver()
    {
        // Dừng thời gian trong game
        Time.timeScale = 0;

        // Hiển thị màn hình tạm dừng hoặc kết thúc game
        if (pauseGame != null)
        {
            pauseGame.SetActive(true);
        }
    }

}
