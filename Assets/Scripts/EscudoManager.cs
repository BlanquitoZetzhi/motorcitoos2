using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class EscudoManager : MonoBehaviour
{
    public static EscudoManager Instance;
    public TextMeshProUGUI escudosTexto;
    public int escudos;

    private void Awake()
    {
       // escudos = RemoteConfigManager.shield;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        escudosTexto.text = "Escudos: " + escudos;
    }

    public void PerderVida()
    {
        escudos--;
        Debug.Log("¡Perdiste un Escudo! Escudos restantes: " + escudos);

        UpdateEscudosUI();
    }

    private void UpdateEscudosUI()
    {
        if (escudosTexto != null)
        {
            escudosTexto.text = "Escudos: " + escudos;
        }
    }
}

