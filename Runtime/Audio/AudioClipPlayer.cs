using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    public class AudioClipPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioClipSetup AudioClipSetup { get; private set; }
        public AudioPlaylistPlayer AudioPlaylistPlayer { get; private set; }

        public bool Running => AudioSource != null && AudioSource.isPlaying;

        private bool _stopping;
        private float _stopDuration;
        private float _time;

        public static AudioClipPlayer Create(AudioSource audioSource, AudioClipSetup audioClipSetup)
        {
            return Create(audioSource, audioClipSetup, null);
        }

        public static AudioClipPlayer Create(AudioSource audioSource, AudioClipSetup audioClipSetup, AudioPlaylistPlayer audioPlaylistPlayer)
        {
            AudioClipPlayer audioClipPlayer = new AudioClipPlayer();
            audioClipPlayer.AudioSource = audioSource;
            audioClipPlayer.AudioClipSetup = audioClipSetup;
            audioClipPlayer.AudioPlaylistPlayer = audioPlaylistPlayer;
            return audioClipPlayer;
        }

        public void Tick(float time)
        {
            if (!Running)
                return;
            if (!FadeOrStop)
                return;

            _time += time;

            if (_stopping && _time >= _stopDuration)
            {
                Stop();
                return;
            }

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
                AudioSource.time = (AudioClipSetup.AudioClip.length - 0.01f).LowerBound(0);
            else
                AudioSource.time = 0;

            ApplyAudioSetup();

            AudioSource.Play();
        }

        public void Stop()
        {
            Stop(0);
        }

        public void Stop(float stopDuration)
        {
            if (AudioSource == null)
                return;

            _stopping = true;
            _stopDuration = stopDuration;
            _time = 0;

            if (stopDuration <= 0)
                AudioSource.Stop();
        }

        public void ApplyAudioSetup()
        {
            if (AudioSource == null)
                return;

            AudioSource.clip = AudioClipSetup.AudioClip;
            AudioSource.loop = AudioClipSetup.Loop;
            AudioSource.volume = GetVolume();
            AudioSource.pitch = Pitch;
            AudioSource.outputAudioMixerGroup = AudioMixerGroup;
        }

        private float GetVolume()
        {
            if (AudioSource == null)
                return 0;

            float volume = Volume;

            if (_stopping && _stopDuration > 0)
                volume *= (1 - _time / _stopDuration).Between(0, 1);

            if (PlaylistStopping)
                volume *= AudioPlaylistPlayer.StoppingVolumeFactor;

            float audioLength = AudioClipSetup.AudioClip.length;
            float audioTime = AudioSource.time;

            if (AudioClipSetup.FadeIn && audioTime <= AudioClipSetup.FadeInDuration)
                volume *= (audioTime / AudioClipSetup.FadeInDuration).Between(0, 1);
            if (AudioClipSetup.FadeOut && audioTime + AudioClipSetup.FadeOutDuration >= audioLength)
                volume *= ((audioLength - audioTime) / AudioClipSetup.FadeOutDuration).Between(0, 1);

            if (AudioPlaylistSetup != null)
            {
                if (AudioPlaylistSetup.FadeIn && audioTime <= AudioPlaylistSetup.FadeInDuration)
                    volume *= (audioTime / AudioPlaylistSetup.FadeInDuration).Between(0, 1);
                if (AudioPlaylistSetup.FadeOut && audioTime + AudioPlaylistSetup.FadeOutDuration >= audioLength)
                    volume *= ((audioLength - audioTime) / AudioPlaylistSetup.FadeOutDuration).Between(0, 1);
            }

            return volume;
        }

        public AudioPlaylistSetup AudioPlaylistSetup => AudioPlaylistPlayer?.AudioPlaylistSetup;

        public AudioMixerGroup AudioMixerGroup => AudioPlaylistSetup?.AudioMixerGroup ?? AudioClipSetup.AudioMixerGroup;
        public float Volume => AudioClipSetup.Volume * (AudioPlaylistSetup?.Volume ?? 1);
        public float Pitch => AudioClipSetup.Pitch * (AudioPlaylistSetup?.Pitch ?? 1);
        public bool FadeIn => AudioClipSetup.FadeIn || (AudioPlaylistSetup?.FadeIn ?? false);
        public bool FadeOut => AudioClipSetup.FadeOut || (AudioPlaylistSetup?.FadeOut ?? false);
        public bool Fade => FadeIn || FadeOut;
        public bool PlaylistStopping => AudioPlaylistPlayer?.Stopping ?? false;
        public bool FadeOrStop => Fade || _stopping || PlaylistStopping;
    }
}