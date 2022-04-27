using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class ModeSwitcher : MonoBehaviour
{
    public bool fpsMode = true;
    public RaycastHit cursorRayHit;
    
    public RaycastHit lineCastHit;
    public bool colliderBetween;

    public Vector3 hitPosition;
    public Vector3 spawnPosition;

    public bool playerOnScreen;

    Image Vignette;

    public bool startInFPSMode;

    public bool Movement_Paused = false;

    private void Awake()
    {
        UpdateController.switcher = this;
        //Cursor.lockState = CursorLockMode.Locked;
    }

    public void _Start()
    {
        fpsMode = true;
        if (!startInFPSMode) 
        { 
            fpsMode = false; 
            assign3DPoint(roundVectorToInt(UpdateController.cc2D.player.position));
            UpdateController.cc2D.respawnPosition = hitPosition;
        }
        if (GameObject.Find("Vignette")) { Vignette = GameObject.Find("Vignette").GetComponent<Image>(); }
        Vignette.enabled = false;
    }

    public bool isSticky;
    public Transform stickyObj;
    public Vector3 hitPosMod_Sticky;

    public void manualUpdate()
    {
        Vignette.enabled = !fpsMode;
        if (!UpdateController.cc2D.player.gameObject.activeInHierarchy) { return; }
        bool LC = Physics.Linecast(UpdateController.cc3D.head.position,
                                   hitPosition - ((hitPosition - UpdateController.cc3D.head.position).normalized * .1f),
                                   out lineCastHit);


        bool b1 = withinBoundsOfTexture(roundVectorToInt(UpdateController.cc2D.player.position), new Vector2Int(Screen.width, Screen.height));
        bool b2 = Vector3.Distance(hitPosition, UpdateController.cc3D.head.position + (UpdateController.cc3D.head.forward * .1f)) <
                  Vector3.Distance(hitPosition, UpdateController.cc3D.head.position + (UpdateController.cc3D.head.forward * -.1f));

        playerOnScreen = b1 && b2;
        colliderBetween = lineCastHit.collider != null;

        if (!colliderBetween && playerOnScreen && (Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V)) && UpdateController.UC.windowSelected)
        {
            fpsMode = !fpsMode;
            if (fpsMode && UpdateController.cc2D.heldObj2D)
            {
                UpdateController.cc2D.heldObj2D.ToggleResizeItem();
                UpdateController.cc2D.heldObj2D = null;
            }

            if (fpsMode)
            {
                UpdateController.sfx.playSound(0);
                checkForStickySurface();

                assign3DPoint(roundVectorToInt(UpdateController.cc2D.player.position));
                UpdateController.cc2D.respawnPosition = hitPosition;
            }
            else
            {
                UpdateController.sfx.playSound(1);
                isSticky = false;
                stickyObj = null;
            }

        }

        if (colliderBetween && playerOnScreen && Input.GetKeyDown(KeyCode.Mouse0)) 
        { 
            UpdateController.POPUP.spawnPopUp("Cannot enter 2D mode with an object between you and the 2D Character...",
                                              new Vector2(300,60),
                                              new Vector2(((Screen.width / 2) - (150)), ((Screen.height / 1.5f) - (30))),
                                              1,
                                              16); 
        }

        if (isSticky)
        { 
            
        }

        if (UpdateController.SUL.fpsCharacterEnabled==false)
        {
            UpdateController.switcher.fpsMode = false;
        }

        if (!fpsMode)
        {
            Movement_Paused = true;
        }

        if (fpsMode && Movement_Paused)
        {
            if (Input.GetKeyDown(KeyCode.W) || Input.GetKeyDown(KeyCode.A) || Input.GetKeyDown(KeyCode.S) || Input.GetKeyDown(KeyCode.D)) { Movement_Paused = false; }
        }
    }
    void checkForStickySurface()
    {
        //print("CHECKING FOR STICKY...");
        bool CR = Physics.Raycast(UpdateController.imageCap.CollisionCamera.ScreenPointToRay(UpdateController.cc2D.player.position), out cursorRayHit);
        if (CR && cursorRayHit.collider.tag == "sticky") 
        { 
            isSticky = true; 
            stickyObj = cursorRayHit.collider.transform;
            hitPosMod_Sticky = stickyObj.position * -1;
        }



    }

    public void assign3DPoint(Vector3Int screenPosition)
    {
        bool CR = Physics.Raycast(UpdateController.imageCap.CollisionCamera.ScreenPointToRay(screenPosition), out cursorRayHit);
        if (CR)
        {
            hitPosition = cursorRayHit.point;
        }
        else
        {
            hitPosition = UpdateController.imageCap.CollisionCamera.ScreenToWorldPoint(screenPosition+new Vector3Int(0,0,10));
        }
    }

    public Vector3Int roundVectorToInt(Vector3 v)
    {
        Vector3Int v2 = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        return v2;
    }

    public bool withinBoundsOfTexture(Vector3Int v, Vector2Int screen)
    {
        return v.x > 0 && v.x < screen.x && v.y > 0 && v.y < screen.y;
    }
}
