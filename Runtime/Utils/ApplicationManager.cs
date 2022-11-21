using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin;
using Mixin.Utils;
using UnityEngine.Audio;

/// <summary>
/// TODO 
/// THIS IS NOT FINISHED YET
/// </summary>
public class ApplicationManager : Singleton<ApplicationManager>
{
    public static string GetGameVersion()
        => Application.version;


    /*private void OnApplicationQuit()
    {
        //SaveManager.Instance.SaveAllData();
    }*/



    public void SetQuality(int index) =>
        QualitySettings.SetQualityLevel(index);

    private float CalculateVolume(float value)
    {
        float volume = Mathf.Log10(value / 100f) * 20f;
        if (value == 0) volume = -80f;
        return volume;
    }

    public void SetSoundVolume(AudioMixer mixer, float value, bool makeEffects)
    {
        mixer.SetFloat("SoundVolume", CalculateVolume(value));
    }

    public void SetSoundVolume(AudioMixer mixer, float value) =>
        SetSoundVolume(mixer, value, false);

    public void SetMusicVolume(AudioMixer mixer, float value) =>
        mixer.SetFloat("MusicVolume", CalculateVolume(value));

    public void Vibrate()
    {
        Handheld.Vibrate();
    }

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
