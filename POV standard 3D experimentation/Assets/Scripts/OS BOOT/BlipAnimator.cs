using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BlipAnimator : MonoBehaviour
{

    Image[] blips;


    void Start()
    {
        blips = GetComponentsInChildren<Image>();
    }

    int fr;
    public int boingSpd;

    int boingCounter = 0;

    float xPos;

    void FixedUpdate()
    {
        fr++;

        if (fr % boingSpd == 0)
        {
            blips[boingCounter].rectTransform.sizeDelta = new Vector2(18, 18);
            boingCounter++;

            if (boingCounter >= blips.Length) { boingCounter = 0; }
        }

        foreach (Image blip in blips)
        {
            blip.rectTransform.sizeDelta = Vector2.Lerp(blip.rectTransform.sizeDelta, new Vector2(16,16), Time.deltaTime * 8);
        }

        xPos += Time.fixedDeltaTime * 200;
        xPos = Mathf.Clamp(xPos, -72, 634);
        if (xPos == 634) { xPos = -72; }

        GetComponent<RectTransform>().anchoredPosition = new Vector2(xPos,0);
    }
}
