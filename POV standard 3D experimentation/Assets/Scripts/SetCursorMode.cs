using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SetCursorMode : MonoBehaviour
{

    public CursorLockMode mode;

    // Start is called before the first frame update
    void Start()
    {
        Cursor.lockState = mode;
    }
}
