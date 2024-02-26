using UnityEngine;
using UnityEngine.Advertisements;
 
public class AdsInitializer : MonoBehaviour, IUnityAdsInitializationListener
{
    [SerializeField] string _androidGameId;
    [SerializeField] string _iOSGameId;
    [SerializeField] bool _testMode = true;
    private string _gameId;
    private bool isLoaded;

    [Header("Element")]
    [SerializeField] private InterstitialAd _interstitialAd;

    [SerializeField] private RewardedAdButton _rewardedAdButton;
 
    void Awake()
    {
        DontDestroyOnLoad(this.gameObject);
        
        InitializeAds();
    }
 
    public void InitializeAds()
    {
#if UNITY_IOS
            _gameId = _iOSGameId;
#elif UNITY_ANDROID
        _gameId = _androidGameId;
#elif UNITY_EDITOR
            _gameId = _androidGameId; //Only for testing the functionality in the Editor
#endif
        if (!Advertisement.isInitialized && Advertisement.isSupported)
        {
            Advertisement.Initialize(_gameId, _testMode, this);
        }
    }

 
    public void OnInitializationComplete()
    {
        Debug.Log("Unity Ads initialization complete.");
        
        _interstitialAd.LoadAd();
        _rewardedAdButton.LoadAd();

        isLoaded = true;
    }
 
    public void OnInitializationFailed(UnityAdsInitializationError error, string message)
    {
        Debug.Log($"Unity Ads Initialization Failed: {error.ToString()} - {message}");
        
        InitializeAds();
    }

    public bool IsLoaded()
    {
        return isLoaded;
    }
}