using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class attactToTransform : MonoBehaviour
{
    public Transform other;


    // Update is called once per frame
    void LateUpdate()
    {
        transform.position = other.position;
        transform.rotation = other.rotation;
        transform.localScale = other.localScale;
    }
}
