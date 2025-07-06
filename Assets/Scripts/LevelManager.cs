using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelManager : MonoBehaviour
{
    [Header("Nombres de Escena")]
    [Tooltip("Nombre de la escena de victoria")]
    [SerializeField] private string winSceneName = "WinScene";
    [Tooltip("Nombre de la escena de derrota")]
    [SerializeField] private string loseSceneName = "LoseScene";

    public void EndLevel(bool victory)
    {
        // Pausa lógica si quieres
        Time.timeScale = 0f;

        // Mostramos el ad y luego cargamos la escena (victoria o derrota)
        AdsManager.Instance.ShowInterstitialAd(() =>
        {
            Time.timeScale = 1f;
            SceneManager.LoadScene(victory ? winSceneName : loseSceneName);
        });
    }
}