using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MergeManager : MonoBehaviour
{
    public GameObject[] prefabs;

    public GameObject SpawnMerged(int level, Vector3 position)
    {
        if (level >= 0 && level < prefabs.Length)
        {
            return Instantiate(prefabs[level], position, Quaternion.identity);
        }

        return null;
    }
}
