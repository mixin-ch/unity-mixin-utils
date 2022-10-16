using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils.Audio
{
    /// <summary>
    /// Plays and manages a single AudioClip.
    /// </summary>
    public class AudioClipPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioClipSetup AudioClipSetup { get; private set; }
        /// <summary>
        /// The PlaylistPlayer playing this clip. <br/>
        /// If this clip does not originate from a playlist, this field is null.
        /// </summary>
        public AudioPlaylistPlayer AudioPlaylistPlayer { get; private set; }

        /// <summary>
        /// Is the clip currently being played?
        /// </summary>
        public bool Running => AudioSource != null && AudioSource.isPlaying;

        /// <summary>
        /// Has the clip been ordered to stop?
        /// </summary>
        private bool _stopping;
        /// <summary>
        /// The time it takes until the clip fades to a stop.
        /// </summary>
        private float _stopDuration;
        /// <summary>
        /// The time passed since the clip was ordered to stop.
        /// </summary>
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

        /// <summary>
        /// A Method to regularly update the Player for things like fading out. 
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
        /// Start playing the clip.
        /// </summary>
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

        /// <summary>
        /// Stop the clip.
        /// </summary>
        public void Stop()
        {
            Stop(0);
        }

        /// <summary>
        /// Fades out the clip, and then stops it.
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

            AudioSource.clip = AudioClipSetup.AudioClip;
            AudioSource.loop = AudioClipSetup.Loop;
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

            // Clip stopping?
            if (_stopping && _stopDuration > 0)
                volume *= (1 - _time / _stopDuration).Between(0, 1);

            // Playlist stopping?
            if (PlaylistStopping)
                volume *= AudioPlaylistPlayer.StoppingVolumeFactor;

            float audioLength = AudioClipSetup.AudioClip.length;
            float audioTime = AudioSource.time;

            // Clip start fade?
            if (AudioClipSetup.StartFade && audioTime <= AudioClipSetup.StartFadeDuration)
                volume *= (audioTime / AudioClipSetup.StartFadeDuration).Between(0, 1);
            // Clip end fade?
            if (AudioClipSetup.EndFade && audioTime + AudioClipSetup.EndFadeDuration >= audioLength)
                volume *= ((audioLength - audioTime) / AudioClipSetup.EndFadeDuration).Between(0, 1);

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
        /// Setup of the referenced PlaylistPlayer.
        /// </summary>
        public AudioPlaylistSetup AudioPlaylistSetup => AudioPlaylistPlayer?.AudioPlaylistSetup;

        /// <summary>
        /// Takes AudioMixerGroup of the Playlist if present, otherwise the one from the Clip.
        /// </summary>
        public AudioMixerGroup AudioMixerGroup => AudioPlaylistSetup?.AudioMixerGroup ?? AudioClipSetup.AudioMixerGroup;
        /// <summary>
        /// The resulting base volume from the Clip and Playlist if present.
        /// </summary>
        public float Volume => AudioClipSetup.Volume * (AudioPlaylistSetup?.Volume ?? 1);
        /// <summary>
        /// The resulting base pitch from the Clip and Playlist if present.
        /// </summary>
        public float Pitch => AudioClipSetup.Pitch * (AudioPlaylistSetup?.Pitch ?? 1);
        /// <summary>
        /// Has the clip a fade at the start? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool StartFade => AudioClipSetup.StartFade || (AudioPlaylistSetup?.FadeIn ?? false);
        /// <summary>
        /// Has the clip a fade at the end? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool EndFade => AudioClipSetup.EndFade || (AudioPlaylistSetup?.FadeOut ?? false);
        /// <summary>
        /// Has the clip a fade at the start or end? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool BoundFade => StartFade || EndFade;
        /// <summary>
        /// Has the Playlist currently in the process of stopping?
        /// </summary>
        public bool PlaylistStopping => AudioPlaylistPlayer?.Stopping ?? false;
        /// <summary>
        /// Has the clip a fade at the start or end or is currently in the process of stopping? <br/>
        /// Also consideres the fade from the Playlist if present.
        /// </summary>
        public bool FadeOrStop => BoundFade || _stopping || PlaylistStopping;
    }
}