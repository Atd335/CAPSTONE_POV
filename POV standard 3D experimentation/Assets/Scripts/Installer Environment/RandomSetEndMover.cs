using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomSetEndMover : MonoBehaviour
{
    public Vector2 startPos;
    public float time;
    public AnimationCurve bounce;
    public float bouncemag;

    float offset;

    float y;

    void Start()
    {
        startPos = GetComponent<RectTransform>().anchoredPosition;
        y = Random.Range(0,10f);
    }

    // Update is called once per frame
    void Update()
    {
        time -= Time.deltaTime;
        time = Mathf.Clamp(time, 0,999);
        offset = 1 + Mathf.PerlinNoise(time, y);
        offset /= 4f;
        GetComponent<RectTransform>().anchoredPosition = startPos + new Vector2(0,bounce.Evaluate(time*offset)*bouncemag);

    }
}
