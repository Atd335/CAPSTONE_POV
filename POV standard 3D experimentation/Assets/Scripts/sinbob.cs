using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class sinbob : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public float spd;
    public float mag;
    public Vector3 pos;

    // Update is called once per frame
    void Update()
    {
        transform.position = pos + new Vector3(0,0,Mathf.Sin(Time.time * spd)*mag);
    }
}
