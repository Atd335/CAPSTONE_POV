using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class CloseToObjectEventInvoker : MonoBehaviour
{
    public UnityEvent closeEvent;
    public float threshold;
    public Transform otherTransform;

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, otherTransform.position) < threshold)
        {
            closeEvent.Invoke();
        }
    }
}
