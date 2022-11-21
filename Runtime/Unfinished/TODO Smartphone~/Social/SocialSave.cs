using UnityEngine;
using GooglePlayGames;
using GooglePlayGames.BasicApi;
using GooglePlayGames.BasicApi.SavedGame;
using System;

public class SocialSave
{
    public event EventHandler<bool> OnDataSavedToCloud;
    public event EventHandler<bool> OnDataLoadedFromCloud;

    //own methods
    public void SaveCloud()
    {
        if (SaveManager.UserSettingsData.UserAcceptsSocialLogin)
        {
            if (SocialManager.SocialSignIn.IsSignedIn())
            {
                Debug.Log("user is connected to play games, saving data to cloud..");
                SocialManager.Instance.IsSaving = true;
                OpenSavedGame(SaveManager.GameDataFileManager.GetFileNameWithoutExtension());
            }
            else
            {
                Debug.Log("connection failed, can not save to cloud");
                Debug.Log("Trying to sign in");
#if UNITY_ANDROID
                SocialManager.SocialSignIn.SignIn(SignInInteractivity.CanPromptAlways);
#endif
            }
        }
        else
            Debug.Log("Can not save to cloud, UserAcceptsSocialLogin is set to false");
    }

    public void LoadCloud()
    {
        if (SocialManager.SocialSignIn.IsSignedIn())
        {
            Debug.Log("user is connected to play games, loading data from the cloud..");
            SocialManager.Instance.IsSaving = false;
            OpenSavedGame(SaveManager.GameDataFileManager.GetFileNameWithoutExtension());
        }
        else
        {
            Debug.Log("connection failed, can not load from cloud");
            Debug.Log("Trying to sign in");
#if UNITY_ANDROID
            SocialManager.SocialSignIn.SignIn(SignInInteractivity.CanPromptAlways);
#endif
        }
    }

    public void DeleteCloudData()
    {
        if (SocialManager.SocialSignIn.IsSignedIn())
        {
            Debug.Log("user is connected to play games, loading data from the cloud..");
            SocialManager.Instance.IsSaving = true;
            DeleteGameData(SaveManager.GameDataFileManager.GetFileNameWithoutExtension());
        }
        else
        {
            Debug.Log("connection failed, can not load from cloud");
            Debug.Log("Trying to sign in");
#if UNITY_ANDROID
            SocialManager.SocialSignIn.SignIn(SignInInteractivity.CanPromptAlways);
#endif
        }
    }

    ////this method checks all conditions for saving to the cloud
    ////if everthing matches, it will return true
    //public bool UsesCloudServices() => 
    //    SaveManager.Instance.SaveData.CloudSaving && 
    //    SocialManager.Instance.SocialSignIn.IsSignedIn();


    //Displaying saved games UI
    //The standard UI for selecting or creating a saved game entry is displayed by calling:
    public void ShowSelectUI()
    {
#if UNITY_ANDROID
        uint maxNumToDisplay = 5;
        bool allowCreateNew = false;
        bool allowDelete = true;

        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ShowSelectSavedGameUI("Select saved game",
            maxNumToDisplay,
            allowCreateNew,
            allowDelete,
            OnSavedGameSelected);
#endif
    }
    public void OnSavedGameSelected(SelectUIStatus status, ISavedGameMetadata game)
    {
        if (status == SelectUIStatus.SavedGameSelected)
        {
            // handle selected game save
        }
        else
        {
            // handle cancel or error
        }
    }


