using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class QoLDebuggingTools : MonoBehaviour
{
    // Start is called before the first frame update
    void Awake()
    {
        UpdateController.qol = this;
    }

    public void _Start()
    {
        Toggle2DCharacter(false);
    }

    public void Toggle2DCharacter(bool b,float x = -1, float y = -1, float z = -1)
    {
        UpdateController.cc2D.player.localScale = Vector3.zero;
        if (new Vector3(x, y, z) != Vector3.one * -1) { UpdateController.switcher.hitPosition = new Vector3(x, y, z); }
        else
        {
            bool r = Physics.Raycast(UpdateController.cc3D.head.position, UpdateController.cc3D.head.forward, out RaycastHit rh);
            UpdateController.switcher.hitPosition = rh.point;
        }

        UpdateController.cc2D.player.gameObject.SetActive(b);
    }

    // Update is called once per frame
    public void manualUpdate()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
        {
            Toggle2DCharacter(false);
        }
        if (Input.GetKeyDown(KeyCode.Alpha2))
        {
            Toggle2DCharacter(true);
        }

        if (Input.GetKeyUp(KeyCode.R)) { UpdateController.cc3D.DIE(); }
    }

    public Vector3Int roundVectorToInt(Vector3 v)
    {
        Vector3Int v2 = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        return v2;
    }
}
