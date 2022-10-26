using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Audio
{
    /// <summary>
    /// Data for playing an Audio Playlist.
    /// </summary>
    [CreateAssetMenu(fileName = "AudioPlaylistSetupSO", menuName = "Mixin/Audio/AudioPlaylistSetupSO")]
    [System.Serializable]
    public class AudioPlaylistSetupSO : ScriptableObject
    {
        /// <summary>
        /// Should the Playlist automatically play the next Track after finishing one?
        /// </summary>
        [Tooltip("Should the Playlist automatically play the next Track after finishing one?")]
        [SerializeField]
        private bool _automatic;

        /// <summary>
        /// Should the order of the Tracks be randomized?
        /// </summary>
        [Tooltip("Should the order of the Tracks be randomized?")]
        [SerializeField]
        private bool _shuffle;

        [SerializeField]
        private AudioMixerGroup _audioMixerGroup;

        [SerializeField]
        [Range(0, 1)]
        private float _volume = 1;

        [SerializeField]
        private float _pitch = 1;

        /// <summary>
        /// The duration of fading in at the start of every Track. <br/>
        /// Does nothing if greater equal 0.
        /// </summary>
        [Tooltip("The duration of fading in at the start of every Track.")]
        [SerializeField]
        [Min(0)]
        private float _fadeInDuration;

        /// <summary>
        /// The duration of fading out at the end of every Track. <br/>
        /// Does nothing if greater equal 0.
        /// </summary>
        [Tooltip("The duration of fading out at the end of every Track.")]
        [SerializeField]
        [Min(0)]
        private float _fadeOutDuration;

        /// <summary>
        /// The list of Tracks of this Playlist.
        /// </summary>
        [Tooltip("The list of Tracks of this Playlist.")]
        [SerializeField]
        private List<AudioTrackSetupSO> _audioTrackSetups;

        /// <summary>
        /// Converts to an AudioPlaylistSetup Object.
        /// </summary>
        public AudioPlaylistSetup ToAudioPlaylistSetup()
        {
            AudioPlaylistSetup audioPlaylistSetup = new AudioPlaylistSetup();

            audioPlaylistSetup.Automatic = _automatic;
            audioPlaylistSetup.Shuffle = _shuffle;
            audioPlaylistSetup.AudioMixerGroup = _audioMixerGroup;
            audioPlaylistSetup.Volume = _volume;
            audioPlaylistSetup.Pitch = _pitch;
            audioPlaylistSetup.FadeInDuration = _fadeInDuration;
            audioPlaylistSetup.FadeOutDuration = _fadeOutDuration;

            List<AudioTrackSetup> audioTrackSetups = new List<AudioTrackSetup>();

            foreach (AudioTrackSetupSO audioTrackSetupSO in _audioTrackSetups)
                audioTrackSetups.Add(audioTrackSetupSO.ToAudioTrackSetup());

            audioPlaylistSetup.AudioTrackSetups = audioTrackSetups;

            return audioPlaylistSetup;
        }
    }
}
