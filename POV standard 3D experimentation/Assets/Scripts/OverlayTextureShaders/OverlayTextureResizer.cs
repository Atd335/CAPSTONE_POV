using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OverlayTextureResizer : MonoBehaviour
{
    public RenderTexture rt;

    public void awake()
    {  
        rt.width = Screen.width;
        rt.height = Screen.height;
    }

}
