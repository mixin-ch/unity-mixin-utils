using UnityEngine;
using UnityEngine.Advertisements;
using UnityEngine.UI;

public class BannerAdsManager : MonoBehaviour
{
    // For the purpose of this example, these buttons are for functionality testing:
    Button _loadBannerButton;
    Button _showBannerButton;
    Button _hideBannerButton;

    BannerPosition _bannerPosition = BannerPosition.BOTTOM_CENTER;

    string _adUnitId;
    bool _useButtons;

    public void Setup(string _adUnitId, bool _useButtons)
    {
        this._adUnitId = _adUnitId;
        this._useButtons = _useButtons;
        // Get the Ad Unit ID for the current platform:


        // Disable the button until an ad is ready to show:
        if (_useButtons)
        {
            _showBannerButton.interactable = false;
            _hideBannerButton.interactable = false;
        }

        // Set the banner position:
        Advertisement.Banner.SetPosition(_bannerPosition);

        // Configure the Load Banner button to call the LoadBanner() method when clicked:
        if (_useButtons)
        {
            _loadBannerButton.onClick.AddListener(LoadBanner);
            _loadBannerButton.interactable = true;
        }

        LoadBanner();
    }
    public void Setup(string _adUnitId)
    {
        Setup(_adUnitId, false);
    }

    // Implement a method to call when the Load Banner button is clicked:
    public void LoadBanner()
    {
        // Set up options to notify the SDK of load events:
        BannerLoadOptions options = new BannerLoadOptions
        {
            loadCallback = OnBannerLoaded,
            errorCallback = OnBannerError
        };

        // Load the Ad Unit with banner content:
        Advertisement.Banner.Load(_adUnitId, options);
    }

    // Implement code to execute when the loadCallback event triggers:
    void OnBannerLoaded()
    {
        Debug.Log("Banner loaded");

        if (_useButtons)
        {
            // Configure the Show Banner button to call the ShowBannerAd() method when clicked:
            _showBannerButton.onClick.AddListener(ShowBannerAd);
            // Configure the Hide Banner button to call the HideBannerAd() method when clicked:
            _hideBannerButton.onClick.AddListener(HideBannerAd);

            // Enable both buttons:
            _showBannerButton.interactable = true;
            _hideBannerButton.interactable = true;
        }
        else
        {
            ShowBannerAd();
        }
    }

    // Implement code to execute when the load errorCallback event triggers:
    void OnBannerError(string message)
    {
        Debug.Log($"Banner Error: {message}");
        // Optionally execute additional code, such as attempting to load another ad.
    }

    // Implement a method to call when the Show Banner button is clicked:
    public void ShowBannerAd()
    {
        // Set up options to notify the SDK of show events:
        BannerOptions options = new BannerOptions
        {
            clickCallback = OnBannerClicked,
            hideCallback = OnBannerHidden,
            showCallback = OnBannerShown
        };

        // Show the loaded Banner Ad Unit:
        Advertisement.Banner.Show(_adUnitId, options);
    }

    // Implement a method to call when the Hide Banner button is clicked:
    public void HideBannerAd()
    {
        // Hide the banner:
        if (Advertisement.Banner.isLoaded)
            Advertisement.Banner.Hide();
    }

    void OnBannerClicked() { }
    void OnBannerShown() { }
    void OnBannerHidden() { }

    void OnDestroy()
    {
        if (_useButtons)
        {
            // Clean up the listeners:
            _loadBannerButton.onClick.RemoveAllListeners();
            _showBannerButton.onClick.RemoveAllListeners();
            _hideBannerButton.onClick.RemoveAllListeners();
        }
    }
}

