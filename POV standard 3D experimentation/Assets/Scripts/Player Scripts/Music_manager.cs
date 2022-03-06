using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Music_manager : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AS;

    bool musicPlaying = false;
    public AudioClip currentTrack;

    public float volume;

    private void Awake()
    {
        UpdateController.music = this;
    }

    public void _Start()
    {
        AS = GetComponents<AudioSource>()[1];
        if (currentTrack != null)
        {
            AS.clip = currentTrack;
            AS.Play();
        }
    }

    public void _Update()
    {

        AS.volume = volume;

    }
}
