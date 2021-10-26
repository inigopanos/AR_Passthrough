using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DisparoDron : MonoBehaviour
{
    [Header("Disparo")]
    public Rigidbody laser;
    public GameObject player;
    public float speed;
    private AudioSource audioSource;
    public AudioClip _blasterSFX;
    Vector3 frente;
    public Movimiento setPos;

    public void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        setPos = this.GetComponent<Movimiento>();
    }

    public void ChanceDisparo()
    {
        int chanceDisparo = Random.Range(1, 4);

        if (chanceDisparo >= 2)
        {
            Debug.Log("Dispara");
            Disparo();
            print("Se llama la corutina");
        }
        else
        {
            Debug.Log("No dispara");
        }

        CallSetNewPos();
    }


    public void Disparo() //Quiero que espere un segundo antes de disparar. Dispara varios laseres a la vez? Cada vez que se llama instancia uno o dos más. 
    {
        frente = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, frente, Color.green);

        Instantiate(laser, transform.position, Quaternion.LookRotation(frente));

        Debug.Log("Nº laser instanciado: " + GameObject.FindGameObjectsWithTag("Laser").Length);

        audioSource.clip = _blasterSFX;
        audioSource.Play();

        CallSetNewPos();
    }

    void CallSetNewPos()
    {
        setPos.NewPosSet();
        Debug.LogWarning("Paso 3 completado");
    }
}