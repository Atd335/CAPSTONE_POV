using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using System.Linq;

public class WindowResolutionManager : MonoBehaviour
{
    public bool overrideResData;
    public Vector2Int overrideRes;
    public bool overrideFS;

    Dictionary<int, Vector2Int> resolutionDict;
    Dictionary<int, bool> fsDict;

    public int currentLevel = -1;
    bool fullScreen;

    public bool testScene;

    private void Begin()
    {
        print("scene loaded");
        //test!!!
        currentLevel = SceneManager.GetActiveScene().buildIndex;
        //test!!!
        if (testScene) { currentLevel = 99; }

        foreach (GameObject g in GameObject.FindGameObjectsWithTag("resManager"))
        {
            if (g != this.gameObject && g.name != "*resManager") { Destroy(g); }
        }

        this.gameObject.name = "*resManager";

        resolutionDict = new Dictionary<int, Vector2Int>();
        fsDict = new Dictionary<int, bool>();

        resolutionDict.Add(0,new Vector2Int(700,583));//installer
        fsDict.Add(0,false);

        resolutionDict.Add(1, new Vector2Int(700, 583));//splashscreen
        fsDict.Add(1, false);

        resolutionDict.Add(2, new Vector2Int(960, 540));//login
        fsDict.Add(2, true);

        resolutionDict.Add(3, new Vector2Int(960, 540));//desktop
        fsDict.Add(3, true);

        resolutionDict.Add(99, new Vector2Int(960, 540));//testing screen. 
        fsDict.Add(99, false);

        SetRes();

        DontDestroyOnLoad(this.gameObject);
    }

    void SetRes()
    {
        Vector2Int defaultRes = new Vector2Int(960, 540);
        bool fsMode = true;

        try
        {
            defaultRes = resolutionDict[currentLevel];
            fsMode = fsDict[currentLevel];
        }
        catch (System.Exception) { }

        if (overrideResData) 
        {
            defaultRes = overrideRes;
            fsMode = overrideFS;
        }

        Screen.SetResolution(defaultRes.x, defaultRes.y, fsMode);
        print("resolution set...");
        print($"level = {currentLevel}");
        //if (currentLevel == 2) { Screen.fullScreenMode = FullScreenMode.ExclusiveFullScreen; }
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
        }
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.R)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
        if (overrideResData) { return; }
        if (GameObject.Find("CamRaw") && SceneManager.GetActiveScene().buildIndex==0)
        {
            RectTransform rt = GameObject.Find("CamRaw").GetComponent<RectTransform>();
            rt.sizeDelta = new Vector2(423,352);
            RectTransform rtt = GameObject.Find("CanvasRaw").GetComponent<RectTransform>();
            rtt.sizeDelta = new Vector2(423, 352);
        }
    }

    void OnEnable()
    {
        //Debug.Log("OnEnable called");
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Begin();
        //Debug.Log("OnSceneLoaded: " + scene.name);
        //Debug.Log(mode);
    }

    private void OnGUI()
    {
        GUI.color = Color.red;
        if (GameObject.Find("CamRaw"))
        {
            RectTransform rt = GameObject.Find("CamRaw").GetComponent<RectTransform>();
            //GUI.Box(new Rect(10, 10, 100, 20), rt.sizeDelta.ToString());
        }
    }

}
