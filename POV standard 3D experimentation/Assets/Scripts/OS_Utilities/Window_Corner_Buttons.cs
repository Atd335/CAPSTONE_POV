using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Corner_Buttons : MonoBehaviour, IWindowButton
{

    Image button;
    GameObject buttonGO;
    public Color hoverColor;
    public Color clickColor;
    public Color normalColor;

    Color currentColor;

    bool hovered;

    public int cornerButtonType;

    void Start()
    {
        button = GetComponent<Image>();
        buttonGO = this.gameObject;
        currentColor = Color.white;
    }

    public void click()
    {
        button.color = clickColor;
    }

    void Update()
    {
        hovered = buttonGO == Window_Canvas_Raycaster.hoveredElement;

        if (hovered)
        {
            currentColor = hoverColor;
        }
        else
        {
            currentColor = normalColor;
        }

        button.color = Color.Lerp(button.color, currentColor, Time.deltaTime * 10);
    }
}
