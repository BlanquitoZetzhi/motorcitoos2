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

        // 1) Sonido de fusión
        if (mergeManager != null)
            mergeManager.PlayMergeSound();

        // 2) Destruir objetos viejos
        Destroy(gameObject);
        Destroy(other.gameObject);

        // 3) Puntos
        Vector3 spawnPos = (transform.position + other.transform.position) / 2f;
        if (scoreManager != null)
        {
            float pts = 10 * (level + 1);
            scoreManager.AgregarPuntos(pts);
        }

        // 4) Spawn siguiente nivel de merge
        if (level < mergeManager.prefabs.Length - 1)
        {
            GameObject nuevo = mergeManager.SpawnMerged(level + 1, spawnPos);
            if (nuevo != null)
            {
                var ms = nuevo.GetComponent<MergeObject>();
                if (ms != null)
                {
                    FindObjectOfType<GameProgressManager>()
                        .ReportarFusion(ms.nombreObjeto);

                    if (ms.level == mergeManager.prefabs.Length - 1
                        && explosionPrefab != null)
                    {
                        var expl = Instantiate(explosionPrefab, spawnPos, Quaternion.identity);
                        Destroy(expl, 1f);
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