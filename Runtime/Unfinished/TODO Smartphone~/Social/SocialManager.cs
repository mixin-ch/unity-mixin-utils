using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using System.Reflection;

public class SocialManager : MonoBehaviour, IMainManager
{
    public static SocialManager Instance { get; private set; }

    public static SocialSignIn SocialSignIn { get; private set; }
    public static SocialAchievement SocialAchievement { get; private set; }
    public static SocialLeaderboard SocialLeaderboard { get; private set; }
    public static SocialSave SocialSave { get; private set; }

    public bool IsSaving { get; set; }
    public bool SetupFinished { get; set; } = false;

#if UNITY_ANDROID
    private PlayGamesPlatform playGamesPlatform;
#endif


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    public void Setup() //gets loaded on start
    {
        SocialSignIn = new SocialSignIn();
        SocialAchievement = new SocialAchievement();
        SocialLeaderboard = new SocialLeaderboard();
        SocialSave = new SocialSave();

        Debug.Log($"{MethodBase.GetCurrentMethod()} - User accepts social login: " +
            $"{SaveManager.UserSettingsData.UserAcceptsSocialLogin}");

        //get UserAcceptsSocialLogin variable to see if player accepts login with social
        if (SaveManager.UserSettingsData.UserAcceptsSocialLogin)
        {
            //if so, then initialize the social platform
            InitializeSocialPlatform();

            //and sign in the player
            SocialSignIn.SignIn();
        }
        else
        {
            //if not, send false to sign in
            SocialSignIn.OnSignIn();
        }
    }

#if UNITY_IOS
    private void InitializeSocialPlatform()
    {
        Debug.LogWarning($"{MethodBase.GetCurrentMethod()} currently not supported");
    }

#else
    private void InitializeSocialPlatform()
    {
        // check if playGamesPlatform already exists
        if (playGamesPlatform != null) return;

        //check if user turned on cloud saving manually
        PlayGamesClientConfiguration config = new PlayGamesClientConfiguration.Builder()
       // enables saving game progress.
       .EnableSavedGames()
       // requests a server auth code be generated so it can be passed to an
       //  associated back end server application and exchanged for an OAuth token.
       /*.RequestServerAuthCode(false)*/
       // requests an ID token be generated.  This OAuth token can be used to
       //  identify the player to other services such as Firebase.
       /*.RequestIdToken()*/
       .Build();

        PlayGamesPlatform.InitializeInstance(config);
        // recommended for debugging:
        PlayGamesPlatform.DebugLogEnabled = true;
        // Activate the Google Play Games platform
        //this will set Social to Play Games 
        playGamesPlatform = PlayGamesPlatform.Activate();
    }
#endif

}


