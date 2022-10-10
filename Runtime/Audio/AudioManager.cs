using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private List<AudioSource> _audioSources = new List<AudioSource>();
        public List<AudioPlaylistPlayer> _audioPlaylistPlayers = new List<AudioPlaylistPlayer>();

        void Update()
        {
            for (int i = 0; i < _audioSources.Count; i++)
            {
                AudioSource audioSource = _audioSources[i];
                if (!audioSource.isPlaying)
                {
                    Destroy(audioSource);
                    _audioSources.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < _audioPlaylistPlayers.Count; i++)
                _audioPlaylistPlayers[i].Tick(Time.deltaTime);
        }

        public AudioSource Play(AudioSetup audioSetup)
        {
            return Play(audioSetup, false);
        }

        public AudioSource Play(AudioSetup audioSetup, bool loop)
        {
            if (audioSetup == null)
                return null;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            _audioSources.Add(audioSource);
            audioSource.clip = audioSetup.AudioClip;
            audioSource.loop = loop;
            audioSource.volume = audioSetup.Volume;
            audioSource.pitch = audioSetup.Pitch;
            audioSource.outputAudioMixerGroup = audioSetup.AudioMixerGroup;

            if (audioSetup.Pitch < 0)
                audioSource.time = (audioSetup.AudioClip.length - 0.01f).LowerBound(0);

            audioSource.Play();

            return audioSource;
        }

        public AudioSetup CreateNewSound(AudioClip audioClip, AudioMixerGroup audioMixerGroup)
        {
            AudioSetup audioSetup = new AudioSetup();

            audioSetup.AudioClip = audioClip;
            audioSetup.Volume = 1;
            audioSetup.Pitch = 1;
            audioSetup.AudioMixerGroup = audioMixerGroup;

            return audioSetup;
        }

        public AudioPlaylistPlayer PlayPlaylist(List<AudioSetup> audioSetupList)
        {
            if (audioSetupList == null)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.Initialize(audioSetupList);
            _audioPlaylistPlayers.Add(audioPlaylistPlayer);
            audioPlaylistPlayer.Start();

            return audioPlaylistPlayer;
        }

        public AudioPlaylistPlayer FadeInPlayPlaylist(List<AudioSetup> audioSetupList, float fadeTime)
        {
            if (audioSetupList == null)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.Initialize(audioSetupList);
            _audioPlaylistPlayers.Add(audioPlaylistPlayer);
            audioPlaylistPlayer.Start(fadeTime);

            return audioPlaylistPlayer;
        }

        public void StopAllAudio()
        {
            while (_audioSources.Count > 0)
            {
                Destroy(_audioSources[0]);
                _audioSources.RemoveAt(0);
            }

            while (_audioPlaylistPlayers.Count > 0)
            {
                _audioPlaylistPlayers[0].Stop();
                _audioPlaylistPlayers.RemoveAt(0);
            }
        }

        public void Remove(AudioPlaylistPlayer audioPlaylistPlayer)
        {
            _audioPlaylistPlayers.Remove(audioPlaylistPlayer);
        }
    }
}