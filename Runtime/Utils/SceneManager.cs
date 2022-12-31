using System;
using System.Collections;
using UnityEngine;

namespace Mixin.Utils
{
    public class SceneManager : Singleton<SceneManager>
    {
        private bool _isLoading;
        private int _progress;

        public bool IsLoading { get => _isLoading;  }
        public int Progress { get => _progress;}

        public static event Action<string> OnBeforeSceneLoad;
        public static event Action<string> OnSceneLoaded;

        public IEnumerator LoadNewSceneAsync(string sceneName)
        {
            OnBeforeSceneLoad?.Invoke(sceneName);
            _isLoading = true;

            AsyncOperation operation = 
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _progress = Mathf.RoundToInt(progress * 100f);
                yield return null;
            }

            _isLoading = false;
            OnSceneLoaded?.Invoke(sceneName);
        }

        public static string GetCurrentSceneName()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }
}