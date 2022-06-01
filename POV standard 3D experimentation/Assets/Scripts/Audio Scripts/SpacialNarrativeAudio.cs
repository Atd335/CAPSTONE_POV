using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using PathCreation.Examples;

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
    public float m_AudioInnerRadius = 1f; 
    public float volMultiplier = 1f; 
    public float timer;
    public float staticTimer;
    public float staticTimerSpd = .3f;
    public float waitTime = 2.5f;


     float m_MoveSpeed = 1f; //~ half as fast as the user moves. 30 seconds for the whole level. 

    public AnimationCurve staticVolumeCurve;

    private Transform m_OrignialLocation;

    public int loopCounter = 0;

    PathFollower follower;

    Transform spriteTransform;

    void Start()
    {
        spriteTransform = GetComponentInChildren<SpriteRenderer>().transform;
        m_AudioSource = GetComponent<AudioSource>();
        AS_Static = GetComponentsInChildren<AudioSource>()[1];

        AL = GameObject.FindObjectOfType<AudioListener>();

        follower = GetComponent<PathFollower>();

        //Look at a random destination, we'll move the object forward in Update();
        //transform.LookAt(DetermineDestination());  
    }

    public float fullVolume;
    float spdMod;
    public float accel = 3f;

    bool poofed;
    void Update()
    {

        //is true if within the mimnimum range of being able to hear the soundclip
        listenerWithinMinimumRange = Vector3.Distance(transform.position, AL.transform.position) <= m_AudioTriggerMinDistance;


        //Move object foward based on DetermineDestination's resulting random target destination. 
        //if (listenerWithinMinimumRange == true)  { transform.Translate(transform.forward * (Time.deltaTime * m_MoveSpeed)); }

        staticTimer += Time.deltaTime * staticTimerSpd;
        staticTimer = Mathf.Clamp(staticTimer, 0, 1);

        m_AudioSource.volume = 1 - (staticVolumeCurve.Evaluate(staticTimer)) * m_StaticMultiplier;
        if (staticTimer == 1) { staticTimer = 0; }
        AS_Static.volume = 1 - m_AudioSource.volume;
        AS_Static.volume *= fullVolume;

        AS_Static.volume *= volMultiplier;
        m_AudioSource.volume *= volMultiplier;
        m_AudioSource.volume *= fullVolume;

        if (!m_AudioSource.isPlaying && !doingWaitLoop)
        {
            StartCoroutine(waitThenPlay(1));            
        }

        if (listenerWithinMinimumRange)
        {
            spdMod += Time.deltaTime*accel;
        }
        else
        {
            spdMod -= Time.deltaTime*accel;
        }


        spdMod = Mathf.Clamp(spdMod, 0, 1);
        follower.distanceTravelled += Time.deltaTime * follower.speed * spdMod;
        follower.distanceTravelled = Mathf.Clamp(follower.distanceTravelled, 0, follower.pathCreator.path.length);
        
        if ((follower.distanceTravelled == follower.pathCreator.path.length)||loopCounter>=3) 
        { 
            Poof();
        }
    
    }

    public void Poof()
    {
        spriteTransform.localScale = Vector3.Lerp(spriteTransform.localScale, Vector3.one * 5.1f, Time.deltaTime*3);
        spriteTransform.GetComponent<SpriteRenderer>().color = Color.Lerp(spriteTransform.GetComponent<SpriteRenderer>().color, new Color(1,1,1,0), Time.deltaTime*3.5f);
        fullVolume = Mathf.Lerp(fullVolume, 0, Time.deltaTime*3.5f);

        if (spriteTransform.localScale.x > 5) { Destroy(this.gameObject); }
    }

    bool doingWaitLoop;
    IEnumerator waitThenPlay(float time)
    {
        doingWaitLoop = true;
        yield return new WaitForSeconds(time);
        m_AudioSource.clip = soundToLoop;
        m_AudioSource.Play();
        loopCounter++;
        doingWaitLoop = false;
    }


    //DetermineDestination takes the object's current location and return's a position on the same Y value to be used as a target to move towards. 
    private Vector3 DetermineDestination(){

        Random.InitState(System.DateTime.Now.Millisecond);

        float randZ = Random.Range(-50, 50);
        float randX = Random.Range(-50, 50);

        Vector3 resultingPos = new Vector3(transform.position.x + randX, transform.position.y, transform.position.z + randZ); 

        return resultingPos;

    }

    private void OnValidate()
    {
        m_AudioSource = GetComponent<AudioSource>();
        AS_Static = GetComponentsInChildren<AudioSource>()[1];

        m_AudioSource.maxDistance = m_AudioTriggerMinDistance;
        m_AudioSource.minDistance = m_AudioInnerRadius;

        AS_Static.maxDistance = m_AudioTriggerMinDistance;
        AS_Static.minDistance = m_AudioInnerRadius;
    }

}
