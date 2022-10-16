using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    [CreateAssetMenu(fileName = "AudioClipSetupSO", menuName = "Mixin/Audio/AudioClipSetupSO")]
    [System.Serializable]
    public class AudioClipSetupSO : ScriptableObject
    {
        [SerializeField]
        private AudioClip _audioClip;
        [SerializeField]
        private AudioMixerGroup _audioMixerGroup;
        [SerializeField]
        [Range(0, 1)]
        private float _volume = 1;
        [SerializeField]
        private float _pitch = 1;
        [SerializeField]
        private bool _loop;
        [SerializeField]
        [Min(0)]
        private float _fadeInDuration;
        [SerializeField]
        [Min(0)]
        private float _fadeOutDuration;

        public AudioClipSetup ToAudioClipSetup()
        {
            AudioClipSetup audioClipSetup = new AudioClipSetup();

            audioClipSetup.AudioClip = _audioClip;
            audioClipSetup.AudioMixerGroup = _audioMixerGroup;
            audioClipSetup.Volume = _volume;
            audioClipSetup.Pitch = _pitch;
            audioClipSetup.Loop = _loop;
            audioClipSetup.FadeInDuration = _fadeInDuration;
            audioClipSetup.FadeOutDuration = _fadeOutDuration;

            return audioClipSetup;
        }
    }
}
