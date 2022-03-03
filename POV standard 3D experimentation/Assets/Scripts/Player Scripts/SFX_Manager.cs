using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SFX_Manager : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AS;

    public AudioClip[] sfxs;

    private void Awake()
    {
        UpdateController.sfx = this;
    }
    public void _Start()
    {
        AS = GetComponents<AudioSource>()[0];
    }

    public void playSound(int id, float volume = 1, float minPitch = 1, float maxPitch = 1)
    {
        AS.pitch = Random.Range(minPitch, maxPitch);
        try
        {
            AS.PlayOneShot(sfxs[id], volume);
        }
        catch (System.Exception)
        {
            print("no sound with this id exists.");
        }
        
    }

    public void playSound(AudioClip clip, float volume = 1, float minPitch = 1, float maxPitch = 1)
    {
        AS.pitch = Random.Range(minPitch, maxPitch);
        AS.PlayOneShot(clip, volume);
    }

    // Update is called once per frame
    public void _Update()
    {
        
    }
}
