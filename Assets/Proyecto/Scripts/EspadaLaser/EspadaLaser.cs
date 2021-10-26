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
        //this.gameObject.transform.parent = manoDer.transform;
        audioSource = this.GetComponent<AudioSource>();
        manager = GameObject.FindGameObjectWithTag("Manager");
    }

    private void OnCollisionEnter(Collision col)
    {
        if (col.gameObject.CompareTag("Laser"))
        {
            Debug.Log("Ha destruido " + col.gameObject.name);

            audioSource.clip = laserClips[Random.Range(0, laserClips.Length)];
            audioSource.Play();

            manager.GetComponent<ScoreManager>().SumarPuntos(1);

            Destroy(col.gameObject);
        }
    }
}