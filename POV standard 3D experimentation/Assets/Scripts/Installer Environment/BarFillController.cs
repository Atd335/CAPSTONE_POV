using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class BarFillController : MonoBehaviour
{
    float timer;
    public float duration;
    public AnimationCurve barFiller;

    public bool fillUp;
    public bool full;

    public UnityEvent fakeFull;
    bool fulltriggered;

    void Update()
    {
        if (!fillUp) { return; }
        timer += Time.deltaTime / duration;
        timer = Mathf.Clamp(timer,0,1);
        full = timer == 1;

        transform.localScale = new Vector3(barFiller.Evaluate(timer), 1, 1);

        if (full && !fulltriggered)
        {
            fakeFull.Invoke();
            fulltriggered = true;
        }

    }
}
