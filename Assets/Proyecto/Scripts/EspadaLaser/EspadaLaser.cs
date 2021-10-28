using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EspadaLaser : MonoBehaviour
{
    [Header("Publicos")]
    public Transform manoDer;
    public AudioClip[] laserClips;

    [Header("Elementos")]
    [SerializeField]
    private AudioSource audioSource;
    [SerializeField]
    private GameObject manager;
    

    void Start()
    {
        audioSource = this.GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Laser"))
        {
            Debug.LogWarning("Ha destruido " + col.gameObject.name);

            audioSource.clip = laserClips[Random.Range(0, laserClips.Length)];
            audioSource.Play();

            manager.GetComponent<ScoreManager>().SumarPuntos(1);

            Destroy(col.gameObject);
        }
        else
        {
            Debug.LogWarning("Ha destruido aunque no debiera " + col.gameObject.name);

            audioSource.clip = laserClips[Random.Range(0, laserClips.Length)];
            audioSource.Play();
        }
    }
}