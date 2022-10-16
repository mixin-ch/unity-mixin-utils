using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    public class AudioClipPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioClipSetupSO AudioClipSetupSO { get; private set; }
        public AudioPlaylistSetupSO AudioPlaylistSetupSO { get; private set; }

        public bool Running => AudioSource != null && AudioSource.isPlaying;

        private bool _stopping;
        private float _stopDuration;
        private float _time;

        public static AudioClipPlayer Create(AudioSource audioSource, AudioClipSetupSO audioClipSetupSO)
        {
            return Create(audioSource, audioClipSetupSO, null);
        }

        public static AudioClipPlayer Create(AudioSource audioSource, AudioClipSetupSO audioClipSetupSO, AudioPlaylistSetupSO audioPlaylistSetupSO)
        {
            AudioClipPlayer audioClipPlayer = new AudioClipPlayer();
            audioClipPlayer.AudioSource = audioSource;
            audioClipPlayer.AudioClipSetupSO = audioClipSetupSO;
            audioClipPlayer.AudioPlaylistSetupSO = audioPlaylistSetupSO;
            return audioClipPlayer;
        }

        public void Tick(float time)
        {
            if (!Running)
                return;
            if (!Fade && !_stopping)
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

            if (Pitch < 0)
                AudioSource.time = (AudioClipSetupSO.AudioClip.length - 0.01f).LowerBound(0);
            else
                AudioSource.time = 0;

            AudioSource.clip = AudioClipSetupSO.AudioClip;
            AudioSource.loop = AudioClipSetupSO.Loop;
            AudioSource.volume = GetVolume();
            AudioSource.pitch = Pitch;
            AudioSource.outputAudioMixerGroup = AudioMixerGroup;

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

            float volume = Volume;

            if (_stopping && _stopDuration > 0)
                volume *= (1 - _time / _stopDuration).Between(0, 1);

            float audioLength = AudioClipSetupSO.AudioClip.length;
            float audioTime = AudioSource.time;

            if (AudioClipSetupSO.FadeIn && audioTime <= AudioClipSetupSO.FadeInDuration)
                volume *= (audioTime / AudioClipSetupSO.FadeInDuration).Between(0, 1);
            if (AudioClipSetupSO.FadeOut && audioTime + AudioClipSetupSO.FadeOutDuration >= audioLength)
                volume *= ((audioLength - audioTime) / AudioClipSetupSO.FadeOutDuration).Between(0, 1);

            if (AudioPlaylistSetupSO != null)
            {
                if (AudioPlaylistSetupSO.FadeIn && audioTime <= AudioPlaylistSetupSO.FadeInDuration)
                    volume *= (audioTime / AudioPlaylistSetupSO.FadeInDuration).Between(0, 1);
                if (AudioPlaylistSetupSO.FadeOut && audioTime + AudioPlaylistSetupSO.FadeOutDuration >= audioLength)
                    volume *= ((audioLength - audioTime) / AudioPlaylistSetupSO.FadeOutDuration).Between(0, 1);
            }

            return volume;
        }

        public AudioMixerGroup AudioMixerGroup => AudioPlaylistSetupSO?.AudioMixerGroup ?? AudioClipSetupSO.AudioMixerGroup;
        public float Volume => AudioClipSetupSO.Volume * AudioPlaylistSetupSO?.Volume ?? 1;
        public float Pitch => AudioClipSetupSO.Pitch * AudioPlaylistSetupSO?.Pitch ?? 1;
        public bool FadeIn => AudioClipSetupSO.FadeIn || (AudioPlaylistSetupSO?.FadeIn ?? false);
        public bool FadeOut => AudioClipSetupSO.FadeOut || (AudioPlaylistSetupSO?.FadeOut ?? false);
        public bool Fade => FadeIn || FadeOut;
    }
}