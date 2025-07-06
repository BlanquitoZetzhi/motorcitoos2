using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PauseManager : MonoBehaviour
{
    public GameObject pausaUI;
    public GameObject opcionesUI;
    private bool estaPausado = false;

    public void AlternarPausa()
    {
        if (estaPausado)
            Reanudar();
        else
            Pausar();
    }

    public void Pausar()
    {
        pausaUI.SetActive(true);
        opcionesUI.SetActive(false);
        Time.timeScale = 0;
        estaPausado = true;
    }

    public void Reanudar()
    {
        pausaUI.SetActive(false);
        opcionesUI.SetActive(false);
        Time.timeScale = 1;
        estaPausado = false;
    }

    public void ReiniciarNivel()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    public void IrAlMenu()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene("Menu");
    }

    public void MostrarOpciones()
    {
        pausaUI.SetActive(false);
        opcionesUI.SetActive(true);
    }

    public void VolverDeOpciones()
    {
        pausaUI.SetActive(true);
        opcionesUI.SetActive(false);
    }
}
