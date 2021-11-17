using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class player_animation_statemachine : MonoBehaviour
{
    Character_Controller_2D cc2d;
    CanvasAnimator playerAnim;
    public Transform animPivot;

    //IDLE = 0-12 (loop)
    //WALK = 13-12 (loop)
    //JUMP = 26-12 (don't loop)
    //IN AIR = 38-1 (loop)

    bool groundedTrigger;

    void Awake()
    {
        UpdateController.anim = this;
    }

    public void _Start()
    {
        playerAnim = GetComponent<CanvasAnimator>();
        playerAnim.SwitchAnimation(new Vector2Int(0,12),5,true);
        cc2d = UpdateController.cc2D;
    }

    public void manualUpdate()
    {
        if (Input.GetAxisRaw("Horizontal") > 0) { playerAnim.flip = false; }
        else if (Input.GetAxisRaw("Horizontal") < 0) { playerAnim.flip = true; }

        //squish and stretch
        if(Input.GetButton("Jump") && cc2d.groundedForJump) { animPivot.localScale = new Vector2(.6f,1.5f);}

        if (!cc2d.groundedForJump)
        {
            playerAnim.SwitchAnimation(new Vector2Int(38, 1), 5, false);
            groundedTrigger = false;
        }
        else
        {
            if (Input.GetAxisRaw("Horizontal") == 0)
            {
                playerAnim.SwitchAnimation(new Vector2Int(0, 12), 5, true);//idle
            }
            else
            {
                playerAnim.SwitchAnimation(new Vector2Int(12, 13), 2, true);//walk
            }
        }

        if (!groundedTrigger && cc2d.groundedForJump)
        {
            animPivot.localScale = new Vector2(1.2f, .55f);
            groundedTrigger = true;
        }

        animPivot.localScale = Vector3.Lerp(animPivot.localScale,Vector3.one, Time.deltaTime*12);
    }
}
