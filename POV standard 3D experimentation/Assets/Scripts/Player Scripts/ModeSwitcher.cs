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
        foreach (KeyCode k in System.Enum.GetValues(typeof(KeyCode)))
        {
            char[] codes = { '0', '1', '2', '3', '4', '5', '6', '7', '8', '9' };
            if (Input.GetKeyUp(k) && codes.Contains<char>(k.ToString()[k.ToString().Length - 1]) && k != KeyCode.Mouse0 && k != KeyCode.Mouse1)
            {
                SceneManager.LoadScene(int.Parse(k.ToString()[k.ToString().Length - 1].ToString()));
            }
        }

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
                checkForStickySurface();

                assign3DPoint(roundVectorToInt(UpdateController.cc2D.player.position));
                UpdateController.cc2D.respawnPosition = hitPosition;
            }
            else
            {
                isSticky = false;
                stickyObj = null;
            }

        }

        if (isSticky)
        { 
            
        }

        if (UpdateController.SUL.fpsCharacterEnabled==false)
        {
            UpdateController.switcher.fpsMode = false;
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
