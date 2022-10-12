using UnityEngine;

namespace Mixin.Utils.Audio
{
    public class AudioPlayer
    {
        public AudioSource AudioSource { get; private set; }
        public AudioSetup AudioSetup { get; private set; }
        public bool Loop { get; private set; }

        public bool Running { get; private set; }

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

        public void Play()
        {
            if (Running)
                return;

            AudioSource.clip = AudioSetup.AudioClip;
            AudioSource.loop = Loop;
            AudioSource.volume = AudioSetup.Volume;
            AudioSource.pitch = AudioSetup.Pitch;
            AudioSource.outputAudioMixerGroup = AudioSetup.AudioMixerGroup;

            if (AudioSetup.Pitch < 0)
                AudioSource.time = (AudioSetup.AudioClip.length - 0.01f).LowerBound(0);

            AudioSource.Play();
            Running = true;
        }

        public void Stop()
        {
            if (!Running)
                return;

            AudioSource.Stop();
            Running = false;
        }
    }
}