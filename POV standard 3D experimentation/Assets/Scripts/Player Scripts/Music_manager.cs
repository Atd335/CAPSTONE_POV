using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Music_manager : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AS;

    bool musicPlaying = false;
    AudioClip currentTrack;
    AudioClip upcomingTrack;

    public float volume;

    private void Awake()
    {
        UpdateController.music = this;
    }

    public AudioClip testTrack;

    public void _Start()
    {
        AS = GetComponents<AudioSource>()[1];
        //TESTING
        if (testTrack != null)
        {
            switchTrack(testTrack, 1);
        }
    }

    public void switchTrack(AudioClip track, float spd = 1)
    {
        upcomingTrack = track;
        StartCoroutine(fadeNewTrack(spd));
    }

    public void _Update()
    {
        if (!musicPlaying)
        {
            AS.Stop();
            return;
        }
        else 
        {
            if (!AS.isPlaying) { AS.PlayOneShot(currentTrack);}
        }

        AS.volume = volume * (1-timer);
    }

    float timer = 0;
    IEnumerator fadeNewTrack(float spd)
    {
        timer = 0;
        while (true)
        {
            timer += Time.deltaTime * spd;
            yield return new WaitForSeconds(Time.deltaTime);
            timer = Mathf.Clamp(timer, 0, 1);
            if (timer == 1) { currentTrack = upcomingTrack; break;}
        }
        while (true)
        {
            timer -= Time.deltaTime * spd;
            yield return new WaitForSeconds(Time.deltaTime);
            timer = Mathf.Clamp(timer, 0, 1);
            if (timer == 0) {break;}
        }
    }
}
