using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;

public class VidaManager : MonoBehaviour
{
    public static VidaManager Instance;
    public TextMeshProUGUI vidasTexto;
    public int vidas;

    private void Awake()
    {
        vidas = RemoteConfigManager.hp;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        vidasTexto.text = "Vidas: " + vidas;
    }

    public void PerderVida()
    {
        vidas--;
        Debug.Log("¡Perdiste una vida! Vidas restantes: " + vidas);

        UpdateVidasUI();

        if (vidas <= 0)
        {
            SceneManager.LoadScene("Perder"); //scena perder
        }
    }

    private void UpdateVidasUI()
    {
        if (vidasTexto != null)
        {
            vidasTexto.text = "Vidas: " + vidas;
        }
    }
}

