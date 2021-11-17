using System;
using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasAnimator : MonoBehaviour
{
    public bool flip;

    public Sprite[] spriteSheet;
    public Sprite[] currentAnim;

    public bool animationFinished;
    public int currentStateID;
    public int spd;  
    int fr;

    bool loopAnimation;
    int currentFrame;

    [HideInInspector]
    public Image imageRenderer;

    private void Start()
    {
        imageRenderer = GetComponent<Image>();

        //set to idle for testing...
        //SwitchAnimation(new Vector2Int(0,12),5,true);
    }

    public void SwitchAnimation(Vector2Int animSegment, int _spd, bool playLooped = false, bool reset = false)
    {
        if (reset) { currentFrame = 0; }
        var segment = new ArraySegment<Sprite>(spriteSheet,animSegment.x,animSegment.y);
        currentAnim = segment.ToArray();
        loopAnimation = playLooped;
        spd = _spd;
    }

    private void FixedUpdate()
    {
        fr++;
        if (currentAnim.Length == 0) { return; }
        if (fr % spd == 0) { currentFrame++; }

        if (currentFrame >= currentAnim.Length && loopAnimation) { currentFrame = 0; }
        else if (currentFrame >= currentAnim.Length && !loopAnimation) { currentFrame = currentAnim.Length-1; }

        imageRenderer.transform.rotation = Quaternion.Euler(imageRenderer.transform.rotation.eulerAngles.x,0, imageRenderer.transform.rotation.eulerAngles.z);
        if (flip) { imageRenderer.transform.rotation = Quaternion.Euler(imageRenderer.transform.rotation.eulerAngles.x, 180, imageRenderer.transform.rotation.eulerAngles.z); }
        imageRenderer.sprite = currentAnim[currentFrame];
    }
}
