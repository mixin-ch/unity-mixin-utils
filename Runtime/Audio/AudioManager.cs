using UnityEngine;
using System.Collections.Generic;
using Mixin.Utils;

namespace Mixin.Audio
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

        /// <inheritdoc cref="StopAllAudio_Private"/>
        public static void StopAllAudio()
        {
            Instance.StopAllAudio_Private();
        }

        /// <inheritdoc cref="Play_Private(AudioTrackSetup)"/>
        public static AudioTrackPlayer Play(AudioTrackSetup audioTrackSetup)
        {
            return Instance.Play_Private(audioTrackSetup);
        }

        /// <inheritdoc cref="Play_Private(AudioTrackSetup, AudioPlaylistPlayer)"/>
        public static AudioTrackPlayer Play(AudioTrackSetup audioTrackSetup, AudioPlaylistPlayer audioPlaylistPlayer)
        {
            return Instance.Play_Private(audioTrackSetup, audioPlaylistPlayer);
        }

        /// <inheritdoc cref="MakePlaylist_Private(AudioPlaylistSetup)"/>
        public static AudioPlaylistPlayer MakePlaylist(AudioPlaylistSetup audioPlaylistSetup)
        {
            return Instance.MakePlaylist_Private(audioPlaylistSetup);
        }

        /// <inheritdoc cref="Play_Private(AudioPlaylistSetup)"/>
        public static AudioPlaylistPlayer Play(AudioPlaylistSetup audioPlaylistSetup)
        {
            return Instance.Play_Private(audioPlaylistSetup);
        }

        /// <inheritdoc cref="RemovePlaylist_Private(AudioPlaylistPlayer)"/>
        public static void RemovePlaylist(AudioPlaylistPlayer audioPlaylistPlayer)
        {
            Instance.RemovePlaylist_Private(audioPlaylistPlayer);
        }

        /// <summary>
        /// Stops and removes all Audio Tracks and Audio Playlists.
        /// </summary>
        private void StopAllAudio_Private()
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

        /// <inheritdoc cref="Play_Private(AudioTrackSetup, AudioPlaylistPlayer)"/>
        private AudioTrackPlayer Play_Private(AudioTrackSetup audioTrackSetup)
        {
            return Play_Private(audioTrackSetup, null);
        }

        /// <summary>
        /// Plays An Audio Track with the specified Setup.
        /// </summary>
        /// <param name="audioTrackSetup">The Setup of the Audio Track.</param>
        /// <param name="audioPlaylistPlayer">The Playlist playing this Track, if existing.</param>
        /// <returns>The Player managing the Audio Track.</returns>
        private AudioTrackPlayer Play_Private(AudioTrackSetup audioTrackSetup, AudioPlaylistPlayer audioPlaylistPlayer)
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
        private AudioPlaylistPlayer MakePlaylist_Private(AudioPlaylistSetup audioPlaylistSetup)
        {
            if (audioPlaylistSetup == null)
                return null;

            AudioPlaylistPlayer audioPlaylistPlayer = AudioPlaylistPlayer.Create(audioPlaylistSetup);
            _audioPlaylistPlayers.Add(audioPlaylistPlayer);

            return audioPlaylistPlayer;
        }

        /// <summary>
        /// Plays An Audio Playlist with the specified Setup.
        /// </summary>
        /// <param name="audioPlaylistSetup">The Setup of the Audio Playlist.</param>
        /// <returns>The Player managing the Audio Playlist.</returns>
        private AudioPlaylistPlayer Play_Private(AudioPlaylistSetup audioPlaylistSetup)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = MakePlaylist_Private(audioPlaylistSetup);

            if (audioPlaylistPlayer != null)
                audioPlaylistPlayer.Play();

            return audioPlaylistPlayer;
        }

        /// <summary>
        /// Stops and Removes the given Playlist Player.
        /// </summary>
        private void RemovePlaylist_Private(AudioPlaylistPlayer audioPlaylistPlayer)
        {
            if (audioPlaylistPlayer == null)
                return;

            audioPlaylistPlayer.Stop();
            _audioPlaylistPlayers.Remove(audioPlaylistPlayer);
        }
    }
}