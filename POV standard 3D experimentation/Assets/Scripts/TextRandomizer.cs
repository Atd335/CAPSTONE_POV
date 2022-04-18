using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextRandomizer : MonoBehaviour
{

    public string[] randomStrings;
    float randomInterval;
    public float minTime = .02f;
    public float maxTime = .1f;

    float timer;

    int randID;

    Text text;
    private void Start()
    {
        text = GetComponent<Text>();
    }

    void Update()
    {
        randomInterval = Random.Range(minTime,maxTime);

        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, randomInterval);
        
        if (timer == randomInterval)
        {
            randID = Random.Range(0,randomStrings.Length);
            text.text = randomStrings[randID];
            timer = 0;
        }

    }
}
