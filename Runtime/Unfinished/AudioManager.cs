using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;
using Mixin.Utils;

public class AudioManager : Singleton<AudioManager>
{
    [SerializeField] private AudioMixer MixinMixer;

    private float CalculateVolume(float value)
    {
        float volume = Mathf.Log10(value / 100f) * 20f;
        if (value == 0) volume = -80f;
        return volume;
    }
    public void SetSoundVolume(float value, bool makeEffects)
    {
        MixinMixer.SetFloat("SoundVolume", CalculateVolume(value));

        /*if (!makeEffects) return;
        AudioManager.Instance.Play(
            AudioManager.Instance.CreateNewSound(
                CollectionOwner.Instance.GetAudioClip(SoundType.LittleTipping)
        ));*/
        Handheld.Vibrate();
    }


    public void SetSoundVolume(float value) =>
        SetSoundVolume(value, false);
    public void SetMusicVolume(float value) =>
        MixinMixer.SetFloat("MusicVolume", CalculateVolume(value));
}
