using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Unity.Profiling;
using UnityEngine.SceneManagement;


public class exhibitionCommandManager : MonoBehaviour
{
    public Texture devTexture;

    float inputTimer = 0;
    float gameplayTimer = 0;

    float gameplayMax = 1800;//1800 seconds = 30 min
    float inputMax = 120;// 90 seconds = 1m 30s

    public int screenSaverSceneID;

    bool devMode;

    ProfilerRecorder totalReservedMemoryRecorder;
    ProfilerRecorder gcReservedMemoryRecorder;
    ProfilerRecorder systemUsedMemoryRecorder;

    

    void Start()
    {
        DontDestroyOnLoad(this.gameObject);
        devMode = false;
    }

    void OnEnable()
    {
        totalReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "Total Reserved Memory");
        gcReservedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "GC Reserved Memory");
        systemUsedMemoryRecorder = ProfilerRecorder.StartNew(ProfilerCategory.Memory, "System Used Memory");
    }

    void OnDisable()
    {
        totalReservedMemoryRecorder.Dispose();
        gcReservedMemoryRecorder.Dispose();
        systemUsedMemoryRecorder.Dispose();
    }

    void LateUpdate()
    {
        if (SceneManager.GetActiveScene().buildIndex == screenSaverSceneID) { Destroy(this.gameObject); }

        inputTimer += Time.unscaledDeltaTime;
        gameplayTimer += Time.unscaledDeltaTime;

        inputTimer = Mathf.Clamp(inputTimer, 0, inputMax);
        gameplayTimer = Mathf.Clamp(gameplayTimer, 0, gameplayMax);

        if (Input.anyKey || new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")).magnitude > 0)//reset the input timer
        {
            inputTimer = 0;
        }

        if (inputTimer == inputMax) { setScene(screenSaverSceneID); }// if theres no input for x seconds, send back to screen saver

        if (Input.GetKeyDown(KeyCode.RightControl) || Input.GetKeyDown(KeyCode.F12)) { devMode = !devMode; }
        
        if (!devMode) { return; }
        if (Input.GetKeyDown(KeyCode.N)){ SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1); }//Next
        if (Input.GetKeyDown(KeyCode.B)){ SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex - 1); }//Nexts
        if (Input.GetKeyDown(KeyCode.R)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }//reset current scene

        if (Input.anyKeyDown)// go to scene in build index (0-9)
        {
            string s = Input.inputString;
            if (s != "" && char.IsNumber(s[0])) { setScene(int.Parse(s)); }
        }

        if (Input.GetKeyDown(KeyCode.K) && GameObject.FindObjectOfType<QoLDebuggingTools>()!=null)
        {
            GameObject.FindObjectOfType<QoLDebuggingTools>().Toggle2DCharacter(true);
        }

        if (Input.GetKeyDown(KeyCode.L)) { toggleCursorMode(); }
    }

    bool cursorMode;
    void toggleCursorMode()
    {
        cursorMode = !cursorMode;
        if (cursorMode)
        {
            Cursor.lockState = CursorLockMode.None;
        }
        else
        {
            Cursor.lockState = CursorLockMode.Locked;
        }
    }


    void setScene(int sceneID)
    {
        SceneManager.LoadScene(sceneID);
    }

    private void OnGUI()
    {
        if (!devMode) { return; }
        GUI.Box(new Rect(0, 0, 235, 200), "");

        GUIStyle devStyle = new GUIStyle();
        GUI.Label(new Rect(0,0,32,32),devTexture,devStyle);
        devStyle.normal.textColor = Color.black;
        GUI.Box(new Rect(11,43,100,100), "R: RESET SCENE\nB: PREV SCENE\nN: NEXT SCENE\nL: TOGGLE CURSOR\nK: PLACE 2D CHARACTER\n0-9: SELECT SCENE\nR CTRL: TOGGLE DEV MODE",devStyle);
        GUI.Box(new Rect(9,41,100,100), "R: RESET SCENE\nB: PREV SCENE\nN: NEXT SCENE\nL: TOGGLE CURSOR\nK: PLACE 2D CHARACTER\n0-9: SELECT SCENE\nR CTRL: TOGGLE DEV MODE",devStyle);
        GUI.Box(new Rect(9,42,100,100), "R: RESET SCENE\nB: PREV SCENE\nN: NEXT SCENE\nL: TOGGLE CURSOR\nK: PLACE 2D CHARACTER\n0-9: SELECT SCENE\nR CTRL: TOGGLE DEV MODE",devStyle);
        GUI.Box(new Rect(11,41,100,100), "R: RESET SCENE\nB: PREV SCENE\nN: NEXT SCENE\nL: TOGGLE CURSOR\nK: PLACE 2D CHARACTER\n0-9: SELECT SCENE\nR CTRL: TOGGLE DEV MODE",devStyle);
        devStyle.normal.textColor = Color.white;
        GUI.Box(new Rect(10,42,100,100), "R: RESET SCENE\nB: PREV SCENE\nN: NEXT SCENE\nL: TOGGLE CURSOR\nK: PLACE 2D CHARACTER\n0-9: SELECT SCENE\nR CTRL: TOGGLE DEV MODE",devStyle);

        devStyle.normal.textColor = Color.yellow;
        GUI.Label(new Rect(10, 150, 100, 100), $"SYSTEM MEMORY USAGE:\n {(systemUsedMemoryRecorder.LastValue / 1048576f) / 1000f}GB", devStyle);

        devStyle.fontSize = 20;
        devStyle.normal.textColor = Color.black;
        GUI.Box(new Rect(39, 7, 200, 100), "EXHIBITION TOOLS", devStyle);
        GUI.Box(new Rect(41, 9, 200, 100), "EXHIBITION TOOLS", devStyle);
        GUI.Box(new Rect(39, 9, 200, 100), "EXHIBITION TOOLS", devStyle);
        GUI.Box(new Rect(41, 7, 200, 100), "EXHIBITION TOOLS", devStyle);
        devStyle.normal.textColor = Color.red;
        GUI.Box(new Rect(40, 8, 200, 100), "EXHIBITION TOOLS", devStyle);



        
    }
}


