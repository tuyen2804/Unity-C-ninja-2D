using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllBoss : MonoBehaviour
{
    private float speed = 3.5f; // Tốc độ di chuyển của nhân vật

    // Update is called once per frame
    void Update()
    {
        transform.parent.Translate(Vector3.right * speed * Time.deltaTime);
    }
}