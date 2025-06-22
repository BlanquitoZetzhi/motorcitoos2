using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Timer : MonoBehaviour
{
    public float limitSeconds = 10f;
    public Color dangerColor;
    public TextMeshProUGUI TimerText;

    private int minutes;
    private float seconds;
    private bool gameOverTriggered = false;

    private void Start()
    {
        minutes = RemoteConfigManager.timerMinutes;
        seconds = RemoteConfigManager.timerSeconds;
        ActualizarContador();
    }

    private void Update()
    {
        if (gameOverTriggered) return;

        seconds -= Time.deltaTime;

        if (seconds <= 0)
        {
            if (minutes > 0)
            {
                minutes--;
                seconds = 60;
            }
            else
            {
                seconds = 0;
                PerderPorTiempo();
                return;
            }
        }

        ActualizarContador();

        if (seconds < limitSeconds && minutes < 1)
        {
            TimerText.color = dangerColor;
        }
    }

    private void PerderPorTiempo()
    {
        gameOverTriggered = true;
        Debug.Log("⏰ Tiempo agotado, perdiste.");
        SceneManager.LoadScene("Perder"); // Cambia por escena
    }

    public void ActualizarContador()
    {
        if (seconds < 9.9f)
        {
            if (minutes < 1)
            {
                TimerText.text = minutes + ":" + seconds.ToString("0.0");
            }
            else
            {
                TimerText.text = minutes + ":0" + seconds.ToString("f0");
            }
        }
        else
        {
            TimerText.text = minutes + ":" + seconds.ToString("f0");
        }
    }
}