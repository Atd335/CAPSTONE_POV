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

    Vector2 windowSize;

    void Start()
    {
        //Fetch the Raycaster from the GameObject (the Canvas)
        m_Raycaster = GetComponent<GraphicRaycaster>();
        //Fetch the Event System from the Scene
        m_EventSystem = GetComponent<EventSystem>();
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
        }

        if (Input.GetKey(KeyCode.Mouse0) && windowRT)
        {
            clickHoldDelta = new Vector2(Input.mousePosition.x, Input.mousePosition.y) - clickDownPos;
            clickHoldDelta.y *= -1;
            windowSize = startSizeDelta + clickHoldDelta;
            windowSize.x = Mathf.Clamp(windowSize.x,200,600);
            windowSize.y = Mathf.Clamp(windowSize.y,120,400);

            windowRT.sizeDelta = windowSize;
        }

    }
}
