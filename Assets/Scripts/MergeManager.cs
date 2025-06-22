using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public void SpawnMerged(int level, Vector3 position)
    {
        if (level >= 0 && level < prefabs.Length)
        {
            GameObject merged = Instantiate(prefabs[level], position, Quaternion.identity);
        }
    }
}
