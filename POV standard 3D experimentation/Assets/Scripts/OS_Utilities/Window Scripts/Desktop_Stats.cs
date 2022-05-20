using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class Desktop_Stats : MonoBehaviour
{

    public Text dateAndTime;
    public Image batteryCharge;
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        dateAndTime.text = $"{System.DateTime.Now.ToString().Split(' ')[0].Split('/')[0]}/{System.DateTime.Now.ToString().Split(' ')[0].Split('/')[1]}/2006\n{System.DateTime.Now.ToString().Split(' ')[1]} {System.DateTime.Now.ToString().Split(' ')[2]}";

        float bc = SystemInfo.batteryLevel;
        if (bc < 0) { bc = 100; }
        batteryCharge.transform.localScale = new Vector3(bc, 1,1);
    }
}
