using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraSway : MonoBehaviour
{
    private void Awake()
    {
        GetComponent<Camera>().enabled = true;
    }
    void Update()
    {
        transform.localRotation = Quaternion.Euler(17,0,Mathf.Sin(Time.time)*23f);
    }
}
