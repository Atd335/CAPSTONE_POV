using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AudioPlayerSliderScript : MonoBehaviour
{
   
    AudioSource AS;
    AudioClip clip;

    float cliplength;
    float currentTime;

    void Start()
    {
        AS = GetComponentInChildren<AudioSource>();
        clip = AS.clip;
        cliplength = clip.length;
        GameObject.FindGameObjectWithTag("audioTitle").GetComponent<Text>().text = clip.name;
    }
    void Update()
    {
        
        if (AS.isPlaying) { currentTime += Time.deltaTime; }
        currentTime = Mathf.Clamp(currentTime,0,cliplength);
        GetComponentInChildren<Slider>().value = currentTime / cliplength;
        GameObject.FindGameObjectWithTag("audioTimer").GetComponent<Text>().text = $"{secondToMinuteString(currentTime)}/{secondToMinuteString(cliplength)}";

    }

    string secondToMinuteString(float secondCount)
    {
        float minutes = Mathf.Floor(secondCount / 60);
        float seconds = secondCount - (Mathf.Floor(secondCount / 60)*60);
        if (seconds < 10)
        {
            return $"{minutes}:0{Mathf.Round(seconds)}";
        }
        else
        {
            return $"{minutes}:{Mathf.Round(seconds)}";
        }
    }
}
