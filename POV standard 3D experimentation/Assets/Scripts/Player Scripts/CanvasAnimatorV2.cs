using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class CanvasAnimatorV2 : MonoBehaviour
{
    public Sprite[] idleSprites;
    public Sprite[] walkSprites;
    public Sprite[] jumpSprites;

    Image img;

    Sprite[] currentSprites;
    Character_Controller_2D cc2d;
    ModeSwitcher switcher;

    int currentFrame;

    public float jumpTimerThreshold;
    float jumpTimer;

    void Start()
    {
        img = GetComponent<Image>();

        cc2d = UpdateController.cc2D;
        switcher = UpdateController.switcher;
        currentSprites = walkSprites;

        
    }

    void Update()
    {

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            currentSprites = walkSprites;
            playSpd = walkSpd;

        }
        else
        {
            currentSprites = idleSprites;
            playSpd = idleSpd;
        }

        //if (!cc2d.groundedForJump) {jumpTimer += Time.deltaTime;}
        //else {jumpTimer = 0;}

        //if (!cc2d.groundedForJump && jumpTimer>=jumpTimerThreshold)
        //{
        //    currentSprites = jumpSprites;
        //    playSpd = jumpSpd;
        //}

        if (!cc2d.groundedForJump)
        {
            currentSprites = jumpSprites;
            playSpd = jumpSpd;
        }

        playSpd = Mathf.Clamp(playSpd, 1, 60);
    }

    int fr;
    public int playSpd;
    public int idleSpd;
    public int jumpSpd;
    public int walkSpd;
    void FixedUpdate()
    {
        if (switcher.fpsMode == false)
        {
            fr++;
            if (fr % playSpd == 0)
            {
                currentFrame++;
            }
            if (currentFrame >= currentSprites.Length) 
            {
                if (currentSprites == jumpSprites) { currentFrame = currentSprites.Length - 1; }
                else { currentFrame = 0; }
            }
            img.sprite = currentSprites[currentFrame];
        }
        else
        {
            img.sprite = idleSprites[0];
        }
    }
}
