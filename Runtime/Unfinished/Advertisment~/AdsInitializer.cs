using UnityEngine;
using UnityEngine.Advertisements;

public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    string _androidGameId;
    string _iOSGameId;
    bool _testMode;
    private string _gameId;

    internal void Setup(string _androidGameId, string _iOSGameId, bool _testMode)
    {
        this._androidGameId = _androidGameId;
        this._iOSGameId = _iOSGameId;
        this._testMode = _testMode;
        InitializeAds();
    }

    private void InitializeAds()
    {
        _gameId = (Application.platform == RuntimePlatform.IPhonePlayer)
            ? _iOSGameId
            : _androidGameId;
        Advertisement.Initialize(_gameId, _testMode, this);
    }

    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
    }

    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
    }
}

