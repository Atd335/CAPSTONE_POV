using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnWindow : MonoBehaviour
{
    public GameObject window;
    public float waitTime;
    float timer;

    void Start()
    {
        timer = 0;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, waitTime);

        if (timer == waitTime) { spawnWindow(); }
    }

    void spawnWindow()
    {
        Instantiate(window);
        Destroy(this);
    }
}
