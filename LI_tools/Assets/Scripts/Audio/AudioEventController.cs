using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioEventController : MonoBehaviour
{
    private AudioController _audioController;

    public enum AudioEventType
    {
        PlayBackground,
        PlayClip,
        StopAll,
    }

    public void SendPlayBackgroundAudioEvent()
    {
        if (_audioController == null) _audioController = FindObjectOfType<AudioController>();
        Assert.IsNotNull(_audioController);
        _audioController.StartBackgroundAudio();
    }

    public void SendPlayClipAudioEvent(int i)
    {
        if (_audioController == null) _audioController = FindObjectOfType<AudioController>();
        Assert.IsNotNull(_audioController);
        _audioController.PlayClip(i, 0);
    }

    public void SendPlayClipAudioEvent2(int i)
    {
        if (_audioController == null) _audioController = FindObjectOfType<AudioController>();
        Assert.IsNotNull(_audioController);
        _audioController.PlayClip(i, 1);
    }

    public void SendStopAllAudioEvent()
    {
        if (_audioController == null) _audioController = FindObjectOfType<AudioController>();
        Assert.IsNotNull(_audioController);
        _audioController.StopAllAudio();
    }

    public void StopBackgroundAudioEvent()
    {
        if (_audioController == null) _audioController = FindObjectOfType<AudioController>();
        Assert.IsNotNull(_audioController);
        _audioController.StopBackgroundAudio();
    }
}
