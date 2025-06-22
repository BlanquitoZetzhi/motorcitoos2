using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs;
    public float intervaloSpawn = 2f;
    public Vector2 AreaMinSpawn = new Vector2(-10f, -10f);
    public Vector2 AreaMaxSpawn = new Vector2(10f, 10f);

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

        Vector3 spawnPosition = new Vector3(
        Random.Range(AreaMinSpawn.x, AreaMaxSpawn.x),
        Random.Range(AreaMinSpawn.y, AreaMaxSpawn.y),
        0f
    );

        Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
    }
}
