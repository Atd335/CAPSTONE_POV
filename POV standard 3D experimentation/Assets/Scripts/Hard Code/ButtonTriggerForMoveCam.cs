using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ButtonTriggerForMoveCam : MonoBehaviour, trigger2D
{

    public MoveCameraTransition mct;
    public void trigger()
    {
        mct.triggerMe();
    }
}
