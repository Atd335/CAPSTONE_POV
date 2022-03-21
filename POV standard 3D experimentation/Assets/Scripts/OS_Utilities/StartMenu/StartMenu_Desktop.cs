using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class StartMenu_Desktop : MonoBehaviour
{
    bool menuActive;
    public GameObject menu;

    void Start()
    {
        menuActive = false;

    }

    
    void Update()
    {
        if (DesktopAsset_Cursor.hoveredElement == this.gameObject && Input.GetKeyUp(KeyCode.Mouse0))
        {
            menuActive = !menuActive;
            SFX_Desktop.dsfx.playSound(1,.15f,1.5f,1.5f);

            foreach (StartMenuButtons g in FindObjectsOfType<StartMenuButtons>())
            {
                g.popOut.SetActive(false);
            }
        }

        menu.SetActive(menuActive);
    }
}
