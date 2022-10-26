using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin;
using Mixin.Utils;

public class ApplicationManager : Singleton<ApplicationManager>
{
    public static string GetGameVersion()
        => Application.version;


    private void OnApplicationQuit()
    {
        //SaveManager.Instance.SaveAllData();
    }



    public void SetQuality(int index) =>
        QualitySettings.SetQualityLevel(index);



    public static void QuitApplication()
    {
        //SaveManager.Instance.SaveAllData();
        Application.Quit();
    }

    public void RestartApplication()
    {
        //_isSetup = false;
        //SceneManager.Instance.ChangeScene("Setup");
    }

}
