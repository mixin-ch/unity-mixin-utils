using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    /// <inheritdoc cref="AudioClipSetupSO"/>
    [System.Serializable]
    public class AudioClipSetup
    {
        /// <inheritdoc cref="AudioClipSetupSO._audioClip"/>
        public AudioClip AudioClip;

        /// <inheritdoc cref="AudioClipSetupSO._audioMixerGroup"/>
        public AudioMixerGroup AudioMixerGroup;

        /// <inheritdoc cref="AudioClipSetupSO._volume"/>
        [Range(0, 1)]
        public float Volume = 1;

        /// <inheritdoc cref="AudioClipSetupSO._pitch"/>
        public float Pitch = 1;

        /// <inheritdoc cref="AudioClipSetupSO._loop"/>
        public bool Loop;

        /// <inheritdoc cref="AudioClipSetupSO._startFadeDuration"/>
        [Min(0)]
        public float StartFadeDuration;

        /// <inheritdoc cref="AudioClipSetupSO._endFadeDuration"/>
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