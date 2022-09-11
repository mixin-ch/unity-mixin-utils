using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace Mixin
{
    public class SceneManager : Singleton
    {
        public static event Action BeforeSceneLoad;
        public static event Action OnSceneLoaded;


        public static string GetCurrentScene()
        {
            return UnityEngine.SceneManagement.SceneManager.GetActiveScene().name;
        }

        //public static string GetSceneName(SceneList sceneType)
        //{
        //    return sceneType.ToString();
        //}
    }
}