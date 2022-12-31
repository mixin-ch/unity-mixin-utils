using Mixin.Utils;
using System.Collections.Generic;

namespace Mixin.Audio
{
    /// <summary>
    /// Plays and manages a single Audio Playlist.
    /// </summary>
    public class AudioPlaylistPlayer
    {
        public AudioPlaylistSetup AudioPlaylistSetup { get; private set; }

        /// <summary>
        /// Is the Playlist currently being played?
        /// </summary>
        public bool Running { get; private set; }

        private bool _paused;
        /// <summary>
        /// Is the Playlist currently paused?
        /// </summary>
        public bool Paused { get => _paused; }

        /// <summary>
        /// Has the Playlist Player been ordered to stop?
        /// </summary>
        private bool _stopping;

        /// <inheritdoc cref="_stopping"/>
        public bool Stopping => _stopping;

        /// <summary>
        /// The time it takes until the Playlist Player fades to a stop.
        /// </summary>
        private float _stopDuration;

        /// <summary>
        /// The time passed since the Playlist Player was ordered to stop.
        /// </summary>
        private float _time;

        /// <summary>
        /// The Volume Factor, in case of a fading Stop.
        /// </summary>
        public float StoppingVolumeFactor => Stopping ? (1 - _time / _stopDuration).Between(0, 1) : 1;

        /// <summary>
        /// The list of Tracks the Playlist will play through in order.
        /// </summary>
        private List<AudioTrackSetup> _audioTracksToPlay = new List<AudioTrackSetup>();

        /// <summary>
        /// The AudioTrackPlayer that is playing. <br/>
        /// This is null if no Track is playing.
        /// </summary>
        private AudioTrackPlayer _currentAudioTrackPlayer;

        internal static AudioPlaylistPlayer Create(AudioPlaylistSetup audioPlaylistSetup)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.AudioPlaylistSetup = audioPlaylistSetup;
            return audioPlaylistPlayer;
        }

        /// <summary>
        /// A method to regularly update the Player for things like fading out. 
        /// </summary>
        /// <param name="time">The time passed since the last tick.</param>
        public void Tick(float time)
        {
            if (!Running)
                return;
            if (_paused)
                return;

            if (_stopping)
            {
                _time += time;

                if (_time >= _stopDuration)
                {
                    Stop();
                    return;
                }
            }

            // If not Running
            if (!_currentAudioTrackPlayer?.Running ?? true)
            {
                if (!AudioPlaylistSetup.Automatic)
                {
                    Running = false;
                    return;
                }

                PlayTrack();
            }
        }

        /// <summary>
        /// Start playing the Playlist.
        /// </summary>
        public void Play()
        {
            _stopping = false;
            _paused = false;
            _stopDuration = 0;
            _time = 0;
            Running = true;
            PlayTrack();
        }

        /// <summary>
        /// Start playing the next Track in the Playlist.
        /// </summary>
        private void PlayTrack()
        {
            if (_currentAudioTrackPlayer != null && _currentAudioTrackPlayer.Running)
                _currentAudioTrackPlayer.Stop();

            if (_audioTracksToPlay.Count == 0)
                RefreshAudioTracksToPlay();

            AudioTrackSetup audioTrackSetup = _audioTracksToPlay[0];
            _audioTracksToPlay.RemoveAt(0);
            _currentAudioTrackPlayer = AudioManager.Instance.Play(audioTrackSetup, this);
        }

        /// <summary>
        /// Stop the Playlist.
        /// </summary>
        public void Stop()
        {
            Stop(0);
        }

        /// <summary>
        /// Fades out the Playlist, and then stops it.
        /// </summary>
        /// <param name="stopDuration">Duration of the fading until the stop.</param>
        public void Stop(float stopDuration)
        {
            if (!Running)
                return;

            if (_currentAudioTrackPlayer == null)
                return;

            _stopping = true;
            _stopDuration = stopDuration;
            _time = 0;

            if (stopDuration <= 0)
            {
                _currentAudioTrackPlayer.Stop();
                Running = false;
            }
        }

        /// <summary>
        /// Pause the Playlist.
        /// </summary>
        public void Pause()
        {
            _paused = true;

            if (_currentAudioTrackPlayer == null)
                return;

            _currentAudioTrackPlayer.Stop();
        }

        /// <summary>
        /// Unpause the Playlist.
        /// </summary>
        public void Unpause()
        {
            if (!Running)
                return;

            _paused = false;

            if (_currentAudioTrackPlayer == null)
                return;

            _currentAudioTrackPlayer.Play();
        }

        /// <summary>
        /// Pause or unpause the Playlist.
        /// </summary>
        public void Toggle()
        {
            if (_paused)
                Unpause();
            else
                Pause();
        }

        /// <summary>
        /// Applies all the values to the AudioSource of the current Track, if currently playing.
        /// </summary>
        public void ApplyAudioSetup()
        {
            if (_currentAudioTrackPlayer == null)
                return;

            _currentAudioTrackPlayer.ApplyAudioSetup();
        }

        /// <summary>
        /// Refreshes the list of Tracks the Playlist will play through in order.
        /// </summary>
        private void RefreshAudioTracksToPlay()
        {
            _audioTracksToPlay = new List<AudioTrackSetup>(AudioPlaylistSetup.AudioTrackSetups);

            if (AudioPlaylistSetup.Shuffle)
                _audioTracksToPlay.Shuffle(new System.Random());
        }
    }
}