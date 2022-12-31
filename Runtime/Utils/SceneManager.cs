using System;
using System.Collections;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mixin.Utils
{
    public class SceneManager : Singleton<SceneManager>
    {
        private bool _isLoading;
        private bool _isUnloading;
        private int _progress;

        public bool IsLoading { get => _isLoading; }
        public bool IsUnloading { get => _isUnloading; }
        public int Progress { get => _progress; }

        public static event Action<string> OnBeforeSceneLoad;
        public static event Action<string> OnSceneLoaded;

        public static event Action<string> OnBeforeSceneUnload;
        public static event Action<string> OnSceneUnloaded;

        #region Load
        public void LoadScene(string sceneName, LoadSceneMode loadSceneMode)
        {
            StartCoroutine(LoadSceneAsync(sceneName, loadSceneMode));
        }

        public void LoadScene(string sceneName)
        {
            LoadScene(sceneName, LoadSceneMode.Single);
        }

        private IEnumerator LoadSceneAsync(string sceneName, LoadSceneMode loadSceneMode)
        {
            OnBeforeSceneLoad?.Invoke(sceneName);
            _isLoading = true;

            AsyncOperation operation =
                UnityEngine.SceneManagement.SceneManager.LoadSceneAsync(sceneName, loadSceneMode);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _progress = Mathf.RoundToInt(progress * 100f);
                yield return null;
            }

            _isLoading = false;
            OnSceneLoaded?.Invoke(sceneName);
        }
        #endregion

        #region Unload
        public void UnloadScene(string sceneName, UnloadSceneOptions unloadSceneOptions)
        {
            StartCoroutine(UnloadSceneAsync(sceneName, unloadSceneOptions));
        }

        public void UnloadScene(string sceneName)
        {
            UnloadScene(sceneName, UnloadSceneOptions.None);
        }

        public IEnumerator UnloadSceneAsync(string sceneName, UnloadSceneOptions unloadSceneOptions)
        {
            OnBeforeSceneUnload?.Invoke(sceneName);
            _isUnloading = true;

            AsyncOperation operation =
                UnityEngine.SceneManagement.SceneManager.UnloadSceneAsync(sceneName, unloadSceneOptions);

            while (!operation.isDone)
            {
                float progress = Mathf.Clamp01(operation.progress / 0.9f);
                _progress = Mathf.RoundToInt(progress * 100f);
                yield return null;
            }

            _isUnloading = false;
            OnSceneUnloaded?.Invoke(sceneName);
        }
        #endregion

        public static string GetCurrentSceneName()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }
    }
}