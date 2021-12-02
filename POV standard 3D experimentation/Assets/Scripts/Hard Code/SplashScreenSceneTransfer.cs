using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SplashScreenSceneTransfer : MonoBehaviour
{
    public float timeToSceneTransfer;
    public int sceneToGoTo;
    public float timer;

    bool triggered;

    void Start()
    {
        timer = -.35f;
        triggered = false;
    }

    void Update()
    {
        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer,-1,timeToSceneTransfer);
        
        if (timer >= timeToSceneTransfer && !triggered)
        {
            GetComponent<SplashScreenAnim>().switchToFull();
            triggered = true;
        }
    }
}
