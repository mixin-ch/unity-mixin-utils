/*using System;

[Serializable]
public class UserSettingsDataFileManager : DataFileManager<UserSettingsData>
{
    private const string newFileName = "user-settings.xml";
    private const FileType newFileType = FileType.XML;

    public UserSettingsDataFileManager() : base(newFileName, newFileType){ }

    protected override void SetFileInformation()
    {
        Data.SetFileInformation();
    }

    public void SetDefaultData()
    {
        Data = new UserSettingsData();
    }

    protected override void onDataDeleted(bool success)
    {
        SetDefaultData();
    }
}*/