using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class BarFillController : MonoBehaviour
{
    float timer;
    public float duration;
    public AnimationCurve barFiller;

    public bool fillUp;

    void Update()
    {
        if (!fillUp) { return; }
        timer += Time.deltaTime / duration;
        transform.localScale = new Vector3(barFiller.Evaluate(timer),1,1);
    }
}
