using UnityEngine;

namespace Mixin.Utils.Audio
{
    public class AudioPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioSetup AudioSetup { get; private set; }

        public FadeState FadeState { get; private set; } = FadeState.Inactive;
        public bool Running => AudioSource.isPlaying;

        private float _fadeDuration;
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
            if (FadeState == FadeState.Inactive)
                return;

            if (FadeState == FadeState.Active)
            {
                if (AudioSetup.FadeOut && AudioSource.time + AudioSetup.FadeOutDuration >= AudioSetup.AudioClip.length)
                {
                    FadeState = FadeState.FadeOut;
                    _fadeDuration = AudioSetup.FadeOutDuration;
                    _time = 0;
                }

                return;
            }
            else
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

                AudioSource.volume = GetVolume();
            }
        }

        public void Play()
        {
            if (AudioSetup.FadeIn)
            {
                FadeState = FadeState.FadeIn;
                _fadeDuration = AudioSetup.FadeInDuration;
            }
            else
            {
                FadeState = FadeState.Active;
                _fadeDuration = 0;
            }

            _time = 0;

            AudioSource.clip = AudioSetup.AudioClip;
            AudioSource.loop = AudioSetup.Loop;
            AudioSource.volume = GetVolume();
            AudioSource.pitch = AudioSetup.Pitch;
            AudioSource.outputAudioMixerGroup = AudioSetup.AudioMixerGroup;

            if (AudioSetup.Pitch < 0)
                AudioSource.time = (AudioSetup.AudioClip.length - 0.01f).LowerBound(0);
            else
                AudioSource.time = 0;


            AudioSource.Play();
        }

        public void Stop()
        {
            FadeState = FadeState.Inactive;
            _fadeDuration = 0;
            _time = 0;

            AudioSource.Stop();
        }

        public void FadeOut(float fadeDuration)
        {
            if (FadeState != FadeState.Active)
                return;

            FadeState = FadeState.FadeOut;
            _fadeDuration = fadeDuration;
            _time = 0;
        }

        private float GetVolume()
        {
            if (FadeState == FadeState.Inactive)
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