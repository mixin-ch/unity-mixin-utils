using System.Collections.Generic;

namespace Mixin.Utils.Audio
{
    public class AudioPlaylistPlayer
    {
        public AudioPlaylistSetup AudioPlaylistSetup { get; private set; }

        public bool Running { get; private set; }

        public static AudioPlaylistPlayer Create(AudioPlaylistSetup audioPlaylistSetup)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.AudioPlaylistSetup = audioPlaylistSetup;
            return audioPlaylistPlayer;
        }

        private List<AudioSetupSO> _audioSetupListToPlay = new List<AudioSetupSO>();
        private AudioPlayer _currentAudioPlayer;

        public void Tick()
        {
            if (!Running)
                return;
            if (_currentAudioPlayer != null && _currentAudioPlayer.Running)
                return;
            if (!AudioPlaylistSetup.Automatic)
            {
                Running = false;
                return;
            }

            PlayTrack();
        }

        public void Play()
        {
            Running = true;
            PlayTrack();
        }

        private void PlayTrack()
        {
            if (_currentAudioPlayer != null && _currentAudioPlayer.Running)
                _currentAudioPlayer.Stop();

            if (_audioSetupListToPlay.Count == 0)
                RefreshAudioClipsToPlay();

            AudioSetupSO audioSetup = _audioSetupListToPlay[0];
            _audioSetupListToPlay.RemoveAt(0);
            _currentAudioPlayer = AudioManager.Instance.Play(audioSetup);
        }

        public void Stop()
        {
            if (!Running)
                return;

            if (_currentAudioPlayer == null)
                return;

            _currentAudioPlayer.Stop();
            Running = false;
        }

        private void RefreshAudioClipsToPlay()
        {
            _audioSetupListToPlay = AudioPlaylistSetup.GenerateAudioSetups();

            if (AudioPlaylistSetup.Shuffle)
                _audioSetupListToPlay.Shuffle(new System.Random());
        }
    }
}