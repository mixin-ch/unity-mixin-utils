using UnityEngine;
using UnityEngine.Audio;

namespace Mixin.Utils
{
    public abstract class ApplicationManagerBase : Singleton<ApplicationManagerBase>
    {
        public static string GetGameVersion()
        {
            return Application.version;
        }

        public void SetQuality(int index)
        {
            QualitySettings.SetQualityLevel(index);
        }

        public void SetFramerate(int framerate)
        {
            Application.targetFrameRate = framerate;
        }

        public void SetVolume(AudioMixer mixer, float value)
        {
            mixer.SetFloat(mixer.ToString(), CalculateVolume(value));
        }

        private float CalculateVolume(float value)
        {
            float volume = Mathf.Log10(value / 100f) * 20f;
            if (value == 0) volume = -80f;
            return volume;
        }

        public static void QuitApplication()
        {
            Application.Quit();
        }
    }
}