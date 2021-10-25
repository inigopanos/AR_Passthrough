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
    public Rigidbody laser;
    public GameObject player;
    public float speed;
    public float contadorDisparo;
    private AudioSource audioSource;
    public AudioClip _laserSFX;

    private void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    void Update()
    {
        MovimientoDron();
        MirarJugador();
    }
    
    void MirarJugador()
    {
        transform.LookAt(player.transform.position);
    }

    void MovimientoDron()
    {
        contador += Time.deltaTime * contSpeed;

        var randX = Random.Range(minX, maxX);
        var randY = Random.Range(minY, maxY);
        var randZ = Random.Range(minZ, maxZ);

        if (contador > tiempoEspera)
        {
            newPos = new Vector3(randX * espacioMover, randY * espacioMover, randZ * espacioMover);

            Vector3 centro = player.transform.position;
            float distancia = Vector3.Distance(newPos, centro);

            if (distancia > radio) //Se encarga de recolocar al dron dentro de una esfera creada alrededor del jugador
            {
                Vector3 deOrigenAObjeto = newPos - centro;
                deOrigenAObjeto *= radio / distancia;
                newPos = centro + deOrigenAObjeto;    
            }

            transform.position = Vector3.Lerp(transform.position, newPos, Time.deltaTime * movSpeed); //Teleport, me gustar�a hacerlo mov r�pido
            print("Se llama al Vector3.Lerp");

            NoAlejarseMucho();
            NoAcercarseDemasiado();
 
            int chanceDisparo = Random.Range(1, 4);

            if (chanceDisparo >= 2)
            {
                Debug.Log("Dispara");
                StartCoroutine(Disparo());
                contador = 0;
                return;
            }
            else
                Debug.Log("No dispara");
                contador = 0;
        }
    }

    IEnumerator Disparo() //Quiero que espere un segundo antes de disparar. Dispara varios laseres a la vez �?
    {
        yield return new WaitForSeconds(contadorDisparo);
        MirarJugador();

        frente = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, frente, Color.green);

        Instantiate(laser, transform.position, Quaternion.LookRotation(frente));
        audioSource.PlayOneShot(_laserSFX, 0.5f);
        Debug.Log("Disparando y sonando");
       
        contador = 0;
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