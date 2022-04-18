using System.Collections;
using System.Collections.Generic;
using UnityEngine;


///////////////////
/*

    This is obsolete, Try SpacialNarrativeAudio.cs

*/
///////////////////


public class NarrativeAudioSource : MonoBehaviour
{
    public AudioClip soundToLoop;
    
    public bool listenerWithinMinimumRange;
    bool loopTrigger;
    
    AudioSource AS;
    AudioSource AS_Static;
    AudioListener AL;

    public float timeTillReset = 3;
    float timer;

    public float staticDegree;

    public AnimationCurve staticVolumeCurve;

    float staticTimer;
    public float staticTimerSpd = .3f;

    void Start()
    {
        AS = GetComponent<AudioSource>();
        AS_Static = GetComponentsInChildren<AudioSource>()[1];

        AL = GameObject.FindObjectOfType<AudioListener>();  
    }
    bool trigger;
    void Update()
    {
        //is true if within the mimnimum range of being able to hear the soundclip
        listenerWithinMinimumRange = Vector3.Distance(transform.position, AL.transform.position)<=AS.maxDistance;

        staticTimer += Time.deltaTime * staticTimerSpd;
        staticTimer = Mathf.Clamp(staticTimer, 0, 1);
        AS.volume = 1 - (staticVolumeCurve.Evaluate(staticTimer))*staticDegree;
        if (staticTimer == 1) { staticTimer = 0; }
        AS_Static.volume = 1 - AS.volume;

        

        if (!listenerWithinMinimumRange)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0,timeTillReset);
            
            if (timer == timeTillReset)
            {
                AS.Stop();
                loopTrigger = false;
            }
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
                if (trigger) { StartCoroutine(waitThenPlay()); }
            }
        }


    }

    public float waitTime = 2.5f;
    IEnumerator waitThenPlay()
    {
        yield return new WaitForSeconds(waitTime);
        AS.PlayOneShot(soundToLoop);
    }

}
