using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnMerge : MonoBehaviour
{
    public GameObject[] prefabs;

    private float timer;

    void Update()
    {
        timer += Time.deltaTime;

        if (timer >= RemoteConfigManager.spawnInterval)
        {
            SpawnObject();
            timer = 0f;
        }
    }

    void SpawnObject()
    {
        GameObject prefabToSpawn = prefabs[Random.Range(0, prefabs.Length)];
        GameObject obj = Instantiate(prefabToSpawn);
        obj.transform.localScale = Vector3.one * RemoteConfigManager.objectScale;
    }
}



