using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdCaller : MonoBehaviour, IUnityAdsListener 
{

    public static bool isReady
    {
        get
        {
            return Advertisement.IsReady(Settings.videoPlacementId);
        }
    }

    void Start()
    {
        Advertisement.AddListener(this);
        Logger.UpdateContent(UILogDataType.Monetization, "Ads initialized");
    }
    public static void ShowAds()
    {
            Advertisement.Show(Settings.videoPlacementId);
    }
    public static void LoadAds()
    {
        Logger.AddContent(UILogDataType.Monetization, "Ads load initiated");
        Advertisement.Load(Settings.videoPlacementId);
    }
    public void OnUnityAdsReady(string placementId)
    {
        if (placementId == Settings.videoPlacementId)
        {
            Debug.Log("Placement " + placementId + " ready");
            Logger.AddContent(UILogDataType.Monetization, "Ads ready");
        }
    }
    public void OnUnityAdsDidError(string message)
    {
        Debug.LogError("Ads error");
        Logger.AddContent(UILogDataType.Monetization, "Ads error");
    }
    public void OnUnityAdsDidFinish (string placementId, ShowResult showResult) {
        if (showResult == ShowResult.Failed)
            Debug.LogWarning("The ad did not finish due to an error");
        Engine.NextLevel();
    }
    public void OnUnityAdsDidStart(string placementId)
    {
        Debug.Log("Demonstrating placement : " + placementId);
    }
}