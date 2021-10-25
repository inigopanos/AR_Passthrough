using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Laser : MonoBehaviour
{
    Rigidbody rbd;
    public float speed;
    public float contador;
    public float tiempoVida;

    void Start()
    {
        rbd = this.GetComponent<Rigidbody>();
        rbd.velocity = transform.TransformDirection(Vector3.forward * speed);
    }

    void Update()
    {
        contador += Time.deltaTime;

        if (contador >= tiempoVida)
        {
            Destroy(gameObject);
        }
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Suelo"))
        {
            Debug.Log("Laser choca contra el suelo");
            Destroy(gameObject);
        }
    }
}