using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseToObjectEventInvoker : MonoBehaviour
{
    public UnityEvent closeEvent;
    public float threshold;
    public Transform otherTransform;

    public bool TwoDimensional;
    Camera visCam;

    // Update is called once per frame
    void Update()
    {
        if (TwoDimensional && GetComponent<Renderer>().isVisible)
        {
            visCam = UpdateController.imageCap.VisualCamera;
            Vector2 v1 = visCam.WorldToScreenPoint(transform.position);
            Vector2 v2 = visCam.WorldToScreenPoint(otherTransform.position);

            if (Vector2.Distance(v1, v2) < threshold)
            {
                closeEvent.Invoke();
            }

        }
        else
        { 
            if (Vector3.Distance(transform.position, otherTransform.position) < threshold)
            {
                closeEvent.Invoke();
            }
        }
    }
}
