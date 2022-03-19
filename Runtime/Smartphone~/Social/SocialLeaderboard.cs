using UnityEngine;
using System;

public class SocialLeaderboard
{
    //Leaderboard Constants 
    //guilty for both platforms
    public const string LEADERBOARD_ID_HIGHSCORE = "CgkIkbL_198fEAIQAQ";

    public void ShowLeaderboardUI()
    {
        if (SocialManager.SocialSignIn.IsSignedIn())
            Social.ShowLeaderboardUI();
    }
    public void ReportScore(int value, string id, Action call)
    {
        if (SocialManager.SocialSignIn.IsSignedIn())
        {
            // post score 12345 to leaderboard ID "Cfji293fjsie_QA")
            Social.ReportScore(value, id, (bool success) =>
            {
                if (success)
                {
                    // handle success or failure
                    if (call != null)
                        call();
                }
                else
                    new PopupObject(PopupType.Error, "Error", "Could not send Score to Server")
                        .AutoOpen();
            });
        }
    }
    public void GetScore(string id, Action call)
    {
        //Social.LoadScores(id, (IScore score) =>
        //{
        //    //call();
        //});
    }
}


