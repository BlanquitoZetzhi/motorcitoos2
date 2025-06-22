using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


//agregar un game objet vacio y tirar C#
//asignar el tokenText con TextMeshProUGUI para que muestre los token


public class CurrencyManager : MonoBehaviour
{
    public static CurrencyManager Instance;

    public int tokens = 0;
    public TextMeshProUGUI tokenText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject); // Para que persista entre escenas
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadTokens();
        UpdateTokenUI();
    }

    public void AddToken(int amount)
    {
        tokens += amount;
        SaveTokens();
        UpdateTokenUI();
        Debug.Log("💰 Token agregado. Total: " + tokens);
    }

    private void UpdateTokenUI()
    {
        if (tokenText != null)
        {
            tokenText.text = tokens.ToString();
        }
    }

    private void SaveTokens()
    {
        PlayerPrefs.SetInt("Tokens", tokens);
        PlayerPrefs.Save();
    }

    private void LoadTokens()
    {
        tokens = PlayerPrefs.GetInt("Tokens", 0);
    }

    public void ResetTokens()
    {
        tokens = 0;
        SaveTokens();
        UpdateTokenUI();
    }
}
