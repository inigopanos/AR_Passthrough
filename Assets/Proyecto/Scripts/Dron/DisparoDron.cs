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

    public void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
    }

    public void Disparo() //Quiero que espere un segundo antes de disparar. Dispara varios laseres a la vez �?
    {
        frente = transform.TransformDirection(Vector3.forward) * 10;
        Debug.DrawRay(transform.position, frente, Color.green);

        Instantiate(laser, transform.position, Quaternion.LookRotation(frente));
        audioSource.PlayOneShot(_blasterSFX, 0.5f);
    }
}
