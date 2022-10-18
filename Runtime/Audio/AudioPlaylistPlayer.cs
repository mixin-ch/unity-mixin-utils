using System.Collections.Generic;

namespace Mixin.Utils.Audio
{
    public class AudioPlaylistPlayer
    {
        public AudioPlaylistSetup AudioPlaylistSetup { get; private set; }

        public bool Running { get; private set; }

        private bool _stopping;
        private float _stopDuration;
        private float _time;

        public bool Stopping => _stopping;
        // The Volume Factor, in case of a fading Stop.
        public float StoppingVolumeFactor => Stopping ? (1 - _time / _stopDuration).Between(0, 1) : 1;

        public static AudioPlaylistPlayer Create(AudioPlaylistSetup audioPlaylistSetup)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.AudioPlaylistSetup = audioPlaylistSetup;
            return audioPlaylistPlayer;
        }

        private List<AudioTrackSetup> _audioTracksToPlay = new List<AudioTrackSetup>();
        private AudioTrackPlayer _currentAudioTrackPlayer;

        public void Tick(float time)
        {
            if (!Running)
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

        public void Play()
        {
            _stopping = false;
            _stopDuration = 0;
            _time = 0;
            Running = true;
            PlayTrack();
        }

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

        public void Stop()
        {
            Stop(0);
        }

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

        public void ApplyAudioSetup()
        {
            if (_currentAudioTrackPlayer == null)
                return;

            _currentAudioTrackPlayer.ApplyAudioSetup();
        }

        private void RefreshAudioTracksToPlay()
        {
            _audioTracksToPlay = new List<AudioTrackSetup>(AudioPlaylistSetup.AudioTrackSetups);

            if (AudioPlaylistSetup.Shuffle)
                _audioTracksToPlay.Shuffle(new System.Random());
        }
    }
}