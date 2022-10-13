using UnityEngine;

namespace Mixin.Utils.Audio
{
    public class AudioPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioSetup AudioSetup { get; private set; }
        public bool Loop { get; private set; }

        public FadeState FadeState { get; private set; } = FadeState.Inactive;
        public bool Running => FadeState != FadeState.Inactive;

        private float _fadeDuration;
        private float _time;

        public static AudioPlayer Create(AudioSource audioSource, AudioSetup audioSetup, bool loop)
        {
            AudioPlayer audioPlayer = new AudioPlayer();
            audioPlayer.AudioSource = audioSource;
            audioPlayer.AudioSetup = audioSetup;
            audioPlayer.Loop = loop;
            return audioPlayer;
        }

        public static AudioPlayer Create(AudioSource audioSource, AudioSetup audioSetup)
        {
            return Create(audioSource, audioSetup, false);
        }

        public void Tick(float time)
        {
            if (!Running)
                return;

            if (FadeState == FadeState.Active)
                return;

            _time += time;

            if (FadeState == FadeState.FadeIn)
            {
                if (_time > _fadeDuration)
                    FadeState = FadeState.Active;

                AudioSource.volume = GetVolume();
            }
            else if (FadeState == FadeState.FadeOut)
            {
                if (_time > _fadeDuration)
                    Stop();
            }
        }

        public void Play()
        {
            FadeState = FadeState.Active;
            _fadeDuration = 0;
            _time = 0;

            SetupClip();
        }

        public void Stop()
        {
            FadeState = FadeState.Inactive;
            _fadeDuration = 0;
            _time = 0;

            AudioSource.Stop();
        }

        public void FadeIn(float fadeDuration)
        {
            FadeState = FadeState.FadeIn;
            _fadeDuration = fadeDuration;
            _time = 0;

            SetupClip();
        }

        public void FadeOut(float fadeDuration)
        {
            FadeState = FadeState.FadeOut;
            _fadeDuration = fadeDuration;
            _time = 0;

            SetupClip();
        }

        private void SetupClip()
        {
            if (!Running)
                return;

            AudioSource.clip = AudioSetup.AudioClip;
            AudioSource.loop = Loop;
            AudioSource.volume = GetVolume();
            AudioSource.pitch = AudioSetup.Pitch;
            AudioSource.outputAudioMixerGroup = AudioSetup.AudioMixerGroup;

            if (AudioSetup.Pitch < 0)
                AudioSource.time = (AudioSetup.AudioClip.length - 0.01f).LowerBound(0);

            AudioSource.Play();
        }

        private float GetVolume()
        {
            if (!Running)
                return 0;

            float volume = AudioSetup.Volume;

            if (FadeState == FadeState.FadeIn)
                volume *= (_time / _fadeDuration).Between(0, 1);
            else if (FadeState == FadeState.FadeOut)
                volume *= (1 - _time / _fadeDuration).Between(0, 1);

            return volume;
        }
    }
}