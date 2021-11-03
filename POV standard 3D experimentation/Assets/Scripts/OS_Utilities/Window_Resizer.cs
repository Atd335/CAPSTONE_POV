using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Resizer : MonoBehaviour
{
    public Image WindowBase;
    
    public Image[] resizers;

    Image left_rsz;
    Image right_rsz;
    Image up_rsz;
    Image down_rsz;

    void Start()
    {
        left_rsz = resizers[0];
        right_rsz = resizers[1];
        up_rsz = resizers[2];
        down_rsz = resizers[3];
    }

    void Update()
    {
        ResizeElements();
    }

    void ResizeElements()
    {
        Vector2 scl = WindowBase.rectTransform.sizeDelta;
    }
}
