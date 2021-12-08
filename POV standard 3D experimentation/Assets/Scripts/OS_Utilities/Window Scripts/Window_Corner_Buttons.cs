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

    Window_Resizer windowResizer;
    public GameObject othertodestroy;


    void Start()
    {
        button = GetComponent<Image>();
        buttonGO = this.gameObject;
        currentColor = Color.white;
        windowResizer = transform.parent.parent.parent.GetComponent<Window_Resizer>();
    }

    public void click()
    {
        button.color = clickColor;
        switch (cornerButtonType)
        {
            case 0:
                windowResizer.maximized = !windowResizer.maximized;
                if (windowResizer.maximized)
                {
                    windowResizer.maximizeStart = windowResizer.WindowBase.rectTransform.localPosition;
                    windowResizer.maximizeStartScale = windowResizer.WindowBase.rectTransform.sizeDelta;
                }
                else
                {
                    windowResizer.WindowBase.rectTransform.localPosition = windowResizer.maximizeStart;
                    windowResizer.WindowBase.rectTransform.sizeDelta = windowResizer.maximizeStartScale;
                }
                break;
            case 1:
                Destroy(windowResizer.gameObject);
                if (othertodestroy) { Destroy(othertodestroy); }
                break;
            default:
                break;
        }
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
