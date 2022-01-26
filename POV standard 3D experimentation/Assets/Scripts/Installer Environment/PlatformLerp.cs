using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformLerp : MonoBehaviour
{
    // Update is called once per frame
    void Update()
    {
        //print(this.gameObject.name.Split(':')[2]);
        Vector3 v = new Vector3(float.Parse(this.gameObject.name.Split(':')[1]), float.Parse(this.gameObject.name.Split(':')[2]), float.Parse(this.gameObject.name.Split(':')[3]));
        transform.localPosition = Vector3.Lerp(transform.localPosition, v, Time.deltaTime * 4f);
    }
}
