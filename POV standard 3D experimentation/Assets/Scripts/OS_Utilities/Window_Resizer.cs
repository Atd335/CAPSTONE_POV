using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Resizer : MonoBehaviour
{
    public Image WindowBase;

    public Image topBar;

    void Start()
    {

    }

    void Update()
    {
        ResizeElements();
    }

    void ResizeElements()
    {
        Vector2 scl = WindowBase.rectTransform.sizeDelta;
        topBar.rectTransform.sizeDelta = new Vector2(scl.x, 32);
    }
}
