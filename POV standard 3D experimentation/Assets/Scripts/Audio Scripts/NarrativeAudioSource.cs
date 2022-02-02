using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NarrativeAudioSource : MonoBehaviour
{
    public AudioClip soundToLoop;
    public bool startPlaying;
    public bool loop;

    AudioSource AS;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        if (startPlaying) { AS.PlayOneShot(soundToLoop); }   
    }

    

    void Update()
    {
        if (loop && !AS.isPlaying)
        {
            AS.PlayOneShot(soundToLoop);
        }
    }
}
