using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllFireball : MonoBehaviour
{
    public float speed = 15f;

    // Update is called once per frame
    void Update()
    {
        transform.parent.Translate(Vector3.right * speed*Time.deltaTime);

    }
}
