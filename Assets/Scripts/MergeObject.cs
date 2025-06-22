using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class MergeObject : MonoBehaviour
{
    public int level;
    private bool hasMerged = false;
    private MergeManager mergeManager;

    void Start()
    {
        mergeManager = FindObjectOfType<MergeManager>();
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

        if (level >= mergeManager.prefabs.Length - 1)
        {
            // Máximo tier -> +1 token
            CurrencyManager.Instance.AddToken(1);
            Explode();
            Destroy(other.gameObject);
        }
        else
        {
            mergeManager.SpawnMerged(level + 1, spawnPosition);
            Destroy(other.gameObject);
        }

        Destroy(gameObject);
    }

    private void Explode()
    {
        Debug.Log("💥 ¡Zebra explotó!");
        // posible efecto
    }
}