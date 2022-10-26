using Mixin.Utils;
using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Audio
{
    /// <summary>
    /// Plays and manages a single Audio Track.
    /// </summary>
    public class AudioTrackPlayer
    {
        public AudioSource AudioSource { get; private set; }

        public AudioTrackSetup AudioTrackSetup { get; private set; }

        /// <summary>
        /// The Playlist Player playing this Track. <br/>
        /// If this Track does not originate from a Playlist, this field is null.
        /// </summary>
        public AudioPlaylistPlayer AudioPlaylistPlayer { get; private set; }

        /// <summary>
        /// Is the Track currently being played?
        /// </summary>
        public bool Running => AudioSource != null && AudioSource.isPlaying;

        /// <summary>
        /// Has the Track Player been ordered to stop?
        /// </summary>
        private bool _stopping;

        /// <summary>
        /// The time it takes until the Track Player fades to a stop.
        /// </summary>
        private float _stopDuration;

        /// <summary>
        /// The time passed since the Track Player was ordered to stop.
        /// </summary>
        private float _time;

        public static AudioTrackPlayer Create(AudioSource audioSource, AudioTrackSetup audioTrackSetup)
        {
            return Create(audioSource, audioTrackSetup, null);
        }

        public static AudioTrackPlayer Create(AudioSource audioSource, AudioTrackSetup audioTrackSetup, AudioPlaylistPlayer audioPlaylistPlayer)
        {
            AudioTrackPlayer audioTrackPlayer = new AudioTrackPlayer();
            audioTrackPlayer.AudioSource = audioSource;
            audioTrackPlayer.AudioTrackSetup = audioTrackSetup;
            audioTrackPlayer.AudioPlaylistPlayer = audioPlaylistPlayer;
            return audioTrackPlayer;
        }

        /// <summary>
        /// A method to regularly update the Player for things like fading out. 
        /// </summary>
        /// <param name="time">The time passed since the last tick.</param>
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

        /// <summary>
        /// Start playing the Track.
        /// </summary>
        public void Play()
        {
            if (AudioSource == null)
                return;

            _stopping = false;
            _stopDuration = 0;
            _time = 0;

            if (Pitch < 0)
                AudioSource.time = (AudioTrackSetup.AudioClip.length - 0.01f).LowerBound(0);
            else
                AudioSource.time = 0;

            ApplyAudioSetup();

            AudioSource.Play();
        }

        /// <summary>
        /// Stop the Track.
        /// </summary>
        public void Stop()
        {
            Stop(0);
        }

        /// <summary>
        /// Fades out the Track, and then stops it.
        /// </summary>
        /// <param name="stopDuration">Duration of the fading until the stop.</param>
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

        /// <summary>
        /// Applies all the values to the AudioSource, if currently playing.
        /// </summary>
        public void ApplyAudioSetup()
        {
            if (AudioSource == null)
                return;

            AudioSource.clip = AudioTrackSetup.AudioClip;
            AudioSource.loop = AudioTrackSetup.Loop;
            AudioSource.volume = GetVolume();
            AudioSource.pitch = Pitch;
            AudioSource.outputAudioMixerGroup = AudioMixerGroup;
        }

        /// <summary>
        /// Calculates the resulting volume from all influences.
        /// </summary>
        private float GetVolume()
        {
            if (AudioSource == null)
                return 0;

            float volume = Volume;

            // Track stopping?
            if (_stopping && _stopDuration > 0)
                volume *= (1 - _time / _stopDuration).Between(0, 1);

            // Playlist stopping?
            if (PlaylistStopping)
                volume *= AudioPlaylistPlayer.StoppingVolumeFactor;

            float audioLength = AudioTrackSetup.AudioClip.length;
            float audioTime = AudioSource.time;

            // Track start fade?
            if (AudioTrackSetup.StartFade && audioTime <= AudioTrackSetup.StartFadeDuration)
                volume *= (audioTime / AudioTrackSetup.StartFadeDuration).Between(0, 1);
            // Track end fade?
            if (AudioTrackSetup.EndFade && audioTime + AudioTrackSetup.EndFadeDuration >= audioLength)
                volume *= ((audioLength - audioTime) / AudioTrackSetup.EndFadeDuration).Between(0, 1);

            if (AudioPlaylistSetup != null)
            {
                // Playlist start fade?
                if (AudioPlaylistSetup.FadeIn && audioTime <= AudioPlaylistSetup.FadeInDuration)
                    volume *= (audioTime / AudioPlaylistSetup.FadeInDuration).Between(0, 1);
                // Playlist end fade?
                if (AudioPlaylistSetup.FadeOut && audioTime + AudioPlaylistSetup.FadeOutDuration >= audioLength)
                    volume *= ((audioLength - audioTime) / AudioPlaylistSetup.FadeOutDuration).Between(0, 1);
            }

            return volume;
        }

        /// <summary>
        /// Setup of the referenced Playlist Player.
        /// </summary>
        public AudioPlaylistSetup AudioPlaylistSetup => AudioPlaylistPlayer?.AudioPlaylistSetup;

        /// <summary>
        /// Takes AudioMixerGroup of the Playlist if present, otherwise the one from the Track.
        /// </summary>
        public AudioMixerGroup AudioMixerGroup => AudioPlaylistSetup?.AudioMixerGroup ?? AudioTrackSetup.AudioMixerGroup;
        /// <summary>
        /// The resulting base volume from the Track and Playlist if present.
        /// </summary>
        public float Volume => AudioTrackSetup.Volume * (AudioPlaylistSetup?.Volume ?? 1);
        /// <summary>
        /// The resulting base pitch from the Track and Playlist if present.
        /// </summary>
        public float Pitch => AudioTrackSetup.Pitch * (AudioPlaylistSetup?.Pitch ?? 1);
        /// <summary>
        /// Has the Track a fade at the start? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool StartFade => AudioTrackSetup.StartFade || (AudioPlaylistSetup?.FadeIn ?? false);
        /// <summary>
        /// Has the Track a fade at the end? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool EndFade => AudioTrackSetup.EndFade || (AudioPlaylistSetup?.FadeOut ?? false);
        /// <summary>
        /// Has the Track a fade at the start or end? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool BoundFade => StartFade || EndFade;
        /// <summary>
        /// Has the Playlist currently in the process of stopping?
        /// </summary>
        public bool PlaylistStopping => AudioPlaylistPlayer?.Stopping ?? false;
        /// <summary>
        /// Has the Track a fade at the start or end or is currently in the process of stopping? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool FadeOrStop => BoundFade || _stopping || PlaylistStopping;
    }
}