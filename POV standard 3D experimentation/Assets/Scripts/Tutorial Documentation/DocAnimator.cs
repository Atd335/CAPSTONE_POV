using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocAnimator : MonoBehaviour
{
    public Sprite[] frames;


    int fr;
    int currentFrame;

    private void OnEnable()
    {
        currentFrame = 0;
    }
    private void OnDisable()
    {
        currentFrame = 0;
    }

    private void FixedUpdate()
    {
        fr++;
        if (fr % 2 == 0)
        {
            currentFrame++;
        }

        if (currentFrame >= frames.Length) { currentFrame = 0; }
        GetComponent<Image>().sprite = frames[currentFrame];
    }
}
