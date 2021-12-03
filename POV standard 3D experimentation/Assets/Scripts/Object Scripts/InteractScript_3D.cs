using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractScript_3D : MonoBehaviour
{
    private void Awake()
    {
        UpdateController.IS3D = this;
    }

    // Start is called before the first frame update
    public void _Start()
    {
        
    }

    public RaycastHit interactRCH;

    // Update is called once per frame
    public void manualUpdate()
    {
        if (!UpdateController.switcher.fpsMode) { return; }
        
        bool r = Physics.Raycast(UpdateController.cc3D.head.position, UpdateController.cc3D.head.forward, out interactRCH);

        if (r)
        {

            if (Input.GetKeyDown(KeyCode.Mouse1))
            {
                try
                {
                    interactRCH.collider.GetComponent<IButton_3D>().click("");
                }
                catch (System.Exception){}
            }

        }

    }
}
