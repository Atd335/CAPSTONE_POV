using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Window_Canvas_Raycaster : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    Vector2 clickDownPos;
    Vector2 clickHoldDelta;

    RectTransform windowRT;
    Vector2 startSizeDelta;
    Vector2 startPosition;

    Vector2 windowSize;

    int interactMode = -1;
    //0 - resize
    //1 - drag window

    public static GameObject hoveredElement;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
        interactMode = -1;
    }
    void Update()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();

        m_Raycaster.Raycast(m_PointerEventData, results);

        if (Input.GetKeyDown(KeyCode.Mouse0) && results.Count>0) 
        { 
            clickDownPos = Input.mousePosition;
            windowRT = results[0].gameObject.transform.parent.GetComponent<RectTransform>();
            startSizeDelta = windowRT.sizeDelta;
            startPosition = windowRT.localPosition;

            if (results[0].gameObject.tag=="resizeCollider") { interactMode = 0; }
            else if (results[0].gameObject.tag=="dragCollider") { interactMode = 1; }
        }

        if (Input.GetKey(KeyCode.Mouse0) && windowRT)
        {
            clickHoldDelta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - clickDownPos;

            if (interactMode == 0)
            {
                resizeWindow();
            }
            else if (interactMode == 1)
            {
                dragWindow();
            }

        }

        if (Input.GetKeyUp(KeyCode.Mouse0))
        {
            windowRT = null;
            interactMode = -1;
        }

        if (results.Count > 0)
        {
            hoveredElement = results[0].gameObject;
        }
        else
        {
            hoveredElement = null;
        }

        if (hoveredElement && Input.GetKeyDown(KeyCode.Mouse0))
        {
            if (hoveredElement.GetComponent<IWindowButton>() != null)
            {
                hoveredElement.GetComponent<IWindowButton>().click();
            }
        }
    }

    void dragWindow()
    {
        Vector2 pos = startPosition + clickHoldDelta;
        pos.x = Mathf.Clamp(pos.x,0,Screen.width-32);
        pos.y = Mathf.Clamp(pos.y,-Screen.height+32,0);
        windowRT.localPosition = pos;
    }

    void resizeWindow()
    {
        clickHoldDelta.y *= -1;
        windowSize = startSizeDelta + clickHoldDelta;
        windowSize.x = Mathf.Clamp(windowSize.x, 200, 600);
        windowSize.y = Mathf.Clamp(windowSize.y, 120, 400);
        windowRT.sizeDelta = windowSize;

    }
}
