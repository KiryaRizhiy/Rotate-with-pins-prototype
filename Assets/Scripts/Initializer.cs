using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameAnalyticsSDK;
using UnityEngine.Advertisements;

public class Initializer : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GameAnalytics.Initialize();
        Advertisement.Initialize(Settings.googlePlayId, Settings.testMode);
        Engine.NextLevel();
    }
}
