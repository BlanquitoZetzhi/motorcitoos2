using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
using UnityEngine.UI;

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

        vidasTexto.text = " " + vidas;
    }

    public void PerderVida()
    {
        vidas--;
        Debug.Log("¡Perdiste una vida! Vidas restantes: " + vidas);

        UpdateVidasUI();

        if (vidas <= 0)
        {
            // Mostramos el anuncio antes de la escena de derrota
            AdsManager.Instance.ShowInterstitialAd(() =>
            {
                SceneManager.LoadScene("Perder");
            });
        }
    }

    private void UpdateVidasUI()
    {

        if (vidasTexto != null)
        {
            vidasTexto.text = " " + vidas;
        }
    }

    public void AddVida()
    {
        vidas++;
        Debug.Log("agregaste vida mostro");
    }

}

