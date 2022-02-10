using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FlatTexManager : MonoBehaviour
{

    float lowPacity = .3f;
    float highPacity = 1f;

    void Update()
    {
        if (UpdateController.switcher.fpsMode)
        {
            GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, new Color(1,1,1,lowPacity), Time.deltaTime * 10);
        }
        else
        {
            GetComponent<RawImage>().color = Color.Lerp(GetComponent<RawImage>().color, new Color(1, 1, 1, highPacity), Time.deltaTime * 10);
        }
    }
}
