using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Audio
{
    /// <inheritdoc cref="AudioTrackSetupSO"/>
    [System.Serializable]
    public class AudioTrackSetup
    {
        /// <inheritdoc cref="AudioTrackSetupSO._audioClip"/>
        public AudioClip AudioClip;

        /// <inheritdoc cref="AudioTrackSetupSO._audioMixerGroup"/>
        public AudioMixerGroup AudioMixerGroup;

        /// <inheritdoc cref="AudioTrackSetupSO._volume"/>
        [Range(0, 1)]
        public float Volume = 1;

        /// <inheritdoc cref="AudioTrackSetupSO._pitch"/>
        public float Pitch = 1;

        /// <inheritdoc cref="AudioTrackSetupSO._loop"/>
        public bool Loop;

        /// <inheritdoc cref="AudioTrackSetupSO._startFadeDuration"/>
        [Min(0)]
        public float StartFadeDuration;

        /// <inheritdoc cref="AudioTrackSetupSO._endFadeDuration"/>
        [Min(0)]
        public float EndFadeDuration;

        /// <summary>
        /// Does the Setup have a Fade at the start?
        /// </summary>
        public bool StartFade => StartFadeDuration > 0;

        /// <summary>
        /// Does the Setup have a Fade at the end?
        /// </summary>
        public bool EndFade => EndFadeDuration > 0;

        /// <summary>
        /// Does the Setup have a Fade at the start or end?
        /// </summary>
        public bool BoundFade => StartFade || EndFade;
    }
}