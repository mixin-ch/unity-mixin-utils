using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    [System.Serializable]
    public class AudioSetup
    {
        public AudioClip AudioClip;
        [Range(0, 1)]
        public float Volume = 1;
        public float Pitch = 1;
        public AudioMixerGroup AudioMixerGroup;

        public AudioSetup Copy()
        {
            AudioSetup audioSetup = new AudioSetup();

            audioSetup.AudioClip = AudioClip;
            audioSetup.Volume = Volume;
            audioSetup.Pitch = Pitch;
            audioSetup.AudioMixerGroup = AudioMixerGroup;

            return audioSetup;
        }
    }
}
