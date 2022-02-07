using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class NarrativeAudioSource : MonoBehaviour
{
    public AudioClip soundToLoop;
    
    public bool listenerWithinMinimumRange;
    bool loopTrigger;
    
    AudioSource AS;
    AudioListener AL;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        AL = GameObject.FindObjectOfType<AudioListener>();  
    }

    void Update()
    {
        //is true if within the mimnimum range of being able to hear the soundclip
        listenerWithinMinimumRange = Vector3.Distance(transform.position, AL.transform.position)<=AS.maxDistance;

        if (!listenerWithinMinimumRange)
        {
            AS.Stop();
            loopTrigger = false;
        }
        else
        {
            //if you exit and re-enter the range of the audiosource, it starts the clip over from the beginning. 
            if (!loopTrigger)
            {
                AS.PlayOneShot(soundToLoop);
                loopTrigger = true;
            }
            else
            {
                if (!AS.isPlaying) { AS.PlayOneShot(soundToLoop); }
            }
        }


    }


}
