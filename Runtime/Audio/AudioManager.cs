using UnityEngine;
using System.Collections.Generic;

namespace Mixin.Utils.Audio
{
    public class AudioManager : Singleton<AudioManager>
    {
        private List<AudioPlayer> _audioPlayers = new List<AudioPlayer>();
        public List<AudioPlaylistPlayer> _audioPlaylistPlayers = new List<AudioPlaylistPlayer>();

        void Update()
        {
            for (int i = 0; i < _audioPlayers.Count; i++)
            {
                AudioPlayer audioPlayer = _audioPlayers[i];
                if (!audioPlayer.Running)
                {
                    Destroy(audioPlayer.AudioSource);
                    _audioPlayers.RemoveAt(i);
                    i--;
                }
            }

            for (int i = 0; i < _audioPlaylistPlayers.Count; i++)
                _audioPlaylistPlayers[i].Tick();
        }

        public void StopAllAudio()
        {
            while (_audioPlayers.Count > 0)
            {
                Destroy(_audioPlayers[0].AudioSource);
                _audioPlayers.RemoveAt(0);
            }

            while (_audioPlaylistPlayers.Count > 0)
            {
                _audioPlaylistPlayers[0].Stop();
                _audioPlaylistPlayers.RemoveAt(0);
            }
        }

        public AudioPlayer Play(AudioSetup audioSetup)
        {
            return Play(audioSetup, false);
        }

        public AudioPlayer Play(AudioSetup audioSetup, bool loop)
        {
            if (audioSetup == null)
                return null;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioPlayer audioPlayer = AudioPlayer.Create(audioSource, audioSetup, loop);
            audioPlayer.Play();
            _audioPlayers.Add(audioPlayer);

            return audioPlayer;
        }

        public AudioPlaylistPlayer MakePlaylist(List<AudioSetup> audioSetups)
        {
            if (audioSetups == null)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = AudioPlaylistPlayer.Create(audioSetups);
            _audioPlaylistPlayers.Add(audioPlaylistPlayer);

            return audioPlaylistPlayer;
        }

        public AudioPlaylistPlayer PlayPlaylist(List<AudioSetup> audioSetups)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = MakePlaylist(audioSetups);

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