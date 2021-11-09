using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScaleResizer : MonoBehaviour
{

    public bool resize;
    bool triggered;

    public float distanceFromPlayer;
    public float initialDistance;
    float baseScale;


    void Start()
    {
        
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (resize)
        {
            this.gameObject.layer = 8;
            if (!triggered)
            {
                baseScale = transform.localScale.x;
                initialDistance = Vector3.Distance(transform.position, UpdateController.cc3D.position);
                triggered = true;
            }

            distanceFromPlayer = Vector3.Distance(transform.position,UpdateController.cc3D.position);            
            transform.localScale = Vector3.one * baseScale * (distanceFromPlayer/initialDistance);

        }
        else
        {
            this.gameObject.layer = 0;
            triggered = false;
        }
    }
}
