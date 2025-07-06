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

    private void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;
        DontDestroyOnLoad(gameObject);
    }

    private void Start()
    {
        // Selecciona Game ID y Placements según plataforma
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            gameId = iOSGameId;
            rewardedPlacement = rewardedPlacementIOS;
            interstitialPlacement = interstitialPlacementIOS;
        }
        else
        {
            gameId = androidGameId;
            rewardedPlacement = rewardedPlacementAndroid;
            interstitialPlacement = interstitialPlacementAndroid;
        }

        Advertisement.Initialize(gameId, testMode);
        LoadAllAds();
    }

    private void LoadAllAds()
    {
        Advertisement.Load(rewardedPlacement, this);
        Advertisement.Load(interstitialPlacement, this);
    }

    /// <summary>
    /// Muestra un rewarded ad. Si no está cargado, invoca callback inmediato.
    /// </summary>
    public void ShowRewardedAd(Action onComplete)
    {
        if (rewardedLoaded)
        {
            onAdFinished = onComplete;
            rewardedLoaded = false; // marca recarga
            Advertisement.Show(rewardedPlacement, this);
        }
        else
        {
            Debug.LogWarning("Rewarded ad no cargado, ejecutando callback inmediato.");
            onComplete?.Invoke();
            Advertisement.Load(rewardedPlacement, this);
        }
    }

    /// <summary>
    /// Muestra un interstitial. Si no está cargado, invoca callback inmediato.
    /// </summary>
    public void ShowInterstitialAd(Action onComplete)
    {
        if (interstitialLoaded)
        {
            onAdFinished = onComplete;
            interstitialLoaded = false;
            Advertisement.Show(interstitialPlacement, this);
        }
        else
        {
            Debug.LogWarning("Interstitial ad no cargado, ejecutando callback inmediato.");
            onComplete?.Invoke();
            Advertisement.Load(interstitialPlacement, this);
        }
    }

    // IUnityAdsLoadListener ─ dispara cuando una placement termina de cargar
    public void OnUnityAdsAdLoaded(string placementId)
    {
        if (placementId == rewardedPlacement)
            rewardedLoaded = true;
        else if (placementId == interstitialPlacement)
            interstitialLoaded = true;
    }

    public void OnUnityAdsFailedToLoad(string placementId, UnityAdsLoadError error, string message)
    {
        Debug.LogError($"Error cargando {placementId}: {message}");
        // Podrías reintentar aquí tras un delay si quieres
    }

    // IUnityAdsShowListener ─ callbacks de show
    public void OnUnityAdsShowStart(string placementId) { }

    public void OnUnityAdsShowClick(string placementId) { }

    public void OnUnityAdsShowFailure(string placementId, UnityAdsShowError error, string message)
    {
        Debug.LogError($"Error mostrando {placementId}: {message}");
        onAdFinished?.Invoke();
        Advertisement.Load(placementId, this);
    }

    public void OnUnityAdsShowComplete(string placementId, UnityAdsShowCompletionState showCompletionState)
    {
        // Rewarded: solo al COMPLETED
        if (placementId == rewardedPlacement && showCompletionState == UnityAdsShowCompletionState.COMPLETED)
            onAdFinished?.Invoke();

        // Interstitial: al finalizar o saltar
        if (placementId == interstitialPlacement)
            onAdFinished?.Invoke();

        // Recarga automática para la próxima vez
        Advertisement.Load(placementId, this);
    }
}