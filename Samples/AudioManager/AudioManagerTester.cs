using Mixin.Audio;
using System;
using System.Collections.Generic;
using UnityEngine;

public class AudioManagerTester : MonoBehaviour
{
    public AudioPlaylistSetupSO AudioPlaylist;

    private AudioPlaylistPlayer _audioPlaylistPlayer;

    private List<Action> _funcs = new List<Action>();
    private float _time;

    private void Start()
    {
        _audioPlaylistPlayer = AudioManager.Instance.PlayPlaylist(AudioPlaylist.ToAudioPlaylistSetup());

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
        _audioPlaylistPlayer.Stop(12);
    }
}
