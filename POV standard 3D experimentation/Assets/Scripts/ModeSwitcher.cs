using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ModeSwitcher : MonoBehaviour
{
    public bool fpsMode = true;
    public RaycastHit cursorRayHit;

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
        bool CR = Physics.Raycast(UpdateController.cc3D.head.position, UpdateController.cc3D.head.forward, out cursorRayHit);
        if (Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (fpsMode && CR && cursorRayHit.collider.tag == "plat") { return; }
            fpsMode = !fpsMode;
        }
    }
}
