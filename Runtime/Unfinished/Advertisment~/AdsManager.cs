using System;
using System.Collections;
using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class AdsManager : MonoBehaviour, IMainManager
{
    public static AdsManager Instance;

    //only used to initialize ads once at the beginning
    [SerializeField] string _androidGameId = "4246821";
    [SerializeField] string _iOSGameId = "4246820";
    private bool _testMode;

    private string AdUnitId;

    public AdsInitializer adsInitializer;
    public RewardedAdsManager rewardedAdsManager;
    public BannerAdsManager bannerAdsManager;
    public InterstitialAdsManager interstitialAdsManager;

    private DeviceType DevicePlatform;

    private RewardType rewardType;
    private PopupManager popupManager;
    [HideInInspector]
    public bool ShowAds;

    public bool SetupFinished { get; private set; } = false;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Setup()
    {
        //first get the platform
        setDevicePlatform();

        //get test mode from gamemanger
        _testMode = GameManager.TestMode;
        //_testMode = false;

        //initialize ads
        adsInitializer.Setup(_androidGameId, _iOSGameId, _testMode);

        popupManager = PopupManager.Instance;
        Reload();

        //tell everything is setup
        SetupFinished = true;
    }
    private DeviceType setDevicePlatform()
    {
        if (Application.platform == RuntimePlatform.IPhonePlayer)
        {
            ShowAds = true;
            DevicePlatform = DeviceType.iOS;
            return DevicePlatform;
        }
        if (Application.platform == RuntimePlatform.Android)
        {
            ShowAds = true;
            DevicePlatform = DeviceType.Android;
            return DevicePlatform;
        }
        else
        {
            ShowAds = false;
            DevicePlatform = DeviceType.UNSUPPORTED;
            return DevicePlatform;
        }
    }
    public string GetAdUnitId(AdType adType)
    {
        switch (DevicePlatform)
        {
            case DeviceType.UNSUPPORTED:
                Debug.LogWarning("platform is not supported, this method should not get called");
                break;
            case DeviceType.iOS:
                switch (adType)
                {
                    case AdType.InterstitialAd:
                        return "Interstitial_iOS";
                    case AdType.RewardedAd:
                        return "Rewarded_iOS";
                    case AdType.BannerAd:
                        return "Banner_iOS";
                    default:
                        break;
                }
                break;
            case DeviceType.Android:
                switch (adType)
                {
                    case AdType.InterstitialAd:
                        return "Interstitial_Android";
                    case AdType.RewardedAd:
                        return "Rewarded_Android";
                    case AdType.BannerAd:
                        return "Banner_Android";
                    default:
                        break;
                }
                break;
        };
        return null;
    }

    public void Reload()
    {
        HideBanner();
    }

    #region Rewarded Ads
    public void LoadRewardedAd(RewardType rewardType, Button button)
    {
        if (!ShowAds) return;

        this.rewardType = rewardType;
        rewardedAdsManager.Setup(GetAdUnitId(AdType.RewardedAd), button, rewardType);
    }
    public void ShowRewardedAd()
    {
        rewardedAdsManager.ShowAd();
    }
    #endregion


    #region Banner Ads
    public void LoadAndShowBannerAd()
    {
        if (!ShowAds) return;

        //this setup call the load banner and also show banner
        bannerAdsManager.Setup(GetAdUnitId(AdType.BannerAd));
    }
    public void HideBanner()
    {
        bannerAdsManager.HideBannerAd();
    }

    #endregion

    public enum AdType
    {
        InterstitialAd,
        RewardedAd,
        BannerAd,
    }
    public enum RewardType
    {
        NONE,
        PlayerRespawn,
        FreeSecrets,
        UnlockAchievementSkyContribution,
        ThankYouPopup,
    }
}
public enum DeviceType
{
    UNSUPPORTED = 0,
    iOS = 1,
    Android = 2,
}

