using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using TMPro;
public class QoLDebuggingTools : MonoBehaviour
{
    public TextMeshProUGUI debugText;

    // Start is called before the first frame update
    void Awake()
    {
        UpdateController.qol = this;
    }

    public void _Start()
    {
        Toggle2DCharacter(false);

    }

    public void debugPrint(string txt)
    {
        debugText.text = txt;
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
        if (Input.GetKeyDown(KeyCode.K))
        {
            Toggle2DCharacter(true);
        }

        if (Input.GetKeyDown(KeyCode.Alpha1)) { UpdateController.col.ChangeColor("platform", "black"); }
        if (Input.GetKeyDown(KeyCode.Alpha2)) { UpdateController.col.ChangeColor("platform", "blue"); }
        if (Input.GetKeyDown(KeyCode.Alpha3)) { UpdateController.col.ChangeColor("platform", "green"); }
        if (Input.GetKeyDown(KeyCode.Alpha4)) { UpdateController.col.ChangeColor("platform", "orange"); }
        if (Input.GetKeyDown(KeyCode.Alpha5)) { UpdateController.col.ChangeColor("platform", "purple"); }
        if (Input.GetKeyDown(KeyCode.Alpha6)) { UpdateController.col.ChangeColor("platform", "white"); }

        if (Input.GetKeyUp(KeyCode.R)) { UpdateController.cc3D.DIE(); }
    }

    public Vector3Int roundVectorToInt(Vector3 v)
    {
        Vector3Int v2 = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        return v2;
    }
}
