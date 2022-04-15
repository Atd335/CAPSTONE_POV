using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpacialNarrativeAudio : MonoBehaviour
{
     public AudioClip soundToLoop;
    
    public bool listenerWithinMinimumRange;
    public bool loopTrigger;
    public bool trigger;
    
    AudioSource m_AudioSource;
    AudioSource AS_Static;
    AudioListener AL;

    public float timeTillReset = 3;
    public float m_StaticMultiplier;
    public float m_AudioTriggerMinDistance = 1f; 
    public float volMultiplier = 1f; 
    public float timer;
    public float staticTimer;
    public float staticTimerSpd = .3f;
    public float waitTime = 2.5f;


     float m_MoveSpeed = 1f; //~ half as fast as the user moves. 30 seconds for the whole level. 

    public AnimationCurve staticVolumeCurve;

    private Transform m_OrignialLocation; 



    void Start()
    {

        m_AudioSource = GetComponent<AudioSource>();
        AS_Static = GetComponentsInChildren<AudioSource>()[1];

        AL = GameObject.FindObjectOfType<AudioListener>();

        //Look at a random destination, we'll move the object forward in Update();
        transform.LookAt(DetermineDestination());  
    }

    

    void Update()
    {

        //Move object foward based on DetermineDestination's resulting random target destination. 
        transform.Translate(transform.forward * (Time.deltaTime * m_MoveSpeed));


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



    IEnumerator waitThenPlay()
    {
        yield return new WaitForSeconds(waitTime);
        m_AudioSource.PlayOneShot(soundToLoop);
    }


    //DetermineDestination takes the object's current location and return's a position on the same Y value to be used as a target to move towards. 
    private Vector3 DetermineDestination(){

        Random.InitState(System.DateTime.Now.Millisecond);

        float randZ = Random.Range(-50, 50);
        float randX = Random.Range(-50, 50);

        Vector3 resultingPos = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ); 

        return resultingPos;

    }


}
