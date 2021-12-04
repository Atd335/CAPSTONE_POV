using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTransferV2 : MonoBehaviour
{

    RenderTexture camRT;
    RenderTexture canvasRT;

    RawImage camRaw;
    RawImage canvasRaw;

    public Camera visCam;
    public Camera canvasCam;

    public bool setNative = false;

    bool inWindow = false;

    private void Start()
    {
        if (!GameObject.FindGameObjectWithTag("3D PLAYER")) { return; }
        camRT = new RenderTexture(Screen.width, Screen.height, 0);
        camRT.filterMode = FilterMode.Point;
        canvasRT = new RenderTexture(Screen.width, Screen.height, 0);
        canvasRT.filterMode = FilterMode.Point;

        camRaw = GetComponentsInChildren<RawImage>()[0];
        canvasRaw = GetComponentsInChildren<RawImage>()[1];

        visCam = GameObject.Find("VisualCam").GetComponent<Camera>();
        visCam.targetTexture = camRT;

        canvasCam = GameObject.Find("CanvasCam").GetComponent<Camera>();
        canvasCam.targetTexture = canvasRT;

        GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>().renderMode = RenderMode.ScreenSpaceCamera;
        GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>().worldCamera = canvasCam;

        camRaw.texture = camRT;
        canvasRaw.texture = canvasRT;

        canvasCam.transform.localPosition = camRaw.transform.parent.GetComponent<RectTransform>().anchoredPosition3D;
        canvasCam.orthographicSize = camRaw.transform.parent.GetComponent<RectTransform>().anchoredPosition3D.y;

        if (setNative)
        {
            camRaw.SetNativeSize();
            canvasRaw.SetNativeSize();
        }

        if (GameObject.FindGameObjectWithTag("GameWindow"))
        {
            camRaw.transform.parent = GameObject.FindGameObjectWithTag("GameWindow").GetComponent<Window_Resizer>().contentSection.transform;
            camRaw.transform.position = GameObject.FindGameObjectWithTag("GameWindow").GetComponent<Window_Resizer>().contentSection.transform.position;
            inWindow = true;
        }
    }

    private void LateUpdate()
    {
        if (!inWindow) { return; }
        Vector2 v = GameObject.FindGameObjectWithTag("GameWindow").GetComponent<Window_Resizer>().contentSection.rectTransform.sizeDelta;

        camRaw.rectTransform.sizeDelta = v;
        canvasRaw.rectTransform.sizeDelta = v;


    }
}
