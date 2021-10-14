using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InstallerWindowResizer : MonoBehaviour
{
    public Transform player;
    public Vector2 pt;
    void Update()
    {
        pt = UpdateController.cc2D.positionRatio;
        player.localPosition = pt - new Vector2(0,1);
    }
}
