using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    /// <summary>
    /// Data for playing an Audio Track.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioTrackSetupSO", menuName = "Mixin/Audio/AudioTrackSetupSO")]
    [System.Serializable]
    public class AudioTrackSetupSO : ScriptableObject
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
        /// Should the Track loop indefinetly?
        /// </summary>
        [Tooltip("Should the Track loop indefinetly?")]
        [SerializeField]
        private bool _loop;

        /// <summary>
        /// The duration of fading in at the start of the Track. <br/>
        /// Does nothing if greater equal 0.
        /// </summary>
        [Tooltip("The duration of fading in at the start of the Track.")]
        [SerializeField]
        [Min(0)]
        private float _startFadeDuration;

        /// <summary>
        /// The duration of fading out at the end of the Track. <br/>
        /// Does nothing if greater equal 0.
        /// </summary>
        [Tooltip("The duration of fading out at the end of the Track.")]
        [SerializeField]
        [Min(0)]
        private float _endFadeDuration;

        /// <summary>
        /// Converts to an AudioTrackSetup Object.
        /// </summary>
        public AudioTrackSetup ToAudioTrackSetup()
        {
            AudioTrackSetup audioTrackSetup = new AudioTrackSetup();

            audioTrackSetup.AudioClip = _audioClip;
            audioTrackSetup.AudioMixerGroup = _audioMixerGroup;
            audioTrackSetup.Volume = _volume;
            audioTrackSetup.Pitch = _pitch;
            audioTrackSetup.Loop = _loop;
            audioTrackSetup.StartFadeDuration = _startFadeDuration;
            audioTrackSetup.EndFadeDuration = _endFadeDuration;

            return audioTrackSetup;
        }
    }
}
