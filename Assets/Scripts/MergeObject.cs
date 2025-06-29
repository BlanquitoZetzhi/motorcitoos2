using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MergeObject : MonoBehaviour
{
    public int level;
    private bool hasMerged = false;
    private MergeManager mergeManager;
    private puntitos scoreManager;
    public GameObject explosionPrefab;

    void Start()
    {
        mergeManager = FindObjectOfType<MergeManager>();
        scoreManager = FindObjectOfType<puntitos>();
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (hasMerged) return;

        MergeObject other = collision.gameObject.GetComponent<MergeObject>();
        if (other == null || other.level != this.level || other.hasMerged) return;

        // Para evitar que ambos ejecuten fusión, solo el de menor ID lo hace
        if (this.GetInstanceID() < other.GetInstanceID())
        {
            MergeWith(other);
        }
    }

    private void MergeWith(MergeObject other)
    {
        hasMerged = true;
        other.hasMerged = true;

        Vector3 spawnPosition = (transform.position + other.transform.position) / 2f;

        if (scoreManager != null)
        {
            float puntos = 10 * (level + 1); // nivel del objeto resultante
            scoreManager.AgregarPuntos(puntos);
        }

        // Si NO es el último nivel
        if (level < mergeManager.prefabs.Length - 1)
        {
            mergeManager.SpawnMerged(level + 1, spawnPosition);

            // Verificamos si el nuevo objeto será el último nivel
            if (level + 1 == mergeManager.prefabs.Length - 1)
            {
                if (explosionPrefab != null)
                {
                    GameObject explosion = Instantiate(explosionPrefab, spawnPosition, Quaternion.identity);
                    Destroy(explosion, 1f);
                }
            }

            Destroy(other.gameObject);
        }
        else
        {
            // Si este objeto ya era el último nivel (por seguridad)
            CurrencyManager.Instance.AddToken(1);
        }

        Destroy(gameObject);
    }
}