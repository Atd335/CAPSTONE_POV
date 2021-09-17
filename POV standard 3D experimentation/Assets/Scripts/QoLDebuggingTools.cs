using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QoLDebuggingTools : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        UpdateController.qol = this;
    }

    public void _Start()
    { 
    
    }

    // Update is called once per frame
    public void manualUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            UpdateController.imageCap.VisualCamera.enabled = !UpdateController.imageCap.VisualCamera.enabled;
        }
        if (Input.GetKeyDown(KeyCode.Space)) { UpdateController.cc2D.startSim = true; }
        if (Input.GetKeyUp(KeyCode.R)) { SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex); }
    }
}
