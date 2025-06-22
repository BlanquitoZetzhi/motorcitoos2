using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class TiendaItem : MonoBehaviour
{
    public string itemId;
    public int costo;
    public Button buyButton;
    public TextMeshProUGUI costoText;

    private TiendaManager tiendaManager;

    private void Start()
    {
        tiendaManager = FindObjectOfType<TiendaManager>();
        costoText.text = costo.ToString();

        buyButton.onClick.AddListener(() => tiendaManager.ComprarItem(itemId, costo));
    }

    public void CargarEstado()
    {
        if (PlayerPrefs.GetInt("Item_" + itemId, 0) == 1)
        {
            MarcarComprado();
        }
    }

    public void MarcarComprado()
    {
        buyButton.interactable = false;
        costoText.text = "Comprado";
    }
}

//Falta:
//*Armar Scena de Tienda con Scroll View
//*Prefabs con este script agregado
// un boton
// y un texto para precios (tokens)
//*TiendaManager tiene referencia a:
// lista de TiendaItem (hay que arrastrar los preefabs)
// el texto que muestra los tokens
