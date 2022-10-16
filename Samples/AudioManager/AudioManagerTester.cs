using Mixin.Utils.Audio;
using UnityEngine;

public class AudioManagerTester : MonoBehaviour
{
    public AudioSetup AudioSetup;

    void Start()
    {
        AudioManager.Instance.Play(AudioSetup);
    }
}
