using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Installer_Buttons : MonoBehaviour, IWindowButton
{
    public bool disabled;
    float lerpSpd = 8;

    Color fillColor;
    Color outlineColor;

    public Color neutralFill;
    public Color hoverFill;
    public Color clickFill;
    public Color disableFill;

    public Color neutralOutline;
    public Color hoverOutline;
    public Color clickOutline;
    public Color disableOutline;

    public bool hovered;

    public Image outline;
    public Image fill;
    public Text text;


    void Start()
    {
        fillColor = neutralFill;
        outlineColor = neutralOutline;
    }

    void Update()
    {
        hovered = Installer_Canvas_Raycaster.hoveredElement == this.gameObject;

        if (hovered)
        {
            fillColor = Color.Lerp(fillColor, hoverFill, Time.deltaTime * lerpSpd);
            outlineColor = Color.Lerp(outlineColor, hoverOutline, Time.deltaTime * lerpSpd);
            if (Input.GetKey(KeyCode.Mouse0))
            {
                fillColor = Color.Lerp(fillColor, clickFill, Time.deltaTime * lerpSpd*2);
                outlineColor = Color.Lerp(outlineColor, clickOutline, Time.deltaTime * lerpSpd*2);
            }
        }
        else
        {
            fillColor = Color.Lerp(fillColor,neutralFill, Time.deltaTime * lerpSpd);
            outlineColor = Color.Lerp(outlineColor, neutralOutline, Time.deltaTime * lerpSpd);
        }

        if (disabled)
        {
            fillColor = disableFill;
            outlineColor = disableOutline;
            text.color = disableOutline;
        }
        else
        {
            text.color = Color.black;
        }

        fill.color = fillColor;
        outline.color = outlineColor;

    }

    public UnityEvent clickEvent;

    public void click()
    {
        if (disabled) { return; }
        clickEvent.Invoke();
    }

}
