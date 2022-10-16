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

        private List<AudioClipSetup> _audioClipsToPlay = new List<AudioClipSetup>();
        private AudioClipPlayer _currentAudioClipPlayer;

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
            if (!_currentAudioClipPlayer?.Running ?? true)
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
            if (_currentAudioClipPlayer != null && _currentAudioClipPlayer.Running)
                _currentAudioClipPlayer.Stop();

            if (_audioClipsToPlay.Count == 0)
                RefreshAudioClipsToPlay();

            AudioClipSetup audioClipSetup = _audioClipsToPlay[0];
            _audioClipsToPlay.RemoveAt(0);
            _currentAudioClipPlayer = AudioManager.Instance.Play(audioClipSetup, this);
        }

        public void Stop()
        {
            Stop(0);
        }

        public void Stop(float stopDuration)
        {
            if (!Running)
                return;

            if (_currentAudioClipPlayer == null)
                return;

            _stopping = true;
            _stopDuration = stopDuration;
            _time = 0;

            if (stopDuration <= 0)
            {
                _currentAudioClipPlayer.Stop();
                Running = false;
            }
        }

        public void ApplyAudioSetup()
        {
            if (_currentAudioClipPlayer == null)
                return;

            _currentAudioClipPlayer.ApplyAudioSetup();
        }

        private void RefreshAudioClipsToPlay()
        {
            _audioClipsToPlay = new List<AudioClipSetup>(AudioPlaylistSetup.AudioClipSetups);

            if (AudioPlaylistSetup.Shuffle)
                _audioClipsToPlay.Shuffle(new System.Random());
        }
    }
}