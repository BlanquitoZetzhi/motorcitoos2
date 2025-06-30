using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

[System.Serializable]
public class ObjetivoDeNivel
{
    public string nombreObjeto; // Por ejemplo: "Zebra"
    public int cantidadRequerida;
}

[System.Serializable]
public class NivelProgresion
{
    public string nombreNivel;
    public List<ObjetivoDeNivel> objetivos;
}

public class GameProgressManager : MonoBehaviour
{
    public List<NivelProgresion> niveles;
    private int nivelActual = 0;
    private Dictionary<string, int> progresoActual = new Dictionary<string, int>();
    private bool victoriaMostrada = false;

    void Start()
    {
        nivelActual = PlayerPrefs.GetInt("NivelGuardado", 0); // si no hay nada, empieza en 0
        IniciarNivel(nivelActual);
    }

    public void IniciarNivel(int index)
    {
        progresoActual.Clear();

        foreach (var objetivo in niveles[index].objetivos)
        {
            progresoActual[objetivo.nombreObjeto] = 0;
        }
    }

    public void ReportarFusion(string nombreObjeto)
    {
        if (!progresoActual.ContainsKey(nombreObjeto)) return;

        progresoActual[nombreObjeto]++;
        Debug.Log($" {nombreObjeto} fusionado. Progreso: {progresoActual[nombreObjeto]}");

        VerificarProgreso();
    }

    private void VerificarProgreso()
    {
        var objetivos = niveles[nivelActual].objetivos;
        foreach (var objetivo in objetivos)
        {
            if (progresoActual[objetivo.nombreObjeto] < objetivo.cantidadRequerida)
                return;
        }

        Debug.Log(" ¡Nivel completado!");

        nivelActual++;
        if (nivelActual < niveles.Count)
        {
            Victoria();
            IniciarNivel(nivelActual);
        }
        else
        {
            Victoria();
        }
    }

    private void Victoria()
    {
        if (victoriaMostrada) return;

        victoriaMostrada = true;

        Debug.Log("¡Juego terminado por progreso!");

        //Desbloqueo progresivo
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelMaxDesbloqueado", 0);
        if (nivelActual > nivelDesbloqueado)
        {
            PlayerPrefs.SetInt("NivelMaxDesbloqueado", nivelActual);
        }

        //Guardamos en qué nivel quedó para continuar
        PlayerPrefs.SetInt("NivelGuardado", nivelActual);
        PlayerPrefs.Save();

        SceneManager.LoadScene("Victoria");
    }
}

