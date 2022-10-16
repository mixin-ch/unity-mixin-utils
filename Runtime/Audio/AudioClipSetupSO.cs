using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    /// <summary>
    /// Data for playing an Audioclip.
    /// </summary>
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

        /// <summary>
        /// Should the clip loop indefinetly?
        /// </summary>
        [Tooltip("Should the clip loop indefinetly?")]
        [SerializeField]
        private bool _loop;

        /// <summary>
        /// The duration of fading in at the start of the clip. <br/>
        /// Does nothing if greater equal 0.
        /// </summary>
        [Tooltip("The duration of fading in at the start of the clip")]
        [SerializeField]
        [Min(0)]
        private float _startFadeDuration;

        /// <summary>
        /// The duration of fading out at the end of the clip. <br/>
        /// Does nothing if greater equal 0.
        /// </summary>
        [Tooltip("The duration of fading out at the end of the clip")]
        [SerializeField]
        [Min(0)]
        private float _endFadeDuration;

        /// <summary>
        /// Converts to an AudioClipSetup Object.
        /// </summary>
        public AudioClipSetup ToAudioClipSetup()
        {
            AudioClipSetup audioClipSetup = new AudioClipSetup();

            audioClipSetup.AudioClip = _audioClip;
            audioClipSetup.AudioMixerGroup = _audioMixerGroup;
            audioClipSetup.Volume = _volume;
            audioClipSetup.Pitch = _pitch;
            audioClipSetup.Loop = _loop;
            audioClipSetup.StartFadeDuration = _startFadeDuration;
            audioClipSetup.EndFadeDuration = _endFadeDuration;

            return audioClipSetup;
        }
    }
}
