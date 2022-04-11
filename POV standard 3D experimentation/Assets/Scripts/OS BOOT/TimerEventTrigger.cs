using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class TimerEventTrigger : MonoBehaviour
{

    public float duration;

    public UnityEvent timerEvent;
    float timer;
    void Update()
    {
        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer,0,duration);
        if (timer == duration) { timerEvent.Invoke(); DestroyImmediate(this); }
    }
}
