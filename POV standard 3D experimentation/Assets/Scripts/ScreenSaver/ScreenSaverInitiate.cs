using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ScreenSaverInitiate : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.anyKey || new Vector2(Input.GetAxisRaw("Mouse X"), Input.GetAxisRaw("Mouse Y")).magnitude > 0) { GameObject.FindObjectOfType<Boot_Fade>().fadeOpaque(); DestroyImmediate(this); }
    }
}
