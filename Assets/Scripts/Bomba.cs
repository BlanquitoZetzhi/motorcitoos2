using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Bomba : MonoBehaviour
{
    private void OnMouseDown()
    {
        //-1 hp
        VidaManager.Instance.PerderVida();
        Destroy(gameObject);
    }
}
