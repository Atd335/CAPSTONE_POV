using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextUIAnim : MonoBehaviour
{
    public string[] stringFrames;
    int currentFrame;
    int fr;
    public int spd;

    string currentString;
    Text textBox;

    void Start()
    {
        currentFrame = 0;
        textBox = GetComponent<Text>();
    }
    void FixedUpdate()
    {
        fr++;
        spd = Mathf.Clamp(spd,1,999);
        if (fr % spd == 0)
        {
            currentFrame++;
            if (currentFrame >= stringFrames.Length) { currentFrame = 0; }
        }
        currentString = stringFrames[currentFrame];
        textBox.text = currentString;
    }
}
