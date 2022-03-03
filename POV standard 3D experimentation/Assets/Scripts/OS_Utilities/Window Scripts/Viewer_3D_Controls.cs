using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Viewer_3D_Controls : MonoBehaviour
{


    Transform camPivot;
    Transform cam;

    void Start()
    {
        camPivot = GameObject.FindGameObjectWithTag("3DView_Pivot").transform;
        cam = camPivot.GetChild(0);
        vector = new Vector3(0, 0, 0);
        zDistLerp = -5;
        zDist = -5;
    }
    Vector3 vector;

    public float zDist;
    public float zDistLerp;

    void Update()
    {
        if (Window_Canvas_Raycaster.hoveredElement == this.gameObject) 
        {
            zDistLerp += Input.GetAxisRaw("Mouse ScrollWheel") * Time.deltaTime * 900f;
            zDistLerp = Mathf.Clamp(zDistLerp, -10,-5);
            zDist = Mathf.Lerp(zDist,zDistLerp, Time.deltaTime * 12f);
            cam.transform.localPosition = new Vector3(0,0,zDist);

            if (Input.GetKey(KeyCode.Mouse0))
            {
                vector += new Vector3(-Input.GetAxis("Mouse Y"), Input.GetAxis("Mouse X")) * Time.deltaTime * 10000;
                vector.x = Mathf.Clamp(vector.x, -90f, 90f);
                camPivot.rotation = Quaternion.Lerp(camPivot.rotation, Quaternion.Euler(vector), Time.deltaTime * 10);
            }
        }
    }
}
