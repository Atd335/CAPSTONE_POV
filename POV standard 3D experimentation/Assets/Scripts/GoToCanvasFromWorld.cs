using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GoToCanvasFromWorld : MonoBehaviour
{

    Camera cam;
    public Transform transformToGoTo;

    public static GoToCanvasFromWorld g;

    private void Start()
    {
        cam = GameObject.Find("VisualCam").GetComponent<Camera>();
        g = this;
    }

    public void AssignTransform(Transform t)
    {
        transformToGoTo = t;
    }

    // Update is called once per frame
    void LateUpdate()
    {
        if (transformToGoTo == null) { this.gameObject.SetActive(false); }
        Vector3 v = cam.WorldToScreenPoint(transformToGoTo.position);
        transform.position = v;
        GetComponent<Image>().enabled = GetComponent<RectTransform>().anchoredPosition3D.z > 0;
    }
}
