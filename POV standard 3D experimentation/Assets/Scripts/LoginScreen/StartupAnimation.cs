using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class StartupAnimation : MonoBehaviour
{

    public Sprite[] animFrames;
    int currentFrame;
    // Start is called before the first frame update
    void Start()
    {
        currentFrame = 0;
    }

    int fr;
    void FixedUpdate()
    {
        Cursor.lockState = CursorLockMode.None;
        fr++;
        if (fr % 2 == 0)
        {
            currentFrame++;
        }
        if (currentFrame >= animFrames.Length) { Destroy(this); return; }
        GetComponent<Image>().sprite = animFrames[currentFrame];
    }
}
