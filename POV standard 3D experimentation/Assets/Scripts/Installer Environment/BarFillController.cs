using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarFillController : MonoBehaviour
{
    float timer;
    public float duration;
    public AnimationCurve barFiller;

    public bool fillUp;
    public bool full;
    void Update()
    {
        if (!fillUp) { return; }
        timer += Time.deltaTime / duration;

        if (!full)
        {
            transform.localScale = new Vector3(barFiller.Evaluate(timer), 1, 1);
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, new Vector3(1, 1, 1), Time.deltaTime * 15f);
        }
    }
}
