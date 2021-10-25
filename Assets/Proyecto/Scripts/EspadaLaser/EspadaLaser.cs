using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaLaser : MonoBehaviour
{
    public Transform manoDer;
    private AudioSource audioSource;
    private GameObject manager;
    public AudioClip[] laserClips;

    void Start()
    {
        //this.gameObject.transform.parent = manoDer.transform;
        audioSource = this.GetComponent<AudioSource>();
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Laser"))
        {
            Debug.Log("Ha destruido " + col.gameObject.name);

            audioSource.clip = laserClips[Random.Range(0, laserClips.Length)];

            manager.GetComponent<ScoreManager>().SumarPuntos(1);

            Destroy(col.gameObject);
        }
    }
}