using UnityEngine;

namespace Mixin.Utils.Audio
{
    public class AudioClipPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioClipSetupSO AudioClipSetupSO { get; private set; }

        public bool Running => AudioSource != null && AudioSource.isPlaying;

        private bool _stopping;
        private float _stopDuration;
        private float _time;

        public static AudioClipPlayer Create(AudioSource audioSource, AudioClipSetupSO audioClipSetupSO)
        {
            AudioClipPlayer audioClipPlayer = new AudioClipPlayer();
            audioClipPlayer.AudioSource = audioSource;
            audioClipPlayer.AudioClipSetupSO = audioClipSetupSO;
            return audioClipPlayer;
        }

        public void Tick(float time)
        {
            if (!Running)
                return;
            if (!AudioClipSetupSO.Fade && !_stopping)
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

            if (AudioClipSetupSO.Pitch < 0)
                AudioSource.time = (AudioClipSetupSO.AudioClip.length - 0.01f).LowerBound(0);
            else
                AudioSource.time = 0;

            AudioSource.clip = AudioClipSetupSO.AudioClip;
            AudioSource.loop = AudioClipSetupSO.Loop;
            AudioSource.volume = GetVolume();
            AudioSource.pitch = AudioClipSetupSO.Pitch;
            AudioSource.outputAudioMixerGroup = AudioClipSetupSO.AudioMixerGroup;

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

            float volume = AudioClipSetupSO.Volume;

            if (AudioClipSetupSO.FadeIn && AudioSource.time <= AudioClipSetupSO.FadeInDuration)
                volume *= (AudioSource.time / AudioClipSetupSO.FadeInDuration).Between(0, 1);
            if (AudioClipSetupSO.FadeOut && AudioSource.time + AudioClipSetupSO.FadeOutDuration >= AudioClipSetupSO.AudioClip.length)
                volume *= ((AudioClipSetupSO.AudioClip.length - AudioSource.time) / AudioClipSetupSO.FadeOutDuration).Between(0, 1);
            if (_stopping && _stopDuration > 0)
                volume *= (1 - _time / _stopDuration).Between(0, 1);

            return volume;
        }
    }
}