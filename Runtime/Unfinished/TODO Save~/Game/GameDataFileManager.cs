/*using System;

[Serializable]
public class GameDataFileManager : DataFileManager<GameData>
{
    private const string newFileName = "game-data.mxn";
    private const FileType newFileType = FileType.Binary;

    public GameDataFileManager() : base(newFileName, newFileType) { }

    protected override void SetFileInformation()
    {
        Data.SetFileInformation();
    }

    public void SetDefaultData()
    {
        Data = new GameData();
        Data.RefreshData();
    }

    public void LevelEndSetup()
    {
        Data.LevelEndSetup();
        Save();
    }

    protected override void onDataSaved(bool success)
    {
        // only execute when SocialManager is setup
        if (SocialManager.Instance.SetupFinished)
        {
            // if build is not in test mode, then report it to the leaderboard
            if (!GameManager.TestMode)
            {
                // Post score to leaderboard.
                SocialManager.SocialLeaderboard.ReportScore(
                    Data.CurrentLevelStatSavePackage.HighScore,
                    SocialLeaderboard.LEADERBOARD_ID_HIGHSCORE,
                    null
                );
            }
        }
    }

    protected override void onDataLoaded(bool success)
    {
        Data.RefreshData();

        if (success)
        {
            // When Data got illegally changed, destroy the Game
            CheckForLegalData();
        }
    }

    protected override void onDataDeleted(bool success)
    {
        SetDefaultData();

#if UNITY_ANDROID
        // delete data from cloud
        SocialManager.SocialSave.DeleteCloudData();
#endif
    }

    // Check if User made any custom Changes in File to cheat
    private void CheckForLegalData()
    {
        // When Test Mode Variable is not the same, then make the Game corrupt
        if (SaveManager.GameData.TestBuild != GameManager.TestMode)
        {
            GameManager.Instance.StopGameFromWorking = true;
        }
    }
}*/