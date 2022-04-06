using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class TextLoadingBar : MonoBehaviour
{

    public int maxLoad;
    int loadprogress;
    string loadString;

    Text text;

    public AnimationCurve loadCurve;
    public float loadSpeed = 1;
    float loadTimer;

    public UnityEvent loaded;
    bool triggered;
    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        loadTimer = 0;
        triggered = false;
    }

    // Update is called once per frame
    void Update()
    {
        if (triggered) { return; }
        if (loadSpeed != 0) { loadTimer += Time.deltaTime * loadSpeed; }
        else { loadTimer += Time.deltaTime; }
        
        loadTimer = Mathf.Clamp(loadTimer,0,1);

        loadprogress = Mathf.RoundToInt(loadCurve.Evaluate(loadTimer) * maxLoad);
        loadprogress = Mathf.Clamp(loadprogress, 0, maxLoad);

        string ls = string.Empty;
        for (int i = 0; i < loadprogress; i++)
        {
            ls += "*";
        }

        loadString = $"[{ls}]";

        text.text = loadString;

        if (loadTimer==1) { loaded.Invoke(); triggered = true; }
    }
}
