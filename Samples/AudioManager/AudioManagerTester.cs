using Mixin.Utils.Audio;
using UnityEngine;

public class AudioManagerTester : MonoBehaviour
{
    public AudioClipSetupSO AudioClip;
    public AudioPlaylistSetupSO AudioPlaylist;

    void Start()
    {
        //AudioManager.Instance.Play(AudioClip);
        AudioManager.Instance.Play(AudioPlaylist);
    }
}
