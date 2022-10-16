using UnityEngine;

namespace Mixin.Utils.Audio
{
    public class AudioPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioSetup AudioSetup { get; private set; }

        public bool Running => AudioSource != null && AudioSource.isPlaying;

        private bool _stopping;
        private float _stopDuration;
        private float _time;

        public static AudioPlayer Create(AudioSource audioSource, AudioSetup audioSetup)
        {
            AudioPlayer audioPlayer = new AudioPlayer();
            audioPlayer.AudioSource = audioSource;
            audioPlayer.AudioSetup = audioSetup;
            return audioPlayer;
        }

        public void Tick(float time)
        {
            if (!Running)
                return;
            if (!AudioSetup.Fade && !_stopping)
                return;

            _time += time;
            AudioSource.volume = GetVolume();
        }

        public void Play()
        {
            if (AudioSource == null)
                return;

            _stopping = false;
            _stopDuration = 0;
            _time = 0;

            if (AudioSetup.Pitch < 0)
                AudioSource.time = (AudioSetup.AudioClip.length - 0.01f).LowerBound(0);
            else
                AudioSource.time = 0;

            AudioSource.clip = AudioSetup.AudioClip;
            AudioSource.loop = AudioSetup.Loop;
            AudioSource.volume = GetVolume();
            AudioSource.pitch = AudioSetup.Pitch;
            AudioSource.outputAudioMixerGroup = AudioSetup.AudioMixerGroup;

            AudioSource.Play();
        }

        public void Stop()
        {
            Stop(0);
        }

        public void Stop(float fadeDuration)
        {
            if (AudioSource == null)
                return;

            _stopping = true;
            _stopDuration = fadeDuration;
            _time = 0;

            if (fadeDuration == 0)
                AudioSource.Stop();
        }

        private float GetVolume()
        {
            if (AudioSource == null)
                return 0;

            float volume = AudioSetup.Volume;

            if (AudioSetup.FadeIn && AudioSource.time <= AudioSetup.FadeInDuration)
                volume *= (AudioSource.time / AudioSetup.FadeInDuration).Between(0, 1);
            if (AudioSetup.FadeOut && AudioSource.time + AudioSetup.FadeOutDuration >= AudioSetup.AudioClip.length)
                volume *= ((AudioSetup.AudioClip.length - AudioSource.time) / AudioSetup.FadeOutDuration).Between(0, 1);
            if (_stopping && _stopDuration > 0)
                volume *= (1 - _time / _stopDuration).Between(0, 1);

            return volume;
        }
    }
}