using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAnimator : MonoBehaviour
{
    public Sprite[] currentAnim;

    public bool animationFinished;
    public int currentStateID;
    public int spd;  
    int fr;

    bool loopAnimation;
    int currentFrame;

    Image imageRenderer;

    public void SwitchAnimation(Sprite[] switchAnim, int _spd, bool playLooped = false)
    {
        currentFrame = 0;
        currentAnim = switchAnim;
        loopAnimation = playLooped;
        spd = _spd;
    }

    private void FixedUpdate()
    {
        fr++;
        if (fr%spd==0)
        {
            if (currentFrame < currentAnim.Length - 1)
            {
                currentFrame++;
            }
            else
            {
                if (loopAnimation) { currentFrame = 0; }
            }
        }
        
        animationFinished = currentFrame == currentAnim.Length - 1;
        imageRenderer.sprite = currentAnim[currentFrame];

    }
}
