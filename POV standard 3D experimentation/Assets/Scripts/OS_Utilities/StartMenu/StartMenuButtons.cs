using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class StartMenuButtons : MonoBehaviour
{
    public Color hoverColor;
    public Color clickColor;
    public Color normalColor;
    Color currentColor;
    Image img;
    bool hovered;

    public UnityEvent click;

    void Start()
    {
        img = GetComponent<Image>();
    }

    // Update is called once per frame
    void Update()
    {
        hovered = DesktopAsset_Cursor.hoveredElement == this.gameObject;
        
        currentColor = normalColor;
        if (hovered) { currentColor = hoverColor; }
        if (Input.GetKey(KeyCode.Mouse0) && hovered) { currentColor = clickColor; }

        if (hovered && Input.GetKeyUp(KeyCode.Mouse0)) 
        {
            click.Invoke();
        }

        img.color = Color.Lerp(img.color, currentColor, Time.deltaTime * 20);
    }

    public void closePop()
    {

        foreach (StartMenuButtons g in FindObjectsOfType<StartMenuButtons>())
        {
            g.popOut.SetActive(false);
        }

    }

    public GameObject popOut;
    public void popOutMenu(int mode = -1)
    {

        if (mode == -1) { popOut.SetActive(!popOut.activeInHierarchy); return; }
        else if (mode == 0)
        {
            popOut.SetActive(false);
        }
        else if (mode == 1)
        {
            popOut.SetActive(true);
        }
    }

}
