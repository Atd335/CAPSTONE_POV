using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class KeyPressEventExecutor : MonoBehaviour
{

    public UnityEvent pressEvent;
    public KeyCode key;

    void Update()
    {
        if (Input.GetKeyDown(key)) {pressEvent.Invoke();}

    }
}
