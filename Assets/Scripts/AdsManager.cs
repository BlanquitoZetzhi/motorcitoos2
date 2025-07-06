using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using UnityEngine.Advertisements;

public class AdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    public static AdsManager Instance { get; private set; }

    [Header("Game IDs (Inspector)")]
    [SerializeField] private string androidGameId = "5888887";
    [SerializeField] private string iOSGameId = "5888886";
    [SerializeField] private bool testMode = true;

    [Header("Placement IDs (Inspector)")]
    [SerializeField] private string rewardedPlacementAndroid = "Rewarded_Android";
    [SerializeField] private string interstitialPlacementAndroid = "Interstitial_Android";
    [SerializeField] private string rewardedPlacementIOS = "Rewarded_iOS";
    [SerializeField] private string interstitialPlacementIOS = "Interstitial_iOS";

    private string gameId;
    private string rewardedPlacement;
    private string interstitialPlacement;

    private bool rewardedLoaded = false;
    private bool interstitialLoaded = false;
    private Action onAdFinished;

    /// <summary>
    /// Permite al resto del juego consultar si el interstitial ya está listo.
    /// </summary>
    public bool IsInterstitialReady => interstitialLoaded && Advertisement.isInitialized;

    /// <summary>
    /// Permite al resto del juego consultar si el rewarded ya está listo.
    /// </summary>
    public bool IsRewardedReady => rewardedLoaded && Advertisement.isInitialized;

    private void Awake()
    {
        // Singleton
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);

        // Selecciona el Game ID y los placement según la plataforma
#if UNITY_IOS
            gameId               = iOSGameId;
            interstitialPlacement = interstitialPlacementIOS;
            rewardedPlacement     = rewardedPlacementIOS;
#else
        gameId = androidGameId;
        interstitialPlacement = interstitialPlacementAndroid;
        rewardedPlacement = rewardedPlacementAndroid;
#endif

        // Inicializa Unity Ads lo antes posible
        Advertisement.Initialize(gameId, testMode);
    }

    // ─── IUnityAdsInitializationListener ───────────────────────────────────

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads: inicialización completada.");
        // Precarga ambos formatos
        Advertisement.Load(interstitialPlacement, this);
        Advertisement.Load(rewardedPlacement, this);
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.LogError($"Unity Ads inicialización fallida: {error} – {message}");
    }

    // ─── IUnityAdsLoadListener ──────────────────────────────────────────────

    public void OnUnityAdsAdLoaded(string placementId)
    {
        Debug.Log($"Ad cargado: {placementId}");
        if (placementId == interstitialPlacement)
            interstitialLoaded = true;
        if (placementId == rewardedPlacement)
            rewardedLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error cargando Ad Unit {placementId}: {error} – {message}");
    }

    // ─── Métodos públicos para mostrar anuncios ────────────────────────────

    /// <summary>
    /// Muestra un interstitial si está listo; si no, invoca callback de inmediato.
    /// </summary>
    public void ShowInterstitialAd(Action onComplete)
    {
        if (IsInterstitialReady)
        {
            onAdFinished = onComplete;
            interstitialLoaded = false; // marcamos recarga pendiente
            Advertisement.Show(interstitialPlacement, this);
        }
        else
        {
            Debug.LogWarning("Interstitial NO listo, invocando callback inmediato.");
            onComplete?.Invoke();
            Advertisement.Load(interstitialPlacement, this);
        }
    }

    /// <summary>
    /// Muestra un rewarded si está listo; si no, invoca callback de inmediato.
    /// </summary>
    public void ShowRewardedAd(Action onComplete)
    {
        if (IsRewardedReady)
        {
            onAdFinished = onComplete;
            rewardedLoaded = false;
            Advertisement.Show(rewardedPlacement, this);
        }
        else
        {
            Debug.LogWarning("Rewarded NO listo, invocando callback inmediato.");
            onComplete?.Invoke();
            Advertisement.Load(rewardedPlacement, this);
        }
    }

    // ─── IUnityAdsShowListener ──────────────────────────────────────────────

    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error mostrando Ad Unit {placementId}: {error} – {message}");
        onAdFinished?.Invoke();
        Advertisement.Load(placementId, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState state)
    {
        Debug.Log($"Ad show complete: {placementId} – {state}");
        // Rewarded: sólo al COMPLETED
        if (placementId == rewardedPlacement && state == UnityAdsShowCompletionState.COMPLETED)
            onAdFinished?.Invoke();
        // Interstitial: siempre invocamos callback
        if (placementId == interstitialPlacement)
            onAdFinished?.Invoke();
        // Precarga para la próxima vez
        Advertisement.Load(placementId, this);
    }
}