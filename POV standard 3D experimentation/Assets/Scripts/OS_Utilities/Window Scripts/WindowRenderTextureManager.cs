using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindowRenderTextureManager : MonoBehaviour
{

    public Window_Resizer WR;
    public Window_Content_Manager WCM;
    RenderTexture RT;


    void Awake()
    {
        WCM.viewCam.targetTexture = RT;
    }

    // Update is called once per frame
    void Update()
    {
        RT = new RenderTexture(
            Mathf.RoundToInt(WR.contentSection.rectTransform.sizeDelta.x),
            Mathf.RoundToInt(WR.contentSection.rectTransform.sizeDelta.y),
            16, RenderTextureFormat.ARGB32);

        RT.Create();

        WCM.viewCam.targetTexture = RT;

        WR.contentSectionRT.texture = RT;
    }
}
