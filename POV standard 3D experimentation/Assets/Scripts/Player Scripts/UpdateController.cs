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
    public static InteractScript_3D IS3D;
    public static PopUpInfo_GUI POPUP;
    public static SFX_Manager sfx;
    public static Music_manager music;
    public static PauseMenu_Manager pause;
    public static SpeechScript speech;

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
        switcher._Start();

        qol._Start();
        SUL._Start();
        IS3D._Start();

        sfx._Start();
        music._Start();

        speech._Start();

        if (entireSceenActive)
        {
            activeArea1 = Vector2.zero;
            activeArea2.x = Screen.width;
            activeArea2.y = Screen.height;
        }


    }

    public void PrintTest(string str)
    {
        print(str);
    }

    public bool windowSelected;

    void Update()
    {

        debugtimer += Time.deltaTime / waitDuration;
        debugtimer = Mathf.Clamp(debugtimer,0,1);
        if (debugtimer != 1) { return; }

        qol.manualUpdate();
        if (pause == null || !pause.menuOpen)
        {
            imageCap.manualUpdate();
            switcher.manualUpdate();
            cc3D.manualUpdate();
            IS3D.manualUpdate();
            cc2D.manualUpdate();
            sfx._Update();
        }
        music._Update();
        if (pause)
        {
            pause.manualUpdate();
        }

        if (pause && pause.menuOpen) {Cursor.lockState = CursorLockMode.None;}
        else {Cursor.lockState = CursorLockMode.Locked;}

        if (debugMousePos) { print(Input.mousePosition); }

        //CHECKPOINT TEST
        if (Input.GetKeyDown(KeyCode.R) && RayCastInfoPlayer.RespawnCheckPoint != Vector3.zero) 
        {
            switcher.hitPosition = RayCastInfoPlayer.RespawnCheckPoint;
            cc2D.DIE();
            switcher.hitPosition = RayCastInfoPlayer.RespawnCheckPoint;
            switcher.spawnPosition = RayCastInfoPlayer.RespawnCheckPoint;
        }
        //
    }


}
