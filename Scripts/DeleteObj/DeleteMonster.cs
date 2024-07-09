using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class DeleteMonster : MonoBehaviour
{
    public float distance;
    public float maxDistance=20f;
    // Start is called before the first frame update
    protected static DeleteMonster instance;
    public static DeleteMonster Instance => instance;

    protected void Awake()
    {
        if (instance != null)
        {
            Debug.Log("Only 1 DeleteMonster is allowed to exist.");
            return;
        }
        instance = this;
    }
    private void FixedUpdate()
    {
        distance = Vector3.Distance(transform.parent.position, Camera.main.transform.position);
        if(distance > maxDistance) 
        { 
            distance = 0f;
            this.DeleteObj(); 
        }
    }
    public void DeleteObj()
    {
        ControllMonTwo ctr=GetComponent<ControllMonTwo>();
        if(ctr != null)
        {
            ctr.hasAttacked = false;
        }

       SpawnMonster.Instance.Despawn(transform.parent);
    }
}
