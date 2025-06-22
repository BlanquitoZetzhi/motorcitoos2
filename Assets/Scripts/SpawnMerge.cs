using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMerge : MonoBehaviour
{
    public GameObject[] prefabs;
    public float intervaloSpawn = 2f;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= intervaloSpawn)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        
        Instantiate(prefabToSpawn);
    }
}



