using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour
{
    public GameObject[] prefabs; // Prefabs normales
    public GameObject bombaPrefab; // Prefab de la bomba
    public float intervaloSpawn = 2f;
    public Vector2 AreaMinSpawn = new Vector2(-10f, -10f);
    public Vector2 AreaMaxSpawn = new Vector2(10f, 10f);

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= RemoteConfigManager.spawnInterval) // remote config
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        Vector3 spawnPosition = RemoteConfigManager.spawnRandomPositions
            ? new Vector3(
                Random.Range(AreaMinSpawn.x, AreaMaxSpawn.x),
                Random.Range(AreaMinSpawn.y, AreaMaxSpawn.y),
                0f)
            : transform.position;

        GameObject prefabToSpawn;
        // 20% chance de spawn bomba
        if (Random.value < 0.2f)
        {
            prefabToSpawn = bombaPrefab;
        }
        else
        {
            prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        }

        GameObject obj = Instantiate(prefabToSpawn, spawnPosition, Quaternion.identity);
        obj.transform.localScale = Vector3.one * RemoteConfigManager.objectScale;
    }
}
