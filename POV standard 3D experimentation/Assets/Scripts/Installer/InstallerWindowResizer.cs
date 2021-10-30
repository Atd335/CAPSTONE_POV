using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
public class InstallerWindowResizer : MonoBehaviour
{
    public Transform player;
    public Image playerSprite;
    Vector2 pt;



    void LateUpdate()
    {
        playerSprite.enabled = UpdateController.switcher.playerOnScreen;

        pt = UpdateController.cc2D.positionRatio;
        player.localPosition = pt - new Vector2(0,1);
    }
}
