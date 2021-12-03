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

    private void Start()
    {
        camRT = new RenderTexture(Screen.width, Screen.height, 0);
        canvasRT = new RenderTexture(Screen.width, Screen.height, 0);

        camRaw = GetComponentsInChildren<RawImage>()[0];
        canvasRaw = GetComponentsInChildren<RawImage>()[1];

        visCam = GameObject.Find("VisualCam").GetComponent<Camera>();
        visCam.targetTexture = camRT;

        canvasCam = GameObject.Find("CanvasCam").GetComponent<Camera>();
        canvasCam.targetTexture = canvasRT;

        camRaw.texture = camRT;
        canvasRaw.texture = canvasRT;

        camRaw.SetNativeSize();
        canvasRaw.SetNativeSize();
    }

    private void LateUpdate()
    {
        
    }

}
