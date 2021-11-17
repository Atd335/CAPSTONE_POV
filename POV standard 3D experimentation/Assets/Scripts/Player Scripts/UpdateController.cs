using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    public static UpdateController UC;

    public static ImageCap imageCap;
    public static Character_Controller_2D cc2D;
    public static Character_Controller_3D cc3D;
    public static player_animation_statemachine anim;
    public static QoLDebuggingTools qol;
    public static ModeSwitcher switcher;
    public static ChangeCharacterCollisionColors col;
    public static SetUpLevel SUL;

    float debugtimer = 0;
    public float waitDuration;

    public bool entireSceenActive;
    public Vector2 activeArea1;
    public Vector2 activeArea2;

    public bool debugMousePos;

    void Start()
    {
        UC = this;
        imageCap._Start();
        cc3D._Start();
        cc2D._Start();
        anim._Start();
        switcher._Start();

        qol._Start();
        SUL._Start();
        //Universal Stuff

        Screen.SetResolution(700, 583, false); //installer dimensions

        if (entireSceenActive)
        {
            activeArea1 = Vector2.zero;
            activeArea2.x = Screen.width;
            activeArea2.y = Screen.height;
        }
    }

    public bool windowSelected;

    void Update()
    {

        debugtimer += Time.deltaTime / waitDuration;
        debugtimer = Mathf.Clamp(debugtimer,0,1);
        if (debugtimer != 1) { return; }

        qol.manualUpdate();
        imageCap.manualUpdate();
        switcher.manualUpdate();
        cc3D.manualUpdate();
        cc2D.manualUpdate();
        anim.manualUpdate();
        //Some universal stuff...

        windowSelected = Cursor.lockState == CursorLockMode.Locked;

        if (Input.GetKeyDown(KeyCode.Escape))
        {
            Cursor.lockState = CursorLockMode.None;
        }

        bool mouseWithinActiveArea = Input.mousePosition.x >= activeArea1.x && Input.mousePosition.x <= activeArea2.x && Input.mousePosition.y >= activeArea1.y && Input.mousePosition.y <= activeArea2.y; 

        if (Input.GetKeyDown(KeyCode.Mouse0) && mouseWithinActiveArea)
        {
            Cursor.lockState = CursorLockMode.Locked;
        }

        if (debugMousePos) { print(Input.mousePosition); }

        //if (Input.GetKey(KeyCode.Alpha0)) { print(switcher.hitPosition); }
    }
}
