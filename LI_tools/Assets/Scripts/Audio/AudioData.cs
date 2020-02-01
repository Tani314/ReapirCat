using System;
using System.Collections.Generic;
using UnityEngine;

[Serializable]
public class AudioData : ScriptableObject
{
    public AudioClip _background;
    public List<AudioClip> _clips;
}
