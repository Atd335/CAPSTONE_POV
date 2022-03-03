using UnityEngine;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System.Collections.Generic;

public class DesktopAsset_Cursor : MonoBehaviour
{
    Transform canvasParent;

    GraphicRaycaster m_Raycaster;
    PointerEventData m_PointerEventData;
    EventSystem m_EventSystem;

    public static GameObject hoveredElement;
    public static GameObject selectedElement;
    void Start()
    {
        m_Raycaster = GetComponent<GraphicRaycaster>();
        m_EventSystem = GetComponent<EventSystem>();
        canvasParent = transform;
    }
    void Update()
    {
        if (Window_Canvas_Raycaster.hoveredElement != null) { return; }
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

        if (hoveredElement && hoveredElement.GetComponent<IconButtonFunctions>() != null)
        {
            if (Input.GetKeyDown(KeyCode.Mouse0))
            {
                hoveredElement.GetComponent<IconButtonFunctions>().clickDown();
            }
        }
    }
}
