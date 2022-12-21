using System;

[Serializable]
public class DataFile
{
    /// <summary>
    /// Important information for debugging and support
    /// </summary>
    public string GameVersion;
    public int SaveCounter;
    public DateTime LastSave;

    public void SetFileInformation(string gameVersion)
    {
        GameVersion = gameVersion;
        SaveCounter++;
        LastSave = DateTime.Now;
    }
}