using UnityEngine;
using System.Collections.Generic;
using Mixin.Utils;

namespace Mixin.Utils.Audio
{
    /// <summary>
    /// Singleton for managing the playing of various Audio Tracks and Playlists.
    /// </summary>
    public class AudioManager : Singleton<AudioManager>
    {
        private List<AudioTrackPlayer> _audioTrackPlayers = new List<AudioTrackPlayer>();
        private List<AudioPlaylistPlayer> _audioPlaylistPlayers = new List<AudioPlaylistPlayer>();

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

        /// <summary>
        /// Stops and removes all Audio Tracks and Audio Playlists.
        /// </summary>
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

        /// <inheritdoc cref="PlayTrack(AudioTrackSetup, AudioPlaylistPlayer)"/>
        public AudioTrackPlayer PlayTrack(AudioTrackSetup audioTrackSetup)
        {
            return PlayTrack(audioTrackSetup, null);
        }

        /// <inheritdoc cref="PlayTrack(AudioTrackSetup)"/>
        public AudioTrackPlayer PlayTrack(AudioTrackSetupSO audioTrackSetup)
        {
            return PlayTrack(audioTrackSetup.ToAudioTrackSetup());
        }

        /// <summary>
        /// Plays An Audio Track with the specified Setup.
        /// </summary>
        /// <param name="audioTrackSetup">The Setup of the Audio Track.</param>
        /// <param name="audioPlaylistPlayer">The Playlist playing this Track, if existing.</param>
        /// <returns>The Player managing the Audio Track.</returns>
        internal AudioTrackPlayer PlayTrack(AudioTrackSetup audioTrackSetup, AudioPlaylistPlayer audioPlaylistPlayer)
        {
            if (audioTrackSetup == null)
                return null;

            AudioSource audioSource = gameObject.AddComponent<AudioSource>();
            AudioTrackPlayer audioTrackPlayer = AudioTrackPlayer.Create(audioSource, audioTrackSetup, audioPlaylistPlayer);
            audioTrackPlayer.Play();
            _audioTrackPlayers.Add(audioTrackPlayer);

            return audioTrackPlayer;
        }

        /// <summary>
        /// Makes a Playlist Player with the specified Setup.
        /// </summary>
        /// <param name="audioPlaylistSetup">The Setup of the Audio Playlist.</param>
        /// <returns>The Player managing the Audio Playlist.</returns>
        public AudioPlaylistPlayer MakePlaylistPlayer(AudioPlaylistSetup audioPlaylistSetup)
        {
            if (audioPlaylistSetup == null)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = AudioPlaylistPlayer.Create(audioPlaylistSetup);
            _audioPlaylistPlayers.Add(audioPlaylistPlayer);

            return audioPlaylistPlayer;
        }

        /// <inheritdoc cref="MakePlaylistPlayer(AudioPlaylistSetup)"/>
        public AudioPlaylistPlayer MakePlaylistPlayer(AudioPlaylistSetupSO audioPlaylistSetup)
        {
            return MakePlaylistPlayer(audioPlaylistSetup.ToAudioPlaylistSetup());
        }

        /// <summary>
        /// Plays An Audio Playlist with the specified Setup.
        /// </summary>
        /// <param name="audioPlaylistSetup">The Setup of the Audio Playlist.</param>
        /// <returns>The Player managing the Audio Playlist.</returns>
        public AudioPlaylistPlayer PlayPlaylist(AudioPlaylistSetup audioPlaylistSetup)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = MakePlaylistPlayer(audioPlaylistSetup);

            if (audioPlaylistPlayer != null)
                audioPlaylistPlayer.Play();

            return audioPlaylistPlayer;
        }

        /// <inheritdoc cref="PlayPlaylist(AudioPlaylistSetup)"/>
        public AudioPlaylistPlayer PlayPlaylist(AudioPlaylistSetupSO audioPlaylistSetup)
        {
            return PlayPlaylist(audioPlaylistSetup.ToAudioPlaylistSetup());
        }

        /// <summary>
        /// Stops and Removes the given Playlist Player.
        /// </summary>
        public void RemovePlaylist(AudioPlaylistPlayer audioPlaylistPlayer)
        {
            if (audioPlaylistPlayer == null)
                return;

            audioPlaylistPlayer.Stop();
            _audioPlaylistPlayers.Remove(audioPlaylistPlayer);
        }
    }
}