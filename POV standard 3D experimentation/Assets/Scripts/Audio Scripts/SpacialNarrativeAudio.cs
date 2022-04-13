using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacialNarrativeAudio : MonoBehaviour
{
     public AudioClip soundToLoop;
    
    public bool listenerWithinMinimumRange;
    bool loopTrigger;
    
    AudioSource m_AudioSource;
    AudioSource AS_Static;
    AudioListener AL;

    public float timeTillReset = 3;
    public float m_StaticMultiplier;
    public float m_AudioTriggerMinDistance = 1f; 
    float timer;

    public float volMultiplier = 1f; 

    public AnimationCurve staticVolumeCurve;

    float staticTimer;
    public float staticTimerSpd = .3f;

    void Start()
    {
        m_AudioSource = GetComponent<AudioSource>();
        AS_Static = GetComponentsInChildren<AudioSource>()[1];

        AL = GameObject.FindObjectOfType<AudioListener>();  
    }
    bool trigger;
    void Update()
    {
        //is true if within the mimnimum range of being able to hear the soundclip
        listenerWithinMinimumRange = Vector3.Distance(transform.position, AL.transform.position) <= m_AudioTriggerMinDistance;

        staticTimer += Time.deltaTime * staticTimerSpd;
        staticTimer = Mathf.Clamp(staticTimer, 0, 1);

        m_AudioSource.volume = 1 - (staticVolumeCurve.Evaluate(staticTimer)) * m_StaticMultiplier;
        if (staticTimer == 1) { staticTimer = 0; }
        AS_Static.volume = 1 - m_AudioSource.volume;  

        AS_Static.volume *= volMultiplier;
        m_AudioSource.volume *= volMultiplier;


        if (!listenerWithinMinimumRange)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer, 0,timeTillReset);
            
            if (timer == timeTillReset)
            {
                m_AudioSource.Stop();
                loopTrigger = false;
            }
        }
        else
        {
            //if you exit and re-enter the range of the audiosource, it starts the clip over from the beginning. 
            if (!loopTrigger)
            {
                m_AudioSource.PlayOneShot(soundToLoop);
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
        m_AudioSource.PlayOneShot(soundToLoop);
    }


}
