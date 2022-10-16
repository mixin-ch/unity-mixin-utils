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

        private List<AudioClipSetupSO> _audioClipsToPlay = new List<AudioClipSetupSO>();
        private AudioClipPlayer _currentAudioClipPlayer;

        public void Tick()
        {
            if (!Running)
                return;
            if (_currentAudioClipPlayer != null && _currentAudioClipPlayer.Running)
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
            if (_currentAudioClipPlayer != null && _currentAudioClipPlayer.Running)
                _currentAudioClipPlayer.Stop();

            if (_audioClipsToPlay.Count == 0)
                RefreshAudioClipsToPlay();

            AudioClipSetupSO audioClipSetupSO = _audioClipsToPlay[0];
            _audioClipsToPlay.RemoveAt(0);
            _currentAudioClipPlayer = AudioManager.Instance.Play(audioClipSetupSO);
        }

        public void Stop()
        {
            if (!Running)
                return;

            if (_currentAudioClipPlayer == null)
                return;

            _currentAudioClipPlayer.Stop();
            Running = false;
        }

        private void RefreshAudioClipsToPlay()
        {
            _audioClipsToPlay = AudioPlaylistSetup.GenerateClipAudioSetupSOs();

            if (AudioPlaylistSetup.Shuffle)
                _audioClipsToPlay.Shuffle(new System.Random());
        }
    }
}