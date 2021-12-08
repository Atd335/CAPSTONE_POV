using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OneTimeLerp : MonoBehaviour
{
    public Vector2 v;
    public float spd;
    void Update()
    {
        GetComponent<RectTransform>().anchoredPosition = Vector2.Lerp(GetComponent<RectTransform>().anchoredPosition, v, Time.deltaTime * spd);
    }
}