    //Opening a saved game
    //In order to read or write data to a saved game, the saved game needs to be opened.Since the saved game state is cached locally on the device and saved to the cloud, it is possible to encounter conflicts in the state of the saved data.A conflict happens when a device attempts to save state to the cloud but the data currently on the cloud was written by a different device.These conflicts need to be resolved when opening the saved game data. There are 2 open methods that handle conflict resolution, the first OpenWithAutomaticConflictResolution accepts a standard resolution strategy type and automatically resolves the conflicts. The other method, OpenWithManualConflictResolution accepts a callback method to allow the manual resolution of the conflict.
    //See GooglePlayGames/BasicApi/SavedGame/ISavedGameClient.cs for more details on these methods.
    void OpenSavedGame(string filename)
    {
#if UNITY_ANDROID
        Debug.Log("opening " + filename);
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, OnSavedGameOpened);
#endif
    }

    //when file opened
    public void OnSavedGameOpened(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            if (SocialManager.Instance.IsSaving) // writing - saving
            {
                Debug.Log("OnSavedGameOpened saving");

                //byte[] data = System.Text.ASCIIEncoding.ASCII.GetBytes("Test Save");
                byte[] data = SaveManager.Instance.ObjectToByteArray(
                    SaveManager.GameData);
                SaveGame(game, data, TimeSpan.FromSeconds(
                    SaveManager.GameData.GameStatSavePackage.Stats[StatType.SecondsPlayed]));

                // Data successfully saved to the cloud
                OnDataSavedToCloud?.Invoke(this, true);
            }
            else //reading - loading
            {
                Debug.Log("OnSavedGameOpened reading");
                LoadGameData(game);
            }
        }
        else
        {
            // handle error
            Debug.Log("OnSavedGameOpened went wrong");
        }
    }

    //Writing a saved game
    //Once the saved game file is opened, it can be written to save the game state. This is done by calling CommitUpdate. There are four parameters to CommitUpdate:

    //1. the saved game metadata passed to the callback passed to one of the Open calls.
    //2. the updates to make to the metadata.
    //3. the actual byte array of data
    //4. a callback to call when the commit is complete.
    void SaveGame(ISavedGameMetadata game, byte[] savedData, TimeSpan totalPlaytime)
    {
#if UNITY_ANDROID
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;

        SavedGameMetadataUpdate.Builder builder = new SavedGameMetadataUpdate.Builder();
        builder = builder
            .WithUpdatedPlayedTime(totalPlaytime)
            .WithUpdatedDescription("Saved game at " + DateTime.Now);
        //if (savedImage != null)
        //{
        //    // This assumes that savedImage is an instance of Texture2D
        //    // and that you have already called a function equivalent to
        //    // getScreenshot() to set savedImage
        //    // NOTE: see sample definition of getScreenshot() method below
        //    byte[] pngData = savedImage.EncodeToPNG();
        //    builder = builder.WithUpdatedPngCoverImage(pngData);
        //}
        SavedGameMetadataUpdate updatedMetadata = builder.Build();
        savedGameClient.CommitUpdate(game, updatedMetadata, savedData, OnSavedGameWritten);
#endif
    }

    public void OnSavedGameWritten(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            // handle reading or writing of saved game.
            Debug.Log("Succesfully saved to Cloud.");
        }
        else
        {
            // handle error
            Debug.Log("Error trying to save to Cloud.");
        }
    }




    //Reading a saved game
    //Once the saved game file is opened, it can be read to load the game state. This is done by calling ReadBinaryData.
    void LoadGameData(ISavedGameMetadata game)
    {
#if UNITY_ANDROID

        bool success;
        //if (game.TotalTimePlayed.TotalSeconds > SaveManager.GameData().GameStatSavePackage.Stats[StatType.SecondsPlayed])
        //{
        Debug.Log("Cloud Save Version has more PlayTime, and was Prioritized.");
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.ReadBinaryData(game, OnSavedGameDataRead);

        success = true;
        //}
        //else
        //{
        //    success = false;
        //    Debug.Log("Local Save Version has more PlayTime, and was Prioritized.");
        //}

        // Fire Event
        OnDataLoadedFromCloud?.Invoke(this, success);

#endif
    }

    public void OnSavedGameDataRead(SavedGameRequestStatus status, byte[] data)
    {
        if (status == SavedGameRequestStatus.Success)
        {
            Debug.Log("OnSavedGameDataRead start");
            Debug.Log(data.Length);
            SaveManager.GameDataFileManager.Data = (GameData)SaveManager.Instance.ByteArrayToObject(data);
            SaveManager.GameDataFileManager.Save();
            // handle processing the byte array data
        }
        else
        {
            Debug.Log("OnSavedGameDataRead no success");
            // handle error
        }
    }



    //Deleting a saved game
    //Once the saved game file is opened, it can be deleted.This is done by calling Delete.
    void DeleteGameData(string filename)
    {
#if UNITY_ANDROID
        // Open the file to get the metadata.
        ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
        savedGameClient.OpenWithAutomaticConflictResolution(filename, DataSource.ReadCacheOrNetwork,
            ConflictResolutionStrategy.UseLongestPlaytime, DeleteSavedGame);
#endif
    }
    public void DeleteSavedGame(SavedGameRequestStatus status, ISavedGameMetadata game)
    {
#if UNITY_ANDROID
        if (status == SavedGameRequestStatus.Success)
        {
            ISavedGameClient savedGameClient = PlayGamesPlatform.Instance.SavedGame;
            savedGameClient.Delete(game);
        }
        else
        {
            Debug.Log("failed DeleteSavedGame");
        }
#endif
    }

    //Utillities
    public Texture2D getScreenshot()
    {
        // Create a 2D texture that is 1024x700 pixels from which the PNG will be
        // extracted
        Texture2D screenShot = new Texture2D(1024, 700);

        // Takes the screenshot from top left hand corner of screen and maps to top
        // left hand corner of screenShot texture
        screenShot.ReadPixels(
            new Rect(0, 0, Screen.width, (Screen.width / 1024) * 700), 0, 0);
        return screenShot;
    }
}


