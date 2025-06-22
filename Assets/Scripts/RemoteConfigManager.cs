using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.RemoteConfig;

public class RemoteConfigManager : MonoBehaviour
{
    public struct userAttributes { }
    public struct appAttributes { }

    // Variables que traeremos de Remote Config
    public static int spawnInterval;
    public static bool spawnRandomPositions;
    public static float objectScale;
    public static string activeSkin;
    public static float mergeBonusMultiplier;

    void Start()
    {
        ConfigManager.FetchCompleted += ApplyRemoteSettings;
        ConfigManager.FetchConfigs(new userAttributes(), new appAttributes());
    }

    private void ApplyRemoteSettings(ConfigResponse configResponse)
    {
        spawnInterval = ConfigManager.appConfig.GetInt("spawnInterval", 2);
        spawnRandomPositions = ConfigManager.appConfig.GetBool("spawnRandomPositions", true);
        objectScale = ConfigManager.appConfig.GetFloat("objectScale", 1f);
        activeSkin = ConfigManager.appConfig.GetString("activeSkin", "default");
        mergeBonusMultiplier = ConfigManager.appConfig.GetFloat("mergeBonusMultiplier", 1f);

        Debug.Log("Remote Config applied");
    }
}
