using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class CanvasTransferScript : MonoBehaviour
{

    public Image player0;
    public Image Crosshair0;

    public Image player1;
    public Image windowToTranferTo;

    public Vector2 pos;
    public Vector2 size;

    

    void LateUpdate()
    {

        size = player0.rectTransform.sizeDelta;
        size.x /= 1920;
        size.y /= 1600;
        size.x *= windowToTranferTo.rectTransform.sizeDelta.x;
        size.y *= windowToTranferTo.rectTransform.sizeDelta.y;

        pos = player0.rectTransform.localPosition;
        pos.x /= 1920;
        pos.y /= 1600;

        pos += Vector2.one * .5f;

        pos.x *= windowToTranferTo.rectTransform.sizeDelta.x;
        pos.y *= windowToTranferTo.rectTransform.sizeDelta.y;

        player1.rectTransform.localPosition = pos;
        player1.rectTransform.sizeDelta = size;
        //

        player0.color = Color.clear;
        Crosshair0.color = Color.clear;
    }
}
