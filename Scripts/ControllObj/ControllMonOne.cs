using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ControllMonOne : MonoBehaviour
{
    public float time1=1.5f;
    public float timeAttack=3f;
    public float distance;
    public float distance2 = 16f;
    private void FixedUpdate()
    {
        distance=Vector3.Distance(transform.parent.position, Camera.main.transform.position);
        if (distance < distance2) {
            time1 += Time.fixedDeltaTime;
            if (time1 > timeAttack)
            {
                time1 = 0f;
                SpawnFireball.Instance.SpawnObject("Fireball_2", transform.parent.position);
            }
        }
        
    }
}
