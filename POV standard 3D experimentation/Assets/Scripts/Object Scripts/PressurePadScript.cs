using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PressurePadScript : MonoBehaviour
{
    public bool active;

    void Update()
    {
        Collider[] items = Physics.OverlapBox(transform.position, (transform.localScale * 1.1f) / 2, transform.rotation);
        foreach (Collider item in items)
        {
            active = false;
            if (item.tag == "interact")
            {
                active = true;
            }
        }
    }
}
