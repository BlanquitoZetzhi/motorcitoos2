using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MergeObject : MonoBehaviour
{
    public int level;
    public string nombreObjeto; // ← Este es clave
    private bool hasMerged = false;
    private AudioSource audioSource;
    private MergeManager mergeManager;
    private puntitos scoreManager;
    public GameObject explosionPrefab;

    void Start()
    {
        mergeManager = FindObjectOfType<MergeManager>();
        scoreManager = FindObjectOfType<puntitos>();
        audioSource = GetComponent<AudioSource>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged) return;

        MergeObject other = collision.gameObject.GetComponent<MergeObject>();
        if (other == null || other.level != this.level || other.hasMerged) return;

        // Solo uno ejecuta la fusión
        if (this.GetInstanceID() < other.GetInstanceID())
        {
            MergeWith(other);
           
        }
    }

    private void MergeWith(MergeObject other)
    {
        hasMerged = true;
        other.hasMerged = true;

        Destroy(gameObject);
        Destroy(other.gameObject);

        Vector3 spawnPosition = (transform.position + other.transform.position) / 2f;

        if (scoreManager != null)
        {
            float puntos = 10 * (level + 1);
            scoreManager.AgregarPuntos(puntos);
        }

        if (level < mergeManager.prefabs.Length - 1)
        {
            GameObject nuevoObjeto = mergeManager.SpawnMerged(level + 1, spawnPosition);

            if (nuevoObjeto != null)
            {
                MergeObject mergeScript = nuevoObjeto.GetComponent<MergeObject>();
                if (mergeScript != null)
                {
                    FindObjectOfType<GameProgressManager>().ReportarFusion(mergeScript.nombreObjeto);

                    audioSource.Play();


                    if (mergeScript.level == mergeManager.prefabs.Length - 1 && explosionPrefab != null)
                    {
                        GameObject explosion = Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);
                        Destroy(explosion, 1f);
                    }
                }
            }
        }
        else
        {
            CurrencyManager.Instance.AddToken(1);
        }
    }
}