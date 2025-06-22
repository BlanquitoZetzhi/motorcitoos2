using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class TiendaManager : MonoBehaviour
{
    public TiendaItem[] items;  // Referencia a los ítems de la tienda
    public TextMeshProUGUI tokensText;

    private void Start()
    {
        UpdateTokensUI();
        foreach (TiendaItem item in items)
        {
            item.CargarEstado();
        }
    }

    public void ComprarItem(string itemId, int costo)
    {
        if (CurrencyManager.Instance.tokens >= costo)
        {
            CurrencyManager.Instance.AddToken(-costo);
            PlayerPrefs.SetInt("Item_" + itemId, 1); // Guardar como comprado
            PlayerPrefs.Save();
            UpdateTokensUI();

            Debug.Log("🛒 Compraste: " + itemId);

            foreach (TiendaItem item in items)
            {
                if (item.itemId == itemId)
                {
                    item.MarcarComprado();
                }
            }
        }
        else
        {
            Debug.Log("❌ No hay suficientes tokens para comprar " + itemId);
        }
    }

    private void UpdateTokensUI()
    {
        if (tokensText != null)
        {
            tokensText.text = CurrencyManager.Instance.tokens.ToString();
        }
    }
}
