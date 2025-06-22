using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class caida : MonoBehaviour
{
    [Header("Variables Velocidad")]

    public float minimumXSpeed, maximumXSpeed, minimumYSpeed, maximumYSpeed;

    [Header("Variables de Gameplay")]

    public float lifetime;

    private void Start()
    {

        //tira los objetos para arriba 

        gameObject.GetComponent<Rigidbody2D>().velocity = new Vector2(
            Random.Range(minimumXSpeed, maximumXSpeed),
            Random.Range(minimumXSpeed, maximumYSpeed)
        );

        Destroy(gameObject, lifetime);

    }
}
