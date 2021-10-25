using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Cannon : MonoBehaviour
{
    public GameObject player;

    void Start()
    {
        
    }

    void Update()
    {
        this.transform.LookAt(player.transform.position);

        Vector3 forward = transform.TransformDirection(Vector3.forward) * 10;
        //Debug.DrawRay(transform.position, forward, Color.green);
    }

    //private void OnDrawGizmos()
    //{
    //    Debug.DrawRay(this.transform.position, Vector3.forward, Color.blue);
    //}
}
