using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TargetIconSwapper : MonoBehaviour
{
    public Sprite spr1;
    public Sprite spr2;

    void Update()
    {

        Vector3 dir = GetComponent<GoToCanvasFromWorld>().transformToGoTo.position - UpdateController.cc3D.head.position;


        Physics.Raycast(UpdateController.cc3D.head.position, dir.normalized, out RaycastHit hit);
        bool wallBetween = hit.distance < Vector3.Distance(GetComponent<GoToCanvasFromWorld>().transformToGoTo.position,UpdateController.cc3D.head.position) - .2f;

        GetComponent<Image>().sprite = spr1;
        if (wallBetween)
        {
            GetComponent<Image>().sprite = spr2;
        }
    }
}
