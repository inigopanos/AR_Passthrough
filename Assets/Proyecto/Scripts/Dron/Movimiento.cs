using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movimiento : MonoBehaviour
{
    [Header ("Floats e ints")]
    public float movSpeed; //Espacio que se mueve
    public float espacioMover;
    public float tiempoEspera;
    public float contador;
    public float contSpeed;

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
    Vector3 radioPlayer;

    [Header ("Vectores")]
    Vector3 newPos;
    Vector3 frente;

    [Header("Disparo")]
    public DisparoDron disparoDron;
    private AudioSource audioSource;
    public GameObject player;

    private void Start() //Al llegar el contador al segundo 3 palma Unity
    {
        audioSource = this.GetComponent<AudioSource>();
        disparoDron = this.GetComponent<DisparoDron>();
        //StartCoroutine(movimientoDron()); //Crashea

        //MovimientoDron();
    }

    //Cambiado movimiento dron del update al start y al final de la corutina para que no se esté venga llamar

    void Update()
    {
        contador += Time.deltaTime;
        if(contador > tiempoEspera) //Movido de MovimientoDron() a aquí
        {
            MovimientoDron();
        }
        
        MirarJugador();
    }
    
    void MirarJugador()
    {
        transform.LookAt(player.transform.position);
    }

    IEnumerator movimientoDronCorutina() //No se llama bien
    {
        contador = 0;
        print("New pos 2 = " + newPos);
        while (contador < tiempoEspera)
        {
            transform.position = Vector3.MoveTowards(this.transform.position, newPos, movSpeed * Time.deltaTime);
            //transform.position = Vector3.Lerp(transform.position, newPos, 0.1f);

            int chanceDisparo = Random.Range(1, 4);

            if (chanceDisparo >= 2)
            {
                Debug.Log("Dispara");
                disparoDron.Disparo();
                print("Se llama la corutina");
                StartCoroutine(movimientoDronCorutina());
            }
            else
            {
                Debug.Log("No dispara");
                contador = 0;
            }
            print("Se está moviendo con el lerp"); //Se llama constantemente
            yield return null;
        }
        MovimientoDron();       
    }

    void MovimientoDron()
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

        //transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movSpeed); //Teleport, me gustaria hacerlo mover rapido
        //print("Se llama al Vector3.Lerp");

        //transform.position = Vector3.MoveTowards(transform.position, newPos, movSpeed * Time.deltaTime);
        //print("Se mueve a " + newPos);

        NoAlejarseMucho();
        NoAcercarseDemasiado();

        

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