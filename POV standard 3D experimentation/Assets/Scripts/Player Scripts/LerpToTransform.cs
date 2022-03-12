using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LerpToTransform : MonoBehaviour
{

    public Transform TransformToAttachTo;
    public float lerpStrength;

    public bool snapOnFPS;

    public void setTransform(Transform t)
    {
        TransformToAttachTo = t;
    }
    private void LateUpdate()
    {
        if (TransformToAttachTo == null) { return; }
        transform.position = Vector3.Lerp(transform.position,TransformToAttachTo.position, Time.deltaTime * lerpStrength);
        if (snapOnFPS && UpdateController.switcher.fpsMode) { transform.position = TransformToAttachTo.position; }
    }
}
