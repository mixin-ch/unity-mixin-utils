using Mixin.Utils.Audio;
using UnityEngine;

public class AudioManagerTester : MonoBehaviour
{
    public AudioClipSetupSO AudioClip;
    public AudioPlaylistSetup AudioPlaylist;

    void Start()
    {
        //AudioManager.Instance.Play(AudioSetup);
        AudioManager.Instance.Play(AudioPlaylist);
    }
}
