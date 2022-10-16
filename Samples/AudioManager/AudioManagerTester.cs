using Mixin.Utils.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTester : MonoBehaviour
{
    public AudioClipSetupSO AudioClip;
    public AudioClip AudioClip2;
    public AudioPlaylistSetupSO AudioPlaylist;

    private AudioClipPlayer _audioClipPlayer;
    private AudioPlaylistPlayer _audioPlaylistPlayer;

    private List<Action> _funcs = new List<Action>();
    private float _time;

    private void Start()
    {
        //_audioClipPlayer = AudioManager.Instance.Play(AudioClip.ToAudioClipSetup());
        _audioPlaylistPlayer = AudioManager.Instance.Play(AudioPlaylist.ToAudioPlaylistSetup());

        _funcs.Add(First);
    }

    private void Update()
    {
        if (_funcs.Count == 0)
            return;

        _time += Time.deltaTime;
        if (_time >= 1)
        {
            _time -= 1;
            _funcs[0].Invoke();
            _funcs.RemoveAt(0);
        }
    }

    private void First()
    {
        //    _audioClipPlayer.AudioClipSetup.Volume = 0.5f;
        //    _audioClipPlayer.AudioClipSetup.Pitch = 0.5f;
        //    _audioClipPlayer.ApplyAudioSetup();

        _audioPlaylistPlayer.AudioPlaylistSetup.Pitch = 0.5f;
        _audioPlaylistPlayer.ApplyAudioSetup();
    }
}
