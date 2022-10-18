using UnityEngine;
using System.Collections.Generic;

namespace Mixin.Utils.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private List<AudioTrackPlayer> _audioTrackPlayers = new List<AudioTrackPlayer>();
        public List<AudioPlaylistPlayer> _audioPlaylistPlayers = new List<AudioPlaylistPlayer>();

        void Update()
        {
            for (int i = 0; i < _audioTrackPlayers.Count; i++)
            {
                AudioTrackPlayer audioTrackPlayer = _audioTrackPlayers[i];
                audioTrackPlayer.Tick(Time.deltaTime);

                if (!audioTrackPlayer.Running)
                {
                    Destroy(audioTrackPlayer.AudioSource);
                    _audioTrackPlayers.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < _audioPlaylistPlayers.Count; i++)
                _audioPlaylistPlayers[i].Tick(Time.deltaTime);
        }

        public void StopAllAudio()
        {
            while (_audioTrackPlayers.Count > 0)
            {
                Destroy(_audioTrackPlayers[0].AudioSource);
                _audioTrackPlayers.RemoveAt(0);
            }

            while (_audioPlaylistPlayers.Count > 0)
            {
                _audioPlaylistPlayers[0].Stop();
                _audioPlaylistPlayers.RemoveAt(0);
            }
        }

        public AudioTrackPlayer Play(AudioTrackSetup audioTrackSetup)
        {
            return Play(audioTrackSetup, null);
        }

        public AudioTrackPlayer Play(AudioTrackSetup audioTrackSetup, AudioPlaylistPlayer audioPlaylistPlayer)
        {
            if (audioTrackSetup == null)
                return null;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioTrackPlayer audioTrackPlayer = AudioTrackPlayer.Create(audioSource, audioTrackSetup, audioPlaylistPlayer);
            audioTrackPlayer.Play();
            _audioTrackPlayers.Add(audioTrackPlayer);

            return audioTrackPlayer;
        }

        public AudioPlaylistPlayer MakePlaylist(AudioPlaylistSetup audioPlaylistSetup)
        {
            if (audioPlaylistSetup == null)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = AudioPlaylistPlayer.Create(audioPlaylistSetup);
            _audioPlaylistPlayers.Add(audioPlaylistPlayer);

            return audioPlaylistPlayer;
        }

        public AudioPlaylistPlayer Play(AudioPlaylistSetup audioPlaylistSetup)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = MakePlaylist(audioPlaylistSetup);

            if (audioPlaylistPlayer != null)
                audioPlaylistPlayer.Play();

            return audioPlaylistPlayer;
        }


        public void RemovePlaylist(AudioPlaylistPlayer audioPlaylistPlayer)
        {
            if (audioPlaylistPlayer == null)
                return;

            audioPlaylistPlayer.Stop();
            _audioPlaylistPlayers.Remove(audioPlaylistPlayer);
        }
    }
}