using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class puntitos : MonoBehaviour
{
    private float score;

    private TextMeshProUGUI textMesh;

    private void Start()
    {
        textMesh = GetComponent<TextMeshProUGUI>();
    }

    private void Update()
    {
        
        textMesh.text = score.ToString("0");

    }

    public void AgregarPuntos(float puntosAgregados)
    {
        score += puntosAgregados;
    }
}
