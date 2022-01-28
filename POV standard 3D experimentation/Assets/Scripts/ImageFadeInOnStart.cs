using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ImageFadeInOnStart : MonoBehaviour
{
    public AnimationCurve ac;
    public float timerSpd;
    float timer;

    public RawImage[] RIs;

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * timerSpd;
        timer = Mathf.Clamp(timer, 0, 1);

        foreach (RawImage ri in RIs)
        {
            ri.color = new Color(1,1,1,ac.Evaluate(timer));
        }
        if (timer == 1) 
        {
            if (GameObject.Find("BG Anim")) { Destroy(GameObject.Find("BG Anim")); }
            Destroy(this); 
        }
    }
}
