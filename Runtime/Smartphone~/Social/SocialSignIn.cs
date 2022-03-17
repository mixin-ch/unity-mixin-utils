/*using UnityEngine;
using GooglePlayGames;
using System;
using System.Reflection;
using GooglePlayGames.BasicApi;

public class SocialSignIn
{
    /// <summary>
    /// handles the sign in and sign out for all social platforms -> iOS + Android
    /// </summary>
    public event EventHandler<bool> OnUserSignedIn;
    public event EventHandler OnUserSignedOut;

    public bool IsSignedIn() => Social.localUser.authenticated;

#if UNITY_IOS
    public void SignIn()
    {
        if (!IsSignedIn())
            //opens the social platform sign in 
            Social.localUser.Authenticate(OnSignIn);
        else
            Debug.Log($"{ MethodBase.GetCurrentMethod()}: User already signed in");
    }
#else
    public void SignIn() => SignIn(SignInInteractivity.CanPromptOnce);
    public void SignIn(SignInInteractivity signInInteractivity)
    {
        if (!IsSignedIn())
        {
            // authenticate user:
            PlayGamesPlatform.Instance.Authenticate(signInInteractivity, (result) =>
            {
                //bool success;
                Debug.Log($"{MethodBase.GetCurrentMethod()}: Sign in Method: {signInInteractivity}\n" +
                    $"Current Sign in state: {result}");

                //switch (result)
                //{
                //    case SignInStatus.Success:
                //        success = true;
                //        break;
                //    case SignInStatus.UiSignInRequired:
                //        success = true;
                //        Debug.LogWarning("UiSignInRequired");
                //        break;
                //    case SignInStatus.DeveloperError:
                //        success = false;
                //        break;
                //    case SignInStatus.NetworkError:
                //        success = false;
                //        break;
                //    case SignInStatus.InternalError:
                //        success = false;
                //        break;
                //    case SignInStatus.Canceled:
                //        //SaveManager.UserSettingsData().UserAcceptsSocialLogin = false;
                //        //SaveManager.UserSettingsDataFile().Save();
                //        success = false;
                //        break;
                //    case SignInStatus.AlreadyInProgress:
                //        success = false;
                //        break;
                //    case SignInStatus.Failed:
                //        success = false;
                //        break;
                //    case SignInStatus.NotAuthenticated:
                //        Debug.LogWarning("NotAuthenticated");
                //        success = true;
                //        break;
                //    default:
                //        Debug.LogError("Sign in state unknown");
                //        success = false;
                //        break;
                //}

                //if (success)
                //    Debug.Log("Logging in...");
                //else
                //    Debug.Log("Login failed");

                OnSignIn();
            });
        }
        else
            Debug.Log($"{ MethodBase.GetCurrentMethod()}: User already signed in");

    }

#endif

    public void OnSignIn()
    {
        if (IsSignedIn())
        {
            // load data from cloud
            Debug.Log("User successfully signed in with " + Social.Active);
            //#if UNITY_ANDROID
            //            SocialManager.SocialSave.LoadCloud();
            //#endif
            AchievementManager.Instance.SynchronizeAchievementProgress();
            SaveManager.UserSettingsData.UserAcceptsSocialLogin = true;
            SaveManager.UserSettingsDataFileManager.Save();
        }
        else
        {
            Debug.Log($"{ MethodBase.GetCurrentMethod()} failed, playing offline");
        }

        // fire event
        OnUserSignedIn?.Invoke(this, IsSignedIn());

        SocialManager.Instance.SetupFinished = true;
    }
    private void OnSignIn(bool success) => OnSignIn();

    public void SignOut()
    {
        SaveManager.UserSettingsData.UserAcceptsSocialLogin = false;
#if UNITY_ANDROID
        PlayGamesPlatform.Instance.SignOut();
#endif
        new PopupObject(PopupType.Default, "Signed out", "you successfully signed out.")
            .AddSubmitButtonText(ButtonTextType.Ok)
            .AutoOpen();

        SaveManager.UserSettingsDataFileManager.Save();

        // fire event
        OnUserSignedOut?.Invoke(this, EventArgs.Empty);
    }
}


*/