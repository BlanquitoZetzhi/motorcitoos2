using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class EscudoManager : MonoBehaviour
{
    public static EscudoManager Instance;
    public TextMeshProUGUI escudosTexto;
    public int escudos;

    private void Awake()
    {
        escudos = RemoteConfigManager.shield;
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

        escudosTexto.text = " " + escudos;
    }

    public void PerderEscudo()
    {
        escudos--;
        Debug.Log("¡Perdiste un Escudo! Escudos restantes: " + escudos);

        UpdateEscudosUI();
    }

    private void UpdateEscudosUI()
    {

        if (escudosTexto != null)
        {
            escudosTexto.text = " " + escudos;
        }
    }

    public void AddEscudos()
    {
        escudos++;
        Debug.Log("agregaste escudos mostro");
    }
}

