using UnityEngine;
using Mixin.Utils;
using Mixin.Audio;

public class MainManager : Singleton<MainManager>
{
    [SerializeField] private ApplicationManager _applicationManager;
    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private Mixin.SceneManager _sceneManager;

}
