using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UpdateController : MonoBehaviour
{
    public static UpdateController UC;

    public static ImageCap imageCap;
    public static Character_Controller_2D cc2D;

    void Start()
    {
        UC = this;

        imageCap._Start();
        cc2D._Start();
    }

    void Update()
    {
        imageCap.manualUpdate();
        cc2D.manualUpdate();
    }
}
