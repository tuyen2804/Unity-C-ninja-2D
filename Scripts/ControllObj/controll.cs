using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{
    public Transform player; // Đối tượng mà camera sẽ theo dõi
    public Vector3 offset = new Vector3(0, 0, -10); // Khoảng cách giữa camera và nhân vật

    void LateUpdate()
    {
        if (player != null)
        {
            // Cập nhật vị trí của camera dựa trên vị trí của nhân vật và offset
            Vector3 newPosition = player.position + offset;
            newPosition.z = transform.position.z; // Giữ nguyên vị trí trục Z để không làm thay đổi độ sâu của camera
            transform.position = newPosition;
        }
    }
}
