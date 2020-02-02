using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AudioController : MonoBehaviour
{
    public float interval = 15.0f;
    public List<AudioClip> clipList = new List<AudioClip>();
    public AudioSource audioSource;
    public float volume = 1.0f;
    
    // Start is called before the first frame update
    void Start()
    {
        audioSource = FindObjectOfType<AudioSource>();
        InvokeRepeating("PlayRandomClip", 1.0f, interval);
    }

    void PlayRandomClip()
    {
        System.Random rnd = new System.Random();
        int index = rnd.Next(clipList.Count);
        AudioClip clip = clipList[index];
        audioSource.PlayOneShot(clip, volume);
    }

    // Update is called once per frame
    void Update()
    {

    }
}
