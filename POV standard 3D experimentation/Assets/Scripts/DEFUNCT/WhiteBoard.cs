using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WhiteBoard : MonoBehaviour
{
    [HideInInspector]
    public Texture2D texCopy;

    public Texture2D tex;
    private void Awake()
    {
        texCopy = new Texture2D(tex.width,tex.height);
        texCopy.SetPixels(0,0,texCopy.width,texCopy.height,tex.GetPixels());
        texCopy.Apply();
    }
}
