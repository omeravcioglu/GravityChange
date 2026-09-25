using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Advertisements;

public class AdManager : MonoBehaviour
{
    public static AdManager instance;

    private static string rewVideoID = "rewardedVideo";
    private static string videoID = "video";
    private string gameId = "0000000";

    public static bool adIsFinished;
    public static bool isNoAds;

    public bool testMode;

    void Start()
    {
        if (instance == null) instance = this;
        if (Advertisement.isSupported) Advertisement.Initialize(gameId, testMode);
    }

    public bool lastAdIsFinished()
    {
        return adIsFinished;
    }

    public void showAd(float delay)
    {
        StartCoroutine(delayShowAd(delay));
    }

    public int showAd()
    {
        if (Application.internetReachability == NetworkReachability.NotReachable) return -1;

        if (Advertisement.IsReady(videoID))
        {
            showAd(videoID);
            return 0;
        }

        return -1;
    }

    private void showAd(string placementId)
    {
        adIsFinished = false;

        Advertisement.Show(placementId, new ShowOptions { resultCallback = handleShowResult });
    }

    private void handleShowResult(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            adIsFinished = true;
        }      
    }

    private IEnumerator delayShowAd(float time)
    {
        yield return new WaitForSecondsRealtime(time);
        showAd();
    }
}
