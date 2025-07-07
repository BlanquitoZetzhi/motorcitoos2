using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using Unity.Services.Authentication;
using System.Threading.Tasks;

public class RemoteConfigManager : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    // Variables globales
    public static int spawnInterval = 2;
    public static bool spawnRandomPositions = true;
    public static float objectScale = 1f;
    public static string activeSkin = "default";
    public static float mergeBonusMultiplier = 1f;
    public static float puntosVictoria = 100f;
    public static int timerMinutes = 1;
    public static float timerSeconds = 0f;
    public static int hp = 5;
    public static int shield = 0;

    public static bool configLoaded = false;

    async void Awake()
    {
        Debug.Log("🔄 Iniciando Unity Services + Remote Config...");

        try
        {
            await UnityServices.InitializeAsync();

            if (!AuthenticationService.Instance.IsSignedIn)
            {
                Debug.Log("🔐 Iniciando sesión anónima...");
                await AuthenticationService.Instance.SignInAnonymouslyAsync();
            }

            RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
            RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
        }
        catch (System.Exception e)
        {
            Debug.LogError("❌ Error inicializando Remote Config: " + e.Message);
        }
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        var config = RemoteConfigService.Instance.appConfig;

        spawnInterval = config.GetInt("spawnInterval", 2);
        spawnRandomPositions = config.GetBool("spawnRandomPositions", true);
        objectScale = config.GetFloat("objectScale", 1f);
        activeSkin = config.GetString("activeSkin", "default");
        mergeBonusMultiplier = config.GetFloat("mergeBonusMultiplier", 1f);
        puntosVictoria = config.GetFloat("puntosVictoria", 100f);
        timerMinutes = config.GetInt("timerMinutes", 1);
        timerSeconds = config.GetFloat("timerSeconds", 0f);
        hp = config.GetInt("hp", 1);
        shield = config.GetInt("shield", 0);

        configLoaded = true;

        Debug.Log("✅ Remote Config aplicado:");
        Debug.Log($"🧪 spawnInterval: {spawnInterval}");
        Debug.Log($"🧪 spawnRandomPositions: {spawnRandomPositions}");
        Debug.Log($"🧪 objectScale: {objectScale}");
        Debug.Log($"🧪 activeSkin: {activeSkin}");
        Debug.Log($"🧪 mergeBonusMultiplier: {mergeBonusMultiplier}");
        Debug.Log($"🧪 puntosVictoria: {puntosVictoria}");
        Debug.Log($"🧪 timerMinutes: {timerMinutes}");
        Debug.Log($"🧪 timerSeconds: {timerSeconds}");
    }
}