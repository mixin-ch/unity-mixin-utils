/*using UnityEngine;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Reflection;

public class SaveManager : Singleton
{
    public static readonly float ExperienceCostMultiplier = 100;
    public static readonly float ExperienceCostStageIncrease = 1;
    public static readonly float ExperienceCostGrowthPolonomial = 1;

    // Game Data
    private GameDataFileManager _gameDataFileManager;
    public static GameDataFileManager GameDataFileManager => Instance._gameDataFileManager;
    public static GameData GameData => GameDataFileManager.Data;

    // User Settings
    private UserSettingsDataFileManager _userSettingsDataFileManager;
    public static UserSettingsDataFileManager UserSettingsDataFileManager => Instance._userSettingsDataFileManager;
    public static UserSettingsData UserSettingsData => UserSettingsDataFileManager.Data;

    /// <summary>
    /// Variables for data saving
    /// </summary>
    // this is the general project data path / application folder
    public static string ApplicationPath { get; private set; }

    public bool SetupFinished { get; private set; } = false;


    private void Awake()
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(gameObject);
    }

    void Update()
    {
        if (GameManager.TestMode)
        {
            CheckForCheatShortcuts();
        }
    }

    private void CheckForCheatShortcuts()
    {
        // When Left Shift Button gets pressed
        if (Input.GetKey(KeyCode.LeftShift))
        {
            if (Input.GetKeyDown(KeyCode.S)) // L-Shift + S
                // Save the Game Data
                _gameDataFileManager.Save(); 

            if (Input.GetKeyDown(KeyCode.D)) // L-Shift + D
                // Delete the Game Data
                _gameDataFileManager.Delete();

            //if (Input.GetKeyDown(KeyCode.F))
            //    GameDataFile.GameStatSavePackage.Secrets++;
            //if (Input.GetKeyDown(KeyCode.N))
            //    GameDataFile.GameStatSavePackage.Nobility++;
            //if (Input.GetKeyDown(KeyCode.L))
            //    GameDataFile.GameStatSavePackage.Experience += 10000;
        }
    }

    public void Setup()
    {
        // set app path variable
        ApplicationPath = Application.persistentDataPath;
        $"Saving data to this file location: {ApplicationPath}".Log(Color.yellow);

        // define data classes
        // do this as early as possible
        _gameDataFileManager = new GameDataFileManager();
        _userSettingsDataFileManager = new UserSettingsDataFileManager();

        // setup the data files
        _gameDataFileManager.SetDefaultData();
        _userSettingsDataFileManager.SetDefaultData();

        // load all data
        _gameDataFileManager.Load();
        _userSettingsDataFileManager.Load();

        if (GameManager.TestMode)
            GameManager.Instance.ShowOrHideTestModeBanner(true);

        SetupFinished = true;
    }

    public void SaveAllData()
    {
        GameDataFileManager.Save();
        UserSettingsDataFileManager.Save();
    }

    public void DeleteAllData()
    {
        _gameDataFileManager.Delete();
        _userSettingsDataFileManager.Delete();
    }

    // Convert an object to a byte array
    public byte[] ObjectToByteArray(System.Object obj)
    {
        if (obj == null)
            return null;

        BinaryFormatter bf = new BinaryFormatter();
        MemoryStream ms = new MemoryStream();
        bf.Serialize(ms, obj);

        return ms.ToArray();
    }

    // Convert a byte array to an Object
    public System.Object ByteArrayToObject(byte[] arrBytes)
    {
        MemoryStream memStream = new MemoryStream();
        BinaryFormatter binForm = new BinaryFormatter();
        memStream.Write(arrBytes, 0, arrBytes.Length);
        memStream.Seek(0, SeekOrigin.Begin);
        System.Object obj = (System.Object)binForm.Deserialize(memStream);

        return obj;
    }

    public void OnDataDeleted()
    {
        new PopupObject(PopupType.Default, "Data deleted", "")
            .AddToList();
        // execute setup again
        Setup();
        GameManager.Instance.ChangeScene("MainMenu");
        Debug.Log($"{MethodBase.GetCurrentMethod()} Data successfully deleted");
    }

}*/