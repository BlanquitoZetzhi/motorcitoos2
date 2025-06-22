using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class StaminaManager : MonoBehaviour
{
    public static StaminaManager Instance;

    public int maxStamina = 5;
    public int currentStamina;
    public float staminaRechargeTime = 300f; // 5 minutos por unidad de stamina
    private DateTime nextStaminaTime;

    public TextMeshProUGUI staminaText;

    private void Awake()
    {
        if (Instance == null)
        {
            Instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
    }

    private void Start()
    {
        LoadStamina();
        UpdateStaminaUI();
    }

    private void Update()
    {
        if (currentStamina < maxStamina)
        {
            if (DateTime.Now >= nextStaminaTime)
            {
                currentStamina++;
                SaveStamina();
                UpdateNextStaminaTime();
                UpdateStaminaUI();
            }
        }
    }

    public bool TryUseStamina()
    {
        if (currentStamina > 0)
        {
            currentStamina--;
            SaveStamina();
            if (currentStamina < maxStamina && nextStaminaTime == DateTime.MinValue)
            {
                UpdateNextStaminaTime();
            }
            UpdateStaminaUI();
            return true;
        }
        else
        {
            Debug.Log("❌ No hay stamina");
            return false;
        }
    }

    private void UpdateNextStaminaTime()
    {
        nextStaminaTime = DateTime.Now.AddSeconds(staminaRechargeTime);
        PlayerPrefs.SetString("NextStaminaTime", nextStaminaTime.ToBinary().ToString());
        PlayerPrefs.Save();
    }

    private void SaveStamina()
    {
        PlayerPrefs.SetInt("Stamina", currentStamina);
        PlayerPrefs.Save();
    }

    private void LoadStamina()
    {
        currentStamina = PlayerPrefs.GetInt("Stamina", maxStamina);

        if (PlayerPrefs.HasKey("NextStaminaTime"))
        {
            long temp = Convert.ToInt64(PlayerPrefs.GetString("NextStaminaTime"));
            nextStaminaTime = DateTime.FromBinary(temp);

            // Calcula cuántas unidades se recuperaron de stamina
            while (currentStamina < maxStamina && DateTime.Now >= nextStaminaTime)
            {
                currentStamina++;
                nextStaminaTime = nextStaminaTime.AddSeconds(staminaRechargeTime);
            }

            SaveStamina();
            PlayerPrefs.SetString("NextStaminaTime", nextStaminaTime.ToBinary().ToString());
            PlayerPrefs.Save();
        }
        else
        {
            nextStaminaTime = DateTime.MinValue;
        }
    }

    private void UpdateStaminaUI()
    {
        if (staminaText != null)
        {
            staminaText.text = currentStamina + "/" + maxStamina;
        }
    }

    public void ResetStamina()
    {
        currentStamina = maxStamina;
        nextStaminaTime = DateTime.MinValue;
        SaveStamina();
        PlayerPrefs.DeleteKey("NextStaminaTime");
        UpdateStaminaUI();
    }
}