using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ModeSwitcher : MonoBehaviour
{
    public bool fpsMode = true;
    public RaycastHit cursorRayHit;
    
    public RaycastHit lineCastHit;
    public bool colliderBetween;

    public Vector3 hitPosition;
    public Vector3 spawnPosition;

    public bool playerOnScreen;

    private void Awake()
    {
        UpdateController.switcher = this;
        Cursor.lockState = CursorLockMode.Locked;
    }

    public void _Start()
    {
        fpsMode = true;
    }

    public void manualUpdate()
    {
        bool LC = Physics.Linecast(UpdateController.cc3D.head.position, 
                                   hitPosition - ((hitPosition - UpdateController.cc3D.head.position).normalized*.1f), 
                                   out lineCastHit);


        bool b1 = withinBoundsOfTexture(roundVectorToInt(UpdateController.cc2D.player.position), new Vector2Int(Screen.width, Screen.height));
        bool b2 = Vector3.Distance(hitPosition,UpdateController.cc3D.head.position + (UpdateController.cc3D.head.forward * .1f)) < 
                  Vector3.Distance(hitPosition, UpdateController.cc3D.head.position + (UpdateController.cc3D.head.forward * -.1f));

        playerOnScreen = b1 && b2;
        colliderBetween = lineCastHit.collider != null;

        if (!colliderBetween && playerOnScreen &&(Input.GetKeyDown(KeyCode.Mouse0) || Input.GetKeyDown(KeyCode.V)))
        {
            fpsMode = !fpsMode;
            if(fpsMode && UpdateController.cc2D.interactingObject)
            {
                UpdateController.cc2D.interactingObject.GetComponent<ScaleResizer>().resize = false;
                UpdateController.cc2D.interactingObject = null;
            }
        }
    }

    public void assign3DPoint(Vector3Int screenPosition)
    {
        bool CR = Physics.Raycast(UpdateController.imageCap.VisualCamera.ScreenPointToRay(screenPosition), out cursorRayHit);
        if (CR)
        {
            hitPosition = cursorRayHit.point;
        }
        else
        {
            hitPosition = UpdateController.imageCap.VisualCamera.ScreenToWorldPoint(screenPosition+new Vector3Int(0,0,10));
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
