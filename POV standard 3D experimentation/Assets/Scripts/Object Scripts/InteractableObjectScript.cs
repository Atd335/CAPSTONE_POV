using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{

    GameObject flatComponent;
    Collider collider;

    public bool hasRigidBody;

    //Resizing
    public bool resize;
    bool triggered;
    float distanceFromPlayer;
    float initialDistance;
    float baseScale;
    float distanceFromHitPoint;

    Vector3 startPoint;
    float moveTimer;

    //point
    Vector3 spawnPos;
    Vector3 spawnScale;
    Vector3 spawnRot;

    MeshRenderer mrenderer;
    Rigidbody rb;

    public bool dontRescale;

    private void Start()
    {
        mrenderer = GetComponent<MeshRenderer>();
        if (transform.childCount > 0 && transform.GetChild(0).tag == "colliderVisual")
        {
            flatComponent = transform.GetChild(0).gameObject;
        }
        collider = GetComponent<Collider>();
        spawnPos = transform.position;
        spawnScale = transform.localScale;
        spawnRot = transform.rotation.eulerAngles;

        if (hasRigidBody) 
        { 
            this.gameObject.AddComponent<Rigidbody>();
            rb = this.gameObject.GetComponent<Rigidbody>();
        }

    }

    public void resetMe()
    {
        transform.position = spawnPos;
        transform.localScale = spawnScale;
        transform.rotation = Quaternion.Euler(spawnRot);
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
        if (hasRigidBody) { rb.isKinematic = UpdateController.cc2D.heldObj2D == this; }
        if (UpdateController.cc2D.heldObj2D && UpdateController.cc2D.heldObj2D != this) { return; }        
        if (!resize) { return; }

        moveTimer += Time.deltaTime * 8;
        moveTimer = Mathf.Clamp(moveTimer,0,1);


        distanceFromPlayer = Vector3.Distance(UpdateController.switcher.hitPosition, UpdateController.cc3D.position);
        if (!dontRescale)
        {
            transform.localScale = Vector3.one * baseScale * (distanceFromPlayer / initialDistance);
        }
        //transform.position = (UpdateController.switcher.hitPosition + ((UpdateController.cc3D.head.up) * 3 * (distanceFromPlayer / initialDistance) * baseScale));
        transform.position = Vector3.Lerp(startPoint, (UpdateController.switcher.hitPosition + ((UpdateController.cc3D.head.up) * 1 * (distanceFromPlayer / initialDistance) * baseScale)),moveTimer);
        transform.forward = Vector3.Lerp(transform.forward,UpdateController.cc3D.head.forward,moveTimer);

        if (!mrenderer.isVisible && UpdateController.cc2D.heldObj2D == this) { resetMe(); }

        
    }
}
