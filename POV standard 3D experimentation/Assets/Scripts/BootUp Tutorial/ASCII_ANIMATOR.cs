using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ASCII_ANIMATOR : MonoBehaviour
{

    public TextAsset txt;
    string[] frames;
    public string additionalString;
    void Start()
    {
        frames = txt.text.Split('~');
    }

    int fr;
    int currentFrame;
    void FixedUpdate()
    {
        fr++;
        if (fr % 2 == 0)
        {
            currentFrame++;
            if (currentFrame >= frames.Length) { currentFrame = 0; }
        }
        GetComponent<Text>().text = frames[currentFrame] +"\n"+additionalString;
    }
}
