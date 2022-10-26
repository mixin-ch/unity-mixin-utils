using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Mixin.Utils;

public class MainManager : Singleton<MainManager>
{
    [SerializeField] private ApplicationManager _applicationManager;
    [SerializeField] private AudioManagerOld _audioManager;
    [SerializeField] private Mixin.SceneManager _sceneManager;

}
