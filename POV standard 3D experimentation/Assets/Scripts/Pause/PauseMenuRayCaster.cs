//Attach this script to your Canvas GameObject.
//Also attach a GraphicsRaycaster component to your canvas by clicking the Add Component button in the Inspector window.
//Also make sure you have an EventSystem in your hierarchy.

using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class PauseMenuRayCaster : MonoBehaviour
{
    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public static GameObject hoveredObject;

    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
    }

    void Update()
    {
        m_PointerEventData = new PointerEventData(m_EventSystem);
        m_PointerEventData.position = Input.mousePosition;
        List<RaycastResult> results = new List<RaycastResult>();
        m_Raycaster.Raycast(m_PointerEventData, results);

        if (results.Count > 0)
        {
            hoveredObject = results[0].gameObject;
            if (hoveredObject.GetComponent<PauseMenuButton>() && Input.GetKeyDown(KeyCode.Mouse0)) { hoveredObject.GetComponent<PauseMenuButton>().onClick.Invoke(); }
        }
        else
        {
            hoveredObject = null;
        }

    }
}