using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    [CreateAssetMenu(fileName = "AudioSetup", menuName = "Mixin/Audio/AudioSetupSO")]
    [System.Serializable]
    public class AudioClipSetupSO : ScriptableObject
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

        public static List<AudioClipSetupSO> GenerateClipAudioSetupSOs(AudioPlaylistSetup audioPlaylistSetup)
        {
            List<AudioClipSetupSO> audioClipSetupSOs = new List<AudioClipSetupSO>();

            foreach (AudioClip audioClip in audioPlaylistSetup.AudioClips)
            {
                AudioClipSetupSO audioClipSetupSO = new AudioClipSetupSO();
                audioClipSetupSO.AudioClip = audioClip;
                audioClipSetupSO.Volume = audioPlaylistSetup.Volume;
                audioClipSetupSO.Pitch = audioPlaylistSetup.Pitch;
                audioClipSetupSO.FadeInDuration = audioPlaylistSetup.FadeInDuration;
                audioClipSetupSO.FadeOutDuration = audioPlaylistSetup.FadeOutDuration;
                audioClipSetupSOs.Add(audioClipSetupSO);
            }

            return audioClipSetupSOs;
        }
    }
}