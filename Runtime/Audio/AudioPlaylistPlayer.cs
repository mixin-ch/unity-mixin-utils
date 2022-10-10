using System.Collections.Generic;
using UnityEngine;

namespace Mixin.Utils.Audio
{
    public class AudioPlaylistPlayer
    {
        private float overallPitch = 1;

        private List<AudioSetup> audioSetupList;
        private AudioSetup audioSetup;
        private AudioSource audioSource;
        private List<AudioSetup> audioSetupListToPlay;

        private bool isInitialized = false;
        private bool isActive = false;

        private bool fadingIn = false;
        private bool fadingOut = false;
        private float fadeTime = 0;
        private float fade = 0;

        public void Initialize(List<AudioSetup> audioSetupList)
        {
            this.audioSetupList = audioSetupList;
            isInitialized = true;
        }

        public void Tick(float time)
        {
            if (!isActive)
                return;
            if (!audioSource.isPlaying)
            {
                Play();
            }

            if (fadingIn)
            {
                fade = Mathf.Min(1, fade + time / fadeTime);
                audioSource.volume = audioSetup.Volume * fade;

                if (fade == 1)
                    fadingIn = false;
            }

            if (fadingOut)
            {
                fade = Mathf.Max(0, fade - time / fadeTime);
                audioSource.volume = audioSetup.Volume * fade;

                if (fade == 0)
                {
                    fadingOut = false;
                    Destroy();
                }
            }
        }

        public void Start()
        {
            if (!isInitialized)
                return;
            refreshAudioClipsToPlay();
            Play();
        }

        public void Start(float fadeTime)
        {
            if (!isInitialized)
                return;
            refreshAudioClipsToPlay();
            fadeIn(fadeTime);
        }

        public void Stop()
        {
            isActive = false;
            if (audioSource == null)
                return;
            audioSource.Stop();
        }

        private void Play()
        {
            Stop();
            if (audioSetupListToPlay.Count == 0)
                refreshAudioClipsToPlay();

            audioSetup = audioSetupListToPlay[0];
            audioSetupListToPlay.RemoveAt(0);
            AudioSetup adjustedAudioSetup = audioSetup.Copy();
            adjustedAudioSetup.Pitch *= overallPitch;
            audioSource = AudioManager.Instance.Play(audioSetup, false);
            isActive = true;
        }

        public void SetOverallPitch(float pitch)
        {
            overallPitch = pitch;
            refreshCurrentAudio();
        }

        private void refreshCurrentAudio()
        {
            if (audioSetup == null)
                return;
            if (audioSource == null)
                return;

            audioSource.pitch = audioSetup.Pitch * overallPitch;
        }

        private void fadeIn(float fadeTime)
        {
            fadingIn = true;
            fadingOut = false;
            fade = 0;
            this.fadeTime = fadeTime;
            Play();
        }

        public void FadeOut(float fadeTime)
        {
            fadingIn = false;
            fadingOut = true;
            fade = 1;
            this.fadeTime = fadeTime;
        }

        public void Destroy()
        {
            Stop();
            AudioManager.Instance.Remove(this);
        }

        private void refreshAudioClipsToPlay()
        {
            audioSetupListToPlay = new List<AudioSetup>(audioSetupList);
            audioSetupListToPlay.Shuffle(new System.Random());
        }
    }
}