using UnityEngine;
using System.Collections.Generic;

namespace Mixin.Utils.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private List<AudioClipPlayer> _audioClipPlayers = new List<AudioClipPlayer>();
        public List<AudioPlaylistPlayer> _audioPlaylistPlayers = new List<AudioPlaylistPlayer>();

        void Update()
        {
            for (int i = 0; i < _audioClipPlayers.Count; i++)
            {
                AudioClipPlayer audioClipPlayer = _audioClipPlayers[i];
                audioClipPlayer.Tick(Time.deltaTime);

                if (!audioClipPlayer.Running)
                {
                    Destroy(audioClipPlayer.AudioSource);
                    _audioClipPlayers.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < _audioPlaylistPlayers.Count; i++)
                _audioPlaylistPlayers[i].Tick(Time.deltaTime);
        }

        public void StopAllAudio()
        {
            while (_audioClipPlayers.Count > 0)
            {
                Destroy(_audioClipPlayers[0].AudioSource);
                _audioClipPlayers.RemoveAt(0);
            }

            while (_audioPlaylistPlayers.Count > 0)
            {
                _audioPlaylistPlayers[0].Stop();
                _audioPlaylistPlayers.RemoveAt(0);
            }
        }

        public AudioClipPlayer Play(AudioClipSetup audioClipSetup)
        {
            return Play(audioClipSetup, null);
        }

        public AudioClipPlayer Play(AudioClipSetup audioClipSetup, AudioPlaylistPlayer audioPlaylistPlayer)
        {
            if (audioClipSetup == null)
                return null;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioClipPlayer audioClipPlayer = AudioClipPlayer.Create(audioSource, audioClipSetup, audioPlaylistPlayer);
            audioClipPlayer.Play();
            _audioClipPlayers.Add(audioClipPlayer);

            return audioClipPlayer;
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