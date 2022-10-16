using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    [System.Serializable]
    public class AudioSetup
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

        public AudioSetup Copy()
        {
            AudioSetup audioSetup = new AudioSetup();

            audioSetup.AudioClip = AudioClip;
            audioSetup.AudioMixerGroup = AudioMixerGroup;
            audioSetup.Volume = Volume;
            audioSetup.Pitch = Pitch;
            audioSetup.Loop = Loop;
            audioSetup.FadeInDuration = FadeInDuration;
            audioSetup.FadeOutDuration = FadeOutDuration;

            return audioSetup;
        }

        public bool FadeIn => FadeInDuration > 0;
        public bool FadeOut => FadeOutDuration > 0;
    }
}
