using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class VidaManager : MonoBehaviour
{
    public static VidaManager Instance;

    public int vidas = 3;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    public void PerderVida()
    {
        vidas--;
        Debug.Log("¡Perdiste una vida! Vidas restantes: " + vidas);

        if (vidas <= 0)
        {
            SceneManager.LoadScene("Perder"); //scena perder
        }
    }
}

