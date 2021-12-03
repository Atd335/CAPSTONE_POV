using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class Installer_Canvas_Raycaster : MonoBehaviour
{
    Transform canvasParent;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;
    

    public static GameObject hoveredElement;

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

        if (results.Count > 0)
        {
            hoveredElement = results[0].gameObject;
        }
        else
        {
            hoveredElement = null;
        }

        if (hoveredElement && Input.GetKeyUp(KeyCode.Mouse0))
        {
            if (hoveredElement.GetComponent<IWindowButton>() != null)
            {
                hoveredElement.GetComponent<IWindowButton>().click();
            }
        }
    }
}
