using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllFireTwo : MonoBehaviour
{
    public float speed = 2f; // Tốc độ của viên đạn

    private Vector3 direction;

    // Start is called before the first frame update
    void Start()
    {
        // Khởi tạo vector chỉ hướng
        float randomY = Random.Range(-1.5f, -0.1f);
        direction = new Vector3(-1, randomY, 0).normalized;
    }

    // Update is called once per frame
    void Update()
    {
        transform.parent.Translate(direction * speed * Time.deltaTime);

    }
}
