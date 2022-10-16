using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    [CreateAssetMenu(fileName = "AudioSetup", menuName = "Mixin/Audio/AudioSetupSO")]
    [System.Serializable]
    public class AudioSetupSO : ScriptableObject
    {
        public AudioClip AudioClip;
        public AudioMixerGroup AudioMixerGroup;
        [Range(0, 1)]
        public float Volume = 1;
        public float Pitch = 1;
        public bool Loop;
        [Min(0)]
        public float FadeInDuration;
        [Min(0)]
        public float FadeOutDuration;

        public bool FadeIn => FadeInDuration > 0;
        public bool FadeOut => FadeOutDuration > 0;
        public bool Fade => FadeIn || FadeOut;

        public static List<AudioSetupSO> GenerateAudioSetups(AudioPlaylistSetup audioPlaylistSetup)
        {
            List<AudioSetupSO> audioSetups = new List<AudioSetupSO>();

            foreach (AudioClip audioClip in audioPlaylistSetup.AudioClips)
            {
                AudioSetupSO audioSetup = new AudioSetupSO();
                audioSetup.AudioClip = audioClip;
                audioSetup.Volume = audioPlaylistSetup.Volume;
                audioSetup.Pitch = audioPlaylistSetup.Pitch;
                audioSetup.FadeInDuration = audioPlaylistSetup.FadeInDuration;
                audioSetup.FadeOutDuration = audioPlaylistSetup.FadeOutDuration;
                audioSetups.Add(audioSetup);
            }

            return audioSetups;
        }
    }
}