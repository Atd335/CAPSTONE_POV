using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowAlwaysOnTop : MonoBehaviour
{
    void LateUpdate()
    {
        transform.SetAsLastSibling();
    }
}
