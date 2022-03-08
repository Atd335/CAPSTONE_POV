using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class OnTopCam : MonoBehaviour
{
    public RawImage topTex;
    public RawImage normalTex;
    void Awake()
    {

    }

    public void _Start()
    {
       
    }

    
    public void manualUpdate()
    {
        topTex.enabled = !UpdateController.switcher.fpsMode && UpdateController.cc2D.heldObj2D != null;
        normalTex.enabled = !UpdateController.switcher.fpsMode && UpdateController.cc2D.heldObj2D == null;
    }
}
