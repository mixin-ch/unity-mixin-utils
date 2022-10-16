using System.Collections.Generic;

namespace Mixin.Utils.Audio
{
    public class AudioPlaylistPlayer
    {
        public List<AudioSetup> AudioSetups { get; private set; }

        public bool Running { get; private set; }

        private List<AudioSetup> _audioSetupListToPlay;
        private AudioPlayer _currentAudioPlayer;

        public static AudioPlaylistPlayer Create(List<AudioSetup> audioSetups)
        {
            AudioPlaylistPlayer audioPlaylistPlayer = new AudioPlaylistPlayer();
            audioPlaylistPlayer.AudioSetups = audioSetups;
            return audioPlaylistPlayer;
        }

        public void Tick()
        {
            if (!Running)
                return;
            if (_currentAudioPlayer != null && _currentAudioPlayer.Running)
                return;

            if (_audioSetupListToPlay.Count == 0)
                RefreshAudioClipsToPlay();

            AudioSetup audioSetup = _audioSetupListToPlay[0];
            _audioSetupListToPlay.RemoveAt(0);
            _currentAudioPlayer = AudioManager.Instance.Play(audioSetup);
        }

        public void Play()
        {
            if (Running)
                return;

            Running = true;
            Tick();
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
            _audioSetupListToPlay = new List<AudioSetup>(AudioSetups);
            _audioSetupListToPlay.Shuffle(new System.Random());
        }
    }
}