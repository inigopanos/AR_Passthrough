using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DronMovement : MonoBehaviour
{
    [Header("Datos")]
    public Vector3 newPos;
    public int speed;
    public Movimiento movSetPos;
    public DisparoDron disparoDron;

    private void Start()
    {
        disparoDron = this.GetComponent<DisparoDron>();
    }

    void Update()
    {
        if (transform.position != newPos)
        {
            Desplazamiento();
        }
    }

    public void Desplazamiento()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, newPos, speed * Time.deltaTime);
        Debug.Log("Moviendose a " + newPos);

        if(transform.position == newPos)
        {
            disparoDron.ChanceDisparo();

            Debug.LogWarning("Paso 2 completado");
        }
    }
}
