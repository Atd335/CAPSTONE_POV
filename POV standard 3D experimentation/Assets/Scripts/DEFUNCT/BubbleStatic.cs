using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BubbleStatic : MonoBehaviour
{
    void LateUpdate()
    {
        transform.localScale = Vector3.one;
        transform.rotation = Quaternion.Euler(0,0,0);
    }
}
