using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DesktopBackgroundEnabler : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Camera>().enabled = true;
    }
}
