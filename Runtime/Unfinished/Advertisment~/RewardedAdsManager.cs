using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class RewardedAdsManager : MonoBehaviour, IUnityAdsLoadListener, IUnityAdsShowListener
{
    Button _showAdButton;
    string _adUnitId;
    AdsManager.RewardType rewardType;
    PopupManager popupManager;

    public void Setup(string _adUnitId, Button _showAdButton, AdsManager.RewardType _rewardType)
    {
        this._adUnitId = _adUnitId;
        this._showAdButton = _showAdButton;
        this.rewardType = _rewardType;
        popupManager = PopupManager.Instance;

        Debug.Log("button setup");

        //Disable button until ad is ready to show
        this._showAdButton.interactable = false;

        LoadAd();
    }

    // Load content to the Ad Unit:
    private void LoadAd()
    {
        // IMPORTANT! Only load content AFTER initialization (in this example, initialization is handled in a different script).
        Debug.Log("Loading Ad: " + _adUnitId);
        Advertisement.Load(_adUnitId, this);
    }

    // If the ad successfully loads, add a listener to the button and enable it:
    public void OnUnityAdsAdLoaded(string adUnitId)
    {
        Debug.Log("Ad Loaded: " + adUnitId);

        if (adUnitId.Equals(_adUnitId))
        {
            // Configure the button to call the ShowAd() method when clicked:
            _showAdButton.onClick.AddListener(AdsManager.Instance.ShowRewardedAd);
            // Enable the button for users to click:
            _showAdButton.interactable = true;
        }
    }

    // Implement a method to execute when the user clicks the button.
    public void ShowAd()
    {
        Debug.Log("show ad now");
        // Disable the button: 
        _showAdButton.interactable = false;
        // Then show the ad:
        Advertisement.Show(_adUnitId, this);
    }

    // Implement the Show Listener's OnUnityAdsShowComplete callback method to determine if the user gets a reward:
    public void OnUnityAdsShowComplete(string adUnitId, UnityAdsShowCompletionState showCompletionState)
    {
        if (adUnitId.Equals(_adUnitId) && showCompletionState.Equals(UnityAdsShowCompletionState.COMPLETED))
        {
            // Grant a reward.
            ExecuteReward();
            Debug.Log("Unity Ads Rewarded Ad Completed");

            // Load another ad:
            Advertisement.Load(_adUnitId, this);
        }
        else
        {
            new PopupObject(PopupType.Error, "something went wrong", "Try it later again")
                .AutoOpen();
        }
    }

    // Implement Load and Show Listener error callbacks:
    public void OnUnityAdsFailedToLoad(string adUnitId, UnityAdsLoadError error, string message)
    {
        Debug.Log($"Error loading Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowFailure(string adUnitId, UnityAdsShowError error, string message)
    {
        Debug.Log($"Error showing Ad Unit {adUnitId}: {error.ToString()} - {message}");
        // Use the error details to determine whether to try to load another ad.
    }

    public void OnUnityAdsShowStart(string adUnitId) { }
    public void OnUnityAdsShowClick(string adUnitId) { }

    void OnDestroy()
    {
        // Clean up the button listeners:
        if (_showAdButton != null)
            _showAdButton.onClick.RemoveAllListeners();
    }

    /// <summary>
    /// this method executes the reward of the rewarded ad
    /// </summary>
    private void ExecuteReward()
    {
        switch (rewardType)
        {
            case AdsManager.RewardType.NONE:
                break;
            case AdsManager.RewardType.PlayerRespawn:
                Debug.Log("respawn");
                new PopupObject(PopupType.Reward,
                    "Ready?",
                    "Are you ready to continue the game?"
                    )
                    .AddCall(LevelManager.Instance.ContinueLevelNewChance)
                    .AddCancelButton()
                    .AddSubmitButtonText(ButtonTextType.Yes)
                    .AutoOpen();
                break;
            case AdsManager.RewardType.FreeSecrets:
                new PopupObject(
                    PopupType.Reward,
                    "+50 Secrets",
                    "You got 50 Secrets!"
                    )
                    .AddCall(() => PurchaseManager.Instance.PurchaseSecrets(50))
                    .AddSubmitButtonText(ButtonTextType.Wow)
                    .AutoOpen();
                break;
            case AdsManager.RewardType.UnlockAchievementSkyContribution:
                AchievementManager.Instance.SetAchievementUnlocked(AchievementType.SkyContribution);
                SaveManager.GameDataFileManager.Save();
                break;
            case AdsManager.RewardType.ThankYouPopup:
                break;
            default:
                break;
        }
    }

}

