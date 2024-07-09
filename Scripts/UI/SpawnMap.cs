using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMap : MonoBehaviour
{
    public Transform player;
    public float currentDis = 0f;
    public float limitedDis = 15f;
    public float respawnDis = 24f;
    public void FixedUpdate()
    {
        this.GetDistance();
        this.Spawning();
    }
    public void Spawning()
    {
        if (this.currentDis < this.limitedDis) return;
        Debug.Log("Spawning");
        Vector3 pos = transform.position;
        pos.x += this.respawnDis;
        transform.position = pos;
    }
    public void GetDistance()
    {
        this.currentDis=this.player.position.x-transform.position.x;
    }
}
