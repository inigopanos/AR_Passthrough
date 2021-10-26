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
    public float contador;
    public bool nextStep;

    private void Start()
    {
        movSetPos = this.GetComponent<Movimiento>();
        disparoDron = this.GetComponent<DisparoDron>();
    }

    void Update()
    {
        if (transform.position != newPos)
        {
            Desplazamiento();
        }

        FinPaso2();
    }

    public void Desplazamiento()
    {
        transform.position = Vector3.MoveTowards(this.transform.position, newPos, speed * Time.deltaTime);
        Debug.Log("Moviendose a " + newPos);

        if(transform.position == newPos)
        {
            nextStep = true;
        }
    }

    public void FinPaso2()
    {
        if (nextStep)
        {
            contador += Time.deltaTime;

            if (contador >= 1.4)
            {
                disparoDron.ChanceDisparo();
                contador = 0f;
                nextStep = false;

                Debug.LogWarning("Paso 2 completado");
            }
        }
    }
}