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

        AS.volume = volume; 

        if (currentTrack != null)
        {
            AS.clip = currentTrack;
            AS.Play();
        }
    }

    public void _Update(){

        // Removed AS.volume = volume; and put it in _Start(); 
        // Volumne is being changed by another script, constantly overriding any changes. At least this way, we can edit the volume in the inspector. 
        // This means the volume can be changed in inspector, but won't change at runtime through PauseMenu, etc.  

    }
}
