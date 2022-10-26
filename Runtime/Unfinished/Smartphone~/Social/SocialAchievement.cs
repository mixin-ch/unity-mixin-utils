using UnityEngine;
using GooglePlayGames;
using static AchievementManager;
using UnityEngine.SocialPlatforms;

public class SocialAchievement
{
    //calls the google achievements menu
    public void ShowAchievementsUI()
    {
        if (SocialManager.SocialSignIn.IsSignedIn())
            Social.ShowAchievementsUI();
        else
            Debug.Log("Can not open Google Achievements UI, because user is not signed in");
    }

    public delegate void LoadAchievemensCallBack(IAchievement[] achievementList);

    //Loads Achievements.
    public void LoadAchievements(LoadAchievemensCallBack loadAchievemensCallBack)
    {
        if (SocialManager.SocialSignIn.IsSignedIn())
        {
            Social.LoadAchievements(achievements =>
            {
                if (achievements == null)
                    achievements = new IAchievement[0];
                loadAchievemensCallBack(achievements);
            });
        }
        else
            Debug.Log("User not logged in, can not load achievements");
    }

    //Sets Achievement Progesss to 100%.
    public void UnlockAchievement(string achievementID) => SetAchievementProgress(achievementID, 100);

    //Sets Achievement Progesss to 0%.
    public void LockAchievement(string achievementID) => SetAchievementProgress(achievementID, 0);

    //unlocks a achievement by id
    public void SetAchievementProgress(string achievementID, int progress)
    {
        //progress 100 unlocks the achievement
        if (SocialManager.SocialSignIn.IsSignedIn())
        {
            Social.ReportProgress(achievementID, progress, success =>
            {
                //call custom function when successed
            });
        }
    }

    //increment achievement 
    public void IncrementAchievement(string achievementID, int steps)
    {
#if UNITY_ANDROID
        if (SocialManager.SocialSignIn.IsSignedIn())
            PlayGamesPlatform.Instance.IncrementAchievement(achievementID, steps, success => { });
#endif
    }
}


