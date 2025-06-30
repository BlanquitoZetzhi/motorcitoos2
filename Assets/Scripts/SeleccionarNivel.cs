using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using TMPro;

public class BotonNivel : MonoBehaviour
{
    public int nivel; // Ej: 0 para Nivel 1
    public Button boton;
    public GameObject candado; // Objeto visual del candado (opcional)
    public TextMeshProUGUI textoNivel; // Texto que dice "Nivel X"

    void Start()
    {
        int nivelDesbloqueado = PlayerPrefs.GetInt("NivelMaxDesbloqueado", 0);
        bool estaDesbloqueado = nivel <= nivelDesbloqueado;

        // Activar/desactivar botón
        boton.interactable = estaDesbloqueado;

        // Mostrar/ocultar candado visual
        if (candado != null)
            candado.SetActive(!estaDesbloqueado);

        // Mostrar número de nivel en texto
        if (textoNivel != null)
            textoNivel.text = "Nivel " + (nivel + 1); // para que muestre "Nivel 1", "Nivel 2"...
    }

    public void CargarNivel()
    {
        // Evita que se abra un nivel bloqueado por accidente
        if (!boton.interactable) return;

        PlayerPrefs.SetInt("NivelGuardado", nivel);
        SceneManager.LoadScene("LevelOne"); // Nombre de tu escena de juego
    }
}