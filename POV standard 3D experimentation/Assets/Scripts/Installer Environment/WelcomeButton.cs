using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class WelcomeButton : MonoBehaviour, IWindowButton
{
    public UnityEvent clickEvent;

    void Update()
    {
        if (Installer_Canvas_Raycaster.hoveredElement == this.gameObject)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one*1.025f, Time.deltaTime * 8);
            if (Input.GetKey(KeyCode.Mouse0)) { transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * .95f, Time.deltaTime * 8); }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1f, Time.deltaTime * 8);
        }
    }

    public void click()
    {
        clickEvent.Invoke();
    }
}
