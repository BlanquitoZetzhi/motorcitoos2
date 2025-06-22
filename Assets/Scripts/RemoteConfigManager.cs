using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;
using Unity.Services.Core;
using Unity.Services.RemoteConfig;
using System.Threading.Tasks;

public class RemoteConfigManager : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    // Variables que vamos a obtener
    public static int spawnInterval = 2;
    public static bool spawnRandomPositions = true;
    public static float objectScale = 1f;
    public static string activeSkin = "default";
    public static float mergeBonusMultiplier = 1f;
    public static float puntosVictoria = 100f;   
    public static int timerMinutes = 1;         
    public static float timerSeconds = 0f;      

    async void Start()
    {
        await UnityServices.InitializeAsync();

        RemoteConfigService.Instance.FetchCompleted += ApplyRemoteSettings;
        RemoteConfigService.Instance.FetchConfigs(new userAttributes(), new appAttributes());
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

        Debug.Log("Remote Config applied with values:");
        Debug.Log($"spawnInterval: {spawnInterval}");
        Debug.Log($"spawnRandomPositions: {spawnRandomPositions}");
        Debug.Log($"objectScale: {objectScale}");
        Debug.Log($"activeSkin: {activeSkin}");
        Debug.Log($"mergeBonusMultiplier: {mergeBonusMultiplier}");
    }
}
