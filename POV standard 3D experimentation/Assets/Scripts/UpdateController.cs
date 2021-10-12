using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    public static UpdateController UC;

    public static ImageCap imageCap;
    public static Character_Controller_2D cc2D;
    public static Character_Controller_3D cc3D;
    public static QoLDebuggingTools qol;
    public static ModeSwitcher switcher;
    public static DrawOnWalls dow;
    public static ChangeCharacterCollisionColors col;
    float debugtimer = 0;
    
    void Start()
    {
        UC = this;
        imageCap._Start();
        switcher._Start();
        cc3D._Start();
        cc2D._Start();
        dow._Start();

        qol._Start();
        //Cursor.lockState = CursorLockMode.Locked;
    }

    void Update()
    {
        if (Input.GetKeyDown(KeyCode.K)) { col.ChangeColor("platform","green");}
        debugtimer += Time.deltaTime;
        debugtimer = Mathf.Clamp(debugtimer,0,1);
        if (debugtimer != 1) { return; }

        qol.manualUpdate();
        imageCap.manualUpdate();
        switcher.manualUpdate();
        cc3D.manualUpdate();
        cc2D.manualUpdate();
        dow.manualUpdate();
    }
}
