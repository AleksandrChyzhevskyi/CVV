using UnityEngine;
using GoogleMobileAds.Api;
using System;

public class AdsMenedjer : MonoBehaviour
{
    private string adUnitId = "ca-app-pub-7911373869180955/6834077797";
    private InterstitialAd interstitial;
    private int nowLoses;

    private void Start()
    {
        DontDestroyOnLoad(gameObject);

        DestroyAndStartNew(true);
    }

    private void Update()
    {
        if(interstitial.IsLoaded() && GameController.countLoses % 3 == 0 && GameController.countLoses != 0 && GameController.countLoses != nowLoses)
        {
            nowLoses = GameController.countLoses;
            interstitial.Show();
        }
    }    

    public void HandleOnAdFailedToLoad(object sender, AdFailedToLoadEventArgs args)
    {
        DestroyAndStartNew();
    }  

    public void HandleOnAdClosed(object sender, EventArgs args)
    {
        DestroyAndStartNew();
    }
    void DestroyAndStartNew(bool isFirst = false)
    {
        if (!isFirst)
        {
            interstitial.Destroy();
        }
        interstitial = new InterstitialAd(adUnitId);

        
        this.interstitial.OnAdFailedToLoad += HandleOnAdFailedToLoad;        
        this.interstitial.OnAdClosed += HandleOnAdClosed;

        AdRequest request = new AdRequest.Builder().Build();        
        this.interstitial.LoadAd(request);
    }
} 