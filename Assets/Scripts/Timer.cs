using System.Collections;
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
    private bool timerIniciado = false;

    private IEnumerator Start()
    {
        Debug.Log("⏳ Esperando Remote Config...");
        while (!RemoteConfigManager.configLoaded)
            yield return null;

        minutes = Mathf.Max(RemoteConfigManager.timerMinutes, 0);
        seconds = Mathf.Max(RemoteConfigManager.timerSeconds, 0f);

        Debug.Log($"✅ Timer inicializado en: {minutes}:{seconds:00.0}");

        ActualizarContador();
        timerIniciado = true; // ✅ Ahora sí puede empezar a contar
    }

    private void Update()
    {
        if (!timerIniciado || gameOverTriggered)
            return;

        if (minutes <= 0 && seconds <= 0f)
        {
            PerderPorTiempo();
            return;
        }

        seconds -= Time.deltaTime;

        if (seconds < 0f)
        {
            if (minutes > 0)
            {
                minutes--;
                seconds += 60f;
            }
            else
            {
                seconds = 0f;
            }
        }

        ActualizarContador();

        if (minutes < 1 && seconds < limitSeconds)
        {
            TimerText.color = dangerColor;
        }
    }

    private void ActualizarContador()
    {
        Debug.Log($"🕒 Actualizando contador: {minutes}:{seconds:00.0}");

        if (TimerText == null)
        {
            Debug.LogWarning("⚠️ TimerText no está asignado en el Inspector.");
            return;
        }

        if (minutes == 0 && seconds < 10f)
            TimerText.text = $"{minutes}:{seconds:0.0}";
        else
            TimerText.text = $"{minutes}:{seconds:00}";
    }

    private void PerderPorTiempo()
    {
        if (gameOverTriggered) return;
        gameOverTriggered = true;

        Debug.Log($"⛔ Game over llamado con: {minutes}:{seconds:00.0}");
        SceneManager.LoadScene("Perder");
    }
}