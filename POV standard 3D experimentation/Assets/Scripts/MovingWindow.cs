using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovingWindow : MonoBehaviour
{
    Vector3 objPos;
    Vector3 maxPos;
    float moveSpeed;
 
    // Start is called before the first frame update
    void Start()
    {
        
        maxPos = new Vector3 (-10,0,0); 

    }

    // Update is called once per frame
    void Update()
    {
        transform.Translate(-12,6,-5);
        objPos = this.transform.position;
        if (objPos.x < maxPos.x)
        {

        }
        
    }
}
