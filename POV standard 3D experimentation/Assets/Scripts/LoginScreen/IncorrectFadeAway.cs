using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IncorrectFadeAway : MonoBehaviour
{

    Text t;
    Color c;
    Color cT;
    // Start is called before the first frame update
    void Start()
    {
        t =  GetComponent<Text>();
        c = t.color;
        c.a = 1;
        cT = c;
        cT.a = 0;
        t.color = cT;
    }

    void Update()
    {
        t.color = Color.Lerp(t.color, cT, Time.deltaTime);
    }
}
