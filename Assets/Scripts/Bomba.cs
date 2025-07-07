using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomba : MonoBehaviour
{
    [Header("Audio")]
    [SerializeField] private AudioClip explosionClip;   // Clip de explosión
    [SerializeField] private float explosionVolume = 1f;

    [Header("Visual Explosion")]
    [SerializeField] private GameObject explosionPrefab; // Prefab con SpriteRenderer
    [SerializeField] private float explosionDuration = 1f;  // Segundos antes de desaparecer
    private void OnMouseDown()
    {
        if (explosionClip != null)
            AudioSource.PlayClipAtPoint(explosionClip, transform.position, explosionVolume);

        if (explosionPrefab != null)
        {
            GameObject fx = Instantiate(explosionPrefab, transform.position, Quaternion.identity);
            Destroy(fx, explosionDuration);
        }

        //-1 hp
        if(EscudoManager.Instance.escudos == 0)
        {
            VidaManager.Instance.PerderVida();
        }
        else
        {
            EscudoManager.Instance.PerderEscudo();
        }
        

        Destroy(gameObject);
    }
}
