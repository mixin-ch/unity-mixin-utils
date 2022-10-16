using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    [CreateAssetMenu(fileName = "AudioPlaylistSetupSO", menuName = "Mixin/Audio/AudioPlaylistSetupSO")]
    [System.Serializable]
    public class AudioPlaylistSetupSO : ScriptableObject
    {
        public bool Automatic;
        public bool Shuffle;

        public AudioMixerGroup AudioMixerGroup;
        [Range(0, 1)]
        public float Volume = 1;
        public float Pitch = 1;
        [Min(0)]
        public float FadeInDuration;
        [Min(0)]
        public float FadeOutDuration;

        public List<AudioClipSetup> AudioClipSetups;

        public bool FadeIn => FadeInDuration > 0;
        public bool FadeOut => FadeOutDuration > 0;
        public bool Fade => FadeIn || FadeOut;
    }
}
