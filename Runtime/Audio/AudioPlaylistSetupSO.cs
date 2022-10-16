using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    [CreateAssetMenu(fileName = "AudioPlaylistSetupSO", menuName = "Mixin/Audio/AudioPlaylistSetupSO")]
    [System.Serializable]
    public class AudioPlaylistSetupSO : ScriptableObject
    {
        [SerializeField]
        private bool _automatic;
        [SerializeField]
        private bool _shuffle;

        [SerializeField]
        private AudioMixerGroup _audioMixerGroup;
        [SerializeField]
        [Range(0, 1)]
        private float _volume = 1;
        [SerializeField]
        private float _pitch = 1;
        [SerializeField]
        [Min(0)]
        private float _fadeInDuration;
        [SerializeField]
        [Min(0)]
        private float _fadeOutDuration;

        [SerializeField]
        private List<AudioClipSetupSO> _audioClipSetups;

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

            List<AudioClipSetup> audioClipSetups = new List<AudioClipSetup>();

            foreach (AudioClipSetupSO audioClipSetupSO in _audioClipSetups)
                audioClipSetups.Add(audioClipSetupSO.ToAudioClipSetup());

            audioPlaylistSetup.AudioClipSetups = audioClipSetups;

            return audioPlaylistSetup;
        }
    }
}
