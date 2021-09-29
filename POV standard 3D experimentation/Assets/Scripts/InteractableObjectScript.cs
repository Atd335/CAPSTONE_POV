using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractableObjectScript : MonoBehaviour
{

    GameObject colliderComponent;

    private void Start()
    {
        if (transform.childCount > 0 && transform.GetChild(0).tag == "colliderVisual")
        {
            colliderComponent = transform.GetChild(0).gameObject;
        }
    }

    void LateUpdate()
    {
        if (this.gameObject != UpdateController.cc2D.interactingObject) 
        {
            if (colliderComponent)
            {
                colliderComponent.SetActive(true);
            }
            GetComponent<Collider>().enabled = true;
            return; 
        }


        if (colliderComponent)
        {
            colliderComponent.SetActive(false);
            GetComponent<Collider>().enabled = false;
        }
        transform.position = UpdateController.switcher.hitPosition + 2 * (UpdateController.cc3D.head.up * (GetComponent<ScaleResizer>().distanceFromPlayer/ GetComponent<ScaleResizer>().initialDistance));
    }
}
