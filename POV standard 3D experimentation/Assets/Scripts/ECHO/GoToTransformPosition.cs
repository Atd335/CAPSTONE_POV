using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoToTransformPosition : MonoBehaviour
{

    public Transform pos;

    void LateUpdate()
    {
        transform.position = pos.position;
    }
}
