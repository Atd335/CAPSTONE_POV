using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlatTexManager : MonoBehaviour
{

    float lowPacity = .3f;
    float highPacity = 1f;

    public Image white;

    void Update()
    {
        if (UpdateController.switcher.fpsMode)
        {
            white.color = Color.Lerp(white.color, new Color(1,1,1,0), Time.deltaTime * 10);
            GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, new Color(1,1,1,lowPacity), Time.deltaTime * 10);
        }
        else
        {
            white.color = Color.Lerp(white.color, new Color(1, 1, 1, .8f), Time.deltaTime * 10);
            GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, new Color(1, 1, 1, highPacity), Time.deltaTime * 10);
        }
    }
}
