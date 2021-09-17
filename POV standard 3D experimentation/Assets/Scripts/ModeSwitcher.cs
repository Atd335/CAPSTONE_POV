using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public bool fpsMode = true;

    private void Awake()
    {
        UpdateController.switcher = this;   
    }

    public void _Start()
    {
        fpsMode = true;
    }

    public void manualUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            fpsMode = !fpsMode;
        }
    }
}
