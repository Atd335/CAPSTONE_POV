using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Installer_BG_Scroll : MonoBehaviour
{
    Vector2 xy;
    public Vector2 scrollDir;
    void Update()
    {
        xy += scrollDir * Time.deltaTime;

        GetComponent<RawImage>().uvRect = new Rect(xy.x, xy.y, 15, 12.225f);
    }
}
