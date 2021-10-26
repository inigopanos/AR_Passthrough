using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header ("Floats e ints")]
    public float movSpeed; //Espacio que se mueve
    public float espacioMover;

    [Header("Min y Max X")]
    public float minX;
    public float maxX;

    [Header("Min y Max Z")]
    public float minZ;
    public float maxZ;

    [Header("Min y Max Y")]
    public int minY;
    public int maxY;

    [Header("Limite esfera")]
    public float radio;

    [Header ("Vectores")]
    Vector3 newPos;

    [Header("Disparo")]
    public DisparoDron disparoDron;
    private AudioSource audioSource;
    public GameObject player;

    public DronMovement dronMov;

    private void Start() //Al llegar el contador al segundo 3 palma Unity
    {
        audioSource = this.GetComponent<AudioSource>();
        disparoDron = this.GetComponent<DisparoDron>();

        NewPosSet();
    }

    void Update()
    {
        MirarJugador();
        //NoAlejarseMucho();
        //NoAcercarseDemasiado();
    }

    void MirarJugador()
    {
        transform.LookAt(player.transform.position);
    }

    public void NewPosSet()
    {
        var randX = Random.Range(minX, maxX);
        var randY = Random.Range(minY, maxY);
        var randZ = Random.Range(minZ, maxZ);

        newPos = new Vector3(randX * espacioMover, randY * espacioMover, randZ * espacioMover);

        Vector3 centro = player.transform.position;
        float distancia = Vector3.Distance(newPos, centro);

        if (distancia > radio) //Se encarga de recolocar al dron dentro de una esfera creada alrededor del jugador
        {
            Vector3 deOrigenAObjeto = newPos - centro;
            deOrigenAObjeto *= radio / distancia;
            newPos = centro + deOrigenAObjeto;
            print("New pos 1 = " + newPos);
        }

        dronMov.newPos = newPos;

        Debug.Log("dronMov.newPos = newPos" + newPos);
        Debug.LogWarning("Paso 1 completado");
    }

    private void OnDrawGizmos()
    {
        Vector3 centro = player.transform.position;

        Gizmos.color = Color.yellow;
        Gizmos.DrawWireCube(centro, player.transform.position * radio);
    }

    void NoAlejarseMucho()
    {
        Vector3 centro = player.transform.position;
        float distancia = Vector3.Distance(transform.position, centro);

        if (distancia > radio)
        {
            Vector3 deOrigenAObjeto = transform.position - centro;
            deOrigenAObjeto *= radio / distancia;
            transform.position = centro + deOrigenAObjeto;
        }
    }

    void NoAcercarseDemasiado()
    {
        Vector3 centro = player.transform.position;
        float distanciaCentro = Vector3.Distance(transform.position, centro) / 2;

        if (distanciaCentro < radio)
        {
            Vector3 deOrigenAObjeto2 = transform.position - centro;
            deOrigenAObjeto2 *= radio / distanciaCentro;
            transform.position = centro + deOrigenAObjeto2;
        }
    }
}