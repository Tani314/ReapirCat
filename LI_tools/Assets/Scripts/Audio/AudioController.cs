using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

public class AudioController : MonoBehaviour
{
    public AudioSource AudioBackgroundSource;
    public AudioSource AudioClipSource;
    public AudioSource AudioClipSource2;
    public AudioData AudioData;
    
    public void StartBackgroundAudio()
    {
        Assert.IsTrue(AudioBackgroundSource != null);
        Assert.IsTrue(AudioData != null && AudioData._background != null);

        AudioBackgroundSource.clip = AudioData._background;
        AudioBackgroundSource.loop = true;
        AudioBackgroundSource.volume = 0.5f;
        AudioBackgroundSource.Play();
    }

    public void StopBackgroundAudio()
    {
        Assert.IsTrue(AudioBackgroundSource != null);
        Assert.IsTrue(AudioData != null && AudioData._background != null);
        AudioBackgroundSource.Stop();
    }

    public void PlayClip(int i, int layer)
    {
        Assert.IsTrue(AudioClipSource != null);
        Assert.IsTrue(AudioClipSource2 != null);
        Assert.IsTrue(AudioData != null && AudioData._clips != null && i < AudioData._clips.Count);

        if (layer == 0)
        {
            AudioClipSource.Stop();
            AudioClipSource.clip = AudioData._clips[i];
            AudioClipSource.loop = false;
            AudioClipSource.Play();
        }
        else if (layer == 1)
        {
            AudioClipSource2.Stop();
            AudioClipSource2.clip = AudioData._clips[i];
            AudioClipSource2.loop = false;
            AudioClipSource2.Play();
        }
    }    

    public void StopAllAudio()
    {
        Assert.IsTrue(AudioBackgroundSource != null);
        Assert.IsTrue(AudioClipSource != null);
        Assert.IsTrue(AudioClipSource2 != null);

        AudioBackgroundSource.Stop();
        AudioClipSource.Stop();
        AudioClipSource2.Stop();
    }

    public void SetAudioPlayback(bool play)
    {
        if (play)
        {
            AudioBackgroundSource.UnPause();
            AudioClipSource.UnPause();
            AudioClipSource2.UnPause();
        }
        else
        {
            AudioBackgroundSource.Pause();
            AudioClipSource.Pause();
            AudioClipSource2.Pause();
        }
    }
}
