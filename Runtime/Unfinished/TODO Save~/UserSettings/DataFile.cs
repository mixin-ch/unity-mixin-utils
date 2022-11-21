using System;

[Serializable]
public class DataFile
{
    /// <summary>
    /// Important information for debugging and support
    /// </summary>
    public string GameVersion;
    public bool TestBuild;
    public int SaveCounter;
    public DateTime LastSave;

    DateTime _lastBackup;
    const string _BACKUP_PREFIX = "_backup";

    public void SetFileInformation()
    {
        GameVersion = GameManager.GetGameVersion();
        TestBuild = GameManager.TestMode;
        SaveCounter++;
        LastSave = DateTime.Now;

        //TryGenerateBackup();
    }

    public void TryGenerateBackup()
    {

    }

    private void GetLastBackup()
    {

    }
}