using Mixin.Utils.Audio;
using UnityEngine;

public class AudioManagerTester : MonoBehaviour
{
    public AudioSetup AudioSetup;
    public AudioPlaylistSetup AudioPlaylistSetup;

    void Start()
    {
        //AudioManager.Instance.Play(AudioSetup);
        AudioManager.Instance.Play(AudioPlaylistSetup);
    }
}
