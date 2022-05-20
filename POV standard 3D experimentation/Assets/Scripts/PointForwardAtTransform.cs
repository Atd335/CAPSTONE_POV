using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PointForwardAtTransform : MonoBehaviour
{

    public Transform pointer;

    // Update is called once per frame
    void Update()
    {
        transform.forward = (pointer.position - transform.position).normalized;
    }
}
