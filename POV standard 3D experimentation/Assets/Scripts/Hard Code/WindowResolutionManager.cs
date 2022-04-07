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

    bool fullScreen;

    public CursorLockMode lockMode = CursorLockMode.None;

    private void Begin()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("resManager"))
        {
            if (g != this.gameObject && g.name == "*resManager") { Destroy(g); }
        }

        this.gameObject.name = "*resManager";

        //SetRes();

        DontDestroyOnLoad(this.gameObject);
    }

    void SetRes()
    {
        Screen.SetResolution(overrideRes.x, overrideRes.y, overrideFS);
        print("resolution set...");

    }

    private void Start()
    {
        Cursor.lockState = lockMode;
        SetRes();
    }

    private void Update()
    {
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyDown(KeyCode.N)) {SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);}
        
        if (Input.GetKey(KeyCode.LeftShift) && Input.GetKeyUp(KeyCode.R)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }

    void OnEnable()
    {
        SceneManager.sceneLoaded += OnSceneLoaded;
    }

    void OnSceneLoaded(Scene scene, LoadSceneMode mode)
    {
        Begin();
        //Debug.Log("OnSceneLoaded: " + scene.name);
    }

}
