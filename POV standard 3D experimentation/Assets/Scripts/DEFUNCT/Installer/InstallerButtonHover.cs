using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallerButtonHover : MonoBehaviour
{

    public Color hoveredColor;
    public Color hoveredColorOutline;
    public Color normalColor;
    public Color normalColorOutline;

    public Color pressedColor;

    Image I;
    Image buttonOutline;

    public Vector2 dimensions;

    private void Start()
    {
        I = GetComponent<Image>();
        buttonOutline = transform.parent.GetComponent<Image>();
        dimensions.x = I.rectTransform.sizeDelta.x;
        dimensions.y = I.rectTransform.sizeDelta.y;
    }

    void Update()
    {

        if (GraphicRaycasterRaycasterExample.hoveredButtonID == this.gameObject.name)
        {
            if (Input.GetKey(KeyCode.Mouse0))
            {
                I.color = Color.Lerp(I.color, pressedColor, Time.deltaTime * 6);
            }
            else
            {
                I.color = Color.Lerp(I.color, hoveredColor, Time.deltaTime * 6);
            }
            buttonOutline.color = Color.Lerp(buttonOutline.color,hoveredColorOutline,Time.deltaTime*6);
            I.rectTransform.sizeDelta = Vector2.Lerp(I.rectTransform.sizeDelta,new Vector2(146,32),Time.deltaTime * 6);
        }
        else
        {
            I.color = Color.Lerp(I.color, normalColor, Time.deltaTime * 6);
            buttonOutline.color = Color.Lerp(buttonOutline.color, normalColorOutline, Time.deltaTime * 6);
            I.rectTransform.sizeDelta = Vector2.Lerp(I.rectTransform.sizeDelta, dimensions, Time.deltaTime * 6);
        }
    }
}
