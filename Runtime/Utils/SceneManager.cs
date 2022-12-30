using System;
using System.Collections;
using UnityEngine;

namespace Mixin.Utils
{
    public class SceneManager : Singleton<SceneManager>
    {
        private int _progress;

        public static event Action<string> BeforeSceneLoad;
        public static event Action<string> OnSceneLoaded;

        public IEnumerator LoadNewSceneAsync(string sceneName)
        {
            BeforeSceneLoad?.Invoke(sceneName);

            AsyncOperation operation = 
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _progress = Mathf.RoundToInt(progress * 100f);
                yield return null;
            }

            OnSceneLoaded?.Invoke(sceneName);
        }

        public int GetProgress()
        {
            return _progress;
        }

        public static string GetCurrentSceneName()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }
}