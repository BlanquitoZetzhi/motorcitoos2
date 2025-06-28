using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class puntitos : MonoBehaviour
{
    private float score;
    private bool victoryTriggered = false;

    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
        UpdateScoreUI();
    }

    public void AgregarPuntos(float puntosAgregados)
    {
        if (victoryTriggered) return;

        score += puntosAgregados;

        if (score >= RemoteConfigManager.puntosVictoria)
        {
            Victoria();
        }

        UpdateScoreUI();
    }

    private void UpdateScoreUI()
    {
        textMesh.text = score.ToString("0");
    }

    private void Victoria()
    {
        victoryTriggered = true;
        Debug.Log("🎉 ¡Ganaste! Puntos: " + score);
        SceneManager.LoadScene("Victoria"); // Cambia por escena
    }
}
