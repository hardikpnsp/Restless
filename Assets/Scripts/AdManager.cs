using UnityEngine.Monetization;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

//After adding this to my project, I started hating my own creation.
//I am never going this route again. NO ADS!

public class AdManager : MonoBehaviour {

    public static AdManager instance;

    public string gameId = "3171665";
    //change this !
    bool testMode = false;

    public string pIdReward = "rewardedVideo";
    public string pIdVid = "video";
    public string pIdSkip = "skipable";

    public bool showAd = false;

    public int adCounter = 0;

    public int bigAdOffset = 12;
    public int smallAdOffset = 3;

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
        }
        else
        {
            Destroy(gameObject);
            return;
        }
        DontDestroyOnLoad(gameObject);
    }

    void Start()
    {
        Monetization.Initialize(gameId, testMode);
    }

    public void ShowRewardAd()
    {
        if (showAd)
        {
            StartCoroutine(WaitForAd());
            showAd = false;
        }
    }

    IEnumerator WaitForAd()
    {
        while (!Monetization.IsReady(pIdReward))
        {
            yield return null;
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(pIdReward) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show(AdFinished);
        }
    }

    void AdFinished(ShowResult result)
    {
        if (result == ShowResult.Finished)
        {
            Debug.Log("damn son ! got more lemons");
        }
    }

    public void ShowSkipableAd()
    {
        adCounter++;
        if (adCounter % bigAdOffset == 0)
        {
            StartCoroutine(ShowLargeAdWhenReady());
        }
        else if (adCounter % smallAdOffset == 0)
        {
            StartCoroutine(ShowAdWhenReady());
        }
        Debug.Log("AdCounter" + adCounter);
    }

    private IEnumerator ShowAdWhenReady()
    {
        while (!Monetization.IsReady(pIdSkip))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(pIdSkip) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }
    }

    private IEnumerator ShowLargeAdWhenReady()
    {
        while (!Monetization.IsReady(pIdVid))
        {
            yield return new WaitForSeconds(0.25f);
        }

        ShowAdPlacementContent ad = null;
        ad = Monetization.GetPlacementContent(pIdVid) as ShowAdPlacementContent;

        if (ad != null)
        {
            ad.Show();
        }

    }
}
