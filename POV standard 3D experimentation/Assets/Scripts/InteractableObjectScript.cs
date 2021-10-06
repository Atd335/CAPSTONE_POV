using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{

    GameObject flatComponent;
    Collider collider;


    //Resizing
    public bool resize;
    bool triggered;
    float distanceFromPlayer;
    float initialDistance;
    float baseScale;
    float distanceFromHitPoint;

    Vector3 startPoint;
    float moveTimer;

    private void Start()
    {
        if (transform.childCount > 0 && transform.GetChild(0).tag == "colliderVisual")
        {
            flatComponent = transform.GetChild(0).gameObject;
        }
        collider = GetComponent<Collider>();
    }

    public void ToggleResizeItem(string manualBool = "-")
    {
        if (manualBool == "-") { resize = !resize; }
        else
        {
            resize = false;
            if (manualBool == "true") { resize = true; }
        }

        if (resize)
        {
            startPoint = transform.position;
            moveTimer = 0;
            this.gameObject.layer = 8;
            baseScale = transform.localScale.x;
            initialDistance = Vector3.Distance(transform.position, UpdateController.cc3D.position);
            flatComponent.SetActive(false);
            collider.enabled = false;
        }
        else
        {
            this.gameObject.layer = 0;
            flatComponent.SetActive(true);
            collider.enabled = true;
        }
    }

    void LateUpdate()
    {
        if (UpdateController.cc2D.heldObj2D != this) { return; }        
        if (!resize) { return; }

        moveTimer += Time.deltaTime * 10;
        moveTimer = Mathf.Clamp(moveTimer,0,1);


        distanceFromPlayer = Vector3.Distance(UpdateController.switcher.hitPosition, UpdateController.cc3D.position);
        transform.localScale = Vector3.one * baseScale * (distanceFromPlayer / initialDistance);
        //transform.position = (UpdateController.switcher.hitPosition + ((UpdateController.cc3D.head.up) * 3 * (distanceFromPlayer / initialDistance) * baseScale));
        transform.position = Vector3.Lerp(startPoint, (UpdateController.switcher.hitPosition + ((UpdateController.cc3D.head.up) * 3 * (distanceFromPlayer / initialDistance) * baseScale)),moveTimer);
        transform.forward = Vector3.Lerp(transform.forward,UpdateController.cc3D.head.forward,moveTimer);
    }
}
