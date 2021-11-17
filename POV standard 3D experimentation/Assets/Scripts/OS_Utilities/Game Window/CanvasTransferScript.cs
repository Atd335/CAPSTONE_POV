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

    public Vector2Int canvasSize;

    void LateUpdate()
    {
        canvasSize.x = 1920;
        canvasSize.y = (1920 * Screen.height) / Screen.width;

        size = player0.rectTransform.sizeDelta;
        size.x /= canvasSize.x;
        size.y /= canvasSize.y;
        size.x *= windowToTranferTo.rectTransform.sizeDelta.x;
        size.y *= windowToTranferTo.rectTransform.sizeDelta.y;

        pos = player0.rectTransform.localPosition;
        pos.x /= canvasSize.x;
        pos.y /= canvasSize.y;

        pos += Vector2.one * .5f;

        pos.x *= windowToTranferTo.rectTransform.sizeDelta.x;
        pos.y *= windowToTranferTo.rectTransform.sizeDelta.y;

        player1.rectTransform.localPosition = pos;
        player1.rectTransform.sizeDelta = size;
        //

        player0.color = Color.clear;
        Crosshair0.color = Color.clear;

        player0.enabled = UpdateController.switcher.playerOnScreen;
    }
}
