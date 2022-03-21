using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class fileController_Installer : MonoBehaviour
{


    public bool held;
    public UnityEvent moveOn;

    public Vector3[] points;
    int currentPoint;
    // Start is called before the first frame update
    void Start()
    {
        currentPoint = 0;
        transform.position = points[currentPoint];
    }

    // Update is called once per frame
    void Update()
    {
        //print(Vector2.Distance(UpdateController.cc2D.player.position, UpdateController.imageCap.VisualCamera.WorldToScreenPoint(transform.position)));

        if (Vector2.Distance(UpdateController.cc2D.player.position, UpdateController.imageCap.VisualCamera.WorldToScreenPoint(transform.position)) < 35f) { held = true; }



        if (held)
        {
            Vector3 vv = UpdateController.cc2D.player.position;
            vv.z = 2.87f;
            Vector3 v = UpdateController.imageCap.VisualCamera.ScreenToWorldPoint(vv);
            transform.position = v;


            if (Vector3.Distance(transform.position, new Vector3(1.45035255f, -0.845372558f, 2.86999989f)) < .2f) 
            {
                currentPoint++;
                currentPoint = Mathf.Clamp(currentPoint,0,points.Length-1);
                transform.position = points[currentPoint];
                held = false;
                moveOn.Invoke(); 
            }
        }

    }
}
