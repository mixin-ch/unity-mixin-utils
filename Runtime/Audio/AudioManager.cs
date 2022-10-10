using UnityEngine;
using System.Collections.Generic;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        public AudioMixerGroup SoundGroup;
        public AudioMixerGroup MusicGroup;
        public bool Mute;

        public AudioMixerGroup DefaultGroup { get; private set; }
        private List<AudioSource> audioSources = new List<AudioSource>();
        public List<AudioPlaylistPlayer> audioPlaylistPlayers = new List<AudioPlaylistPlayer>();

        private void Start()
        {
            DefaultGroup = SoundGroup;
        }

        void Update()
        {
            for (int i = 0; i < audioSources.Count; i++)
            {
                AudioSource audioSource = audioSources[i];
                if (!audioSource.isPlaying)
                {
                    Destroy(audioSource);
                    audioSources.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < audioPlaylistPlayers.Count; i++)
                audioPlaylistPlayers[i].Tick(Time.deltaTime);
        }

        public AudioSource Play(AudioSetup audioSetup)
        {
            return Play(audioSetup, false);
        }

        public AudioSource Play(AudioSetup audioSetup, bool loop)
        {
            if (audioSetup == null)
                return null;

            if (Mute)
                return null;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            audioSources.Add(audioSource);
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

        public AudioSetup CreateNewSound(AudioClip audioClip)
        {
            AudioSetup audioSetup = new AudioSetup();

            audioSetup.AudioClip = audioClip;
            audioSetup.Volume = 1;
            audioSetup.Pitch = 1;
            audioSetup.AudioMixerGroup = DefaultGroup;

            return audioSetup;
        }

        public AudioPlaylistPlayer PlayPlaylist(List<AudioSetup> audioSetupList)
        {
            if (audioSetupList == null)
                return null;

            if (Mute)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.Initialize(audioSetupList);
            audioPlaylistPlayers.Add(audioPlaylistPlayer);
            audioPlaylistPlayer.Start();

            return audioPlaylistPlayer;
        }

        public AudioPlaylistPlayer FadeInPlayPlaylist(List<AudioSetup> audioSetupList, float fadeTime)
        {
            if (audioSetupList == null)
                return null;

            if (Mute)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.Initialize(audioSetupList);
            audioPlaylistPlayers.Add(audioPlaylistPlayer);
            audioPlaylistPlayer.Start(fadeTime);

            return audioPlaylistPlayer;
        }

        public void StopAllAudio()
        {
            while (audioSources.Count > 0)
            {
                Destroy(audioSources[0]);
                audioSources.RemoveAt(0);
            }

            while (audioPlaylistPlayers.Count > 0)
            {
                audioPlaylistPlayers[0].Stop();
                audioPlaylistPlayers.RemoveAt(0);
            }
        }

        public void Remove(AudioPlaylistPlayer audioPlaylistPlayer)
        {
            audioPlaylistPlayers.Remove(audioPlaylistPlayer);
        }
    }
}