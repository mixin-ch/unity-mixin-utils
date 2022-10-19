using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Audio
{
    /// <inheritdoc cref="AudioPlaylistSetupSO"/>
    [System.Serializable]
    public class AudioPlaylistSetup
    {
        /// <inheritdoc cref="AudioPlaylistSetupSO._automatic"/>
        public bool Automatic;

        /// <inheritdoc cref="AudioPlaylistSetupSO._shuffle"/>
        public bool Shuffle;

        /// <inheritdoc cref="AudioPlaylistSetupSO._audioMixerGroup"/>
        public AudioMixerGroup AudioMixerGroup;

        /// <inheritdoc cref="AudioPlaylistSetupSO._volume"/>
        [Range(0, 1)]
        public float Volume = 1;

        /// <inheritdoc cref="AudioPlaylistSetupSO._pitch"/>
        public float Pitch = 1;

        /// <inheritdoc cref="AudioPlaylistSetupSO._fadeInDuration"/>
        [Min(0)]
        public float FadeInDuration;

        /// <inheritdoc cref="AudioPlaylistSetupSO._fadeOutDuration"/>
        [Min(0)]
        public float FadeOutDuration;

        /// <inheritdoc cref="AudioPlaylistSetupSO._audioTrackSetups"/>
        public List<AudioTrackSetup> AudioTrackSetups;

        /// <summary>
        /// Does the Setup have a Fade at the start?
        /// </summary>
        public bool FadeIn => FadeInDuration > 0;

        /// <summary>
        /// Does the Setup have a Fade at the end?
        /// </summary>
        public bool FadeOut => FadeOutDuration > 0;

        /// <summary>
        /// Does the Setup have a Fade at the start or end?
        /// </summary>
        public bool Fade => FadeIn || FadeOut;
    }
}
