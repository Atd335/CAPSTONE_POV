using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DrawOnWalls : MonoBehaviour
{
    private void Awake()
    {
        UpdateController.dow = this;
    }

    Color[] c;

    float yResize;
    float xResize;

    // Start is called before the first frame update
    public void _Start()
    {
        c = new Color[10000];

        yResize = 1;
        xResize = 1;

        if (transform.localScale.x > transform.localScale.y)
        {
            yResize = transform.localScale.y / transform.localScale.x;
        }
        else
        { 
            xResize = transform.localScale.y / transform.localScale.x;
        }
    }

    // Update is called once per frame
    public void manualUpdate()
    {
        bool rc = Physics.Raycast(UpdateController.cc3D.head.position, UpdateController.cc3D.head.forward, out RaycastHit rch);

        if (rch.collider!=null && rch.collider.tag=="whiteBoard")
        {
            Texture2D tex = rch.collider.GetComponent<WhiteBoard>().texCopy;
            Vector2 v = new Vector2(tex.width,tex.height);
            v.x *= rch.textureCoord.x;
            v.y *= rch.textureCoord.y;

            Vector2Int realTexCoord = new Vector2Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y));

            if (Input.GetKey(KeyCode.Mouse1))
            {
                tex.SetPixels(realTexCoord.x,realTexCoord.y,Mathf.RoundToInt((tex.width/20) * xResize), Mathf.RoundToInt((tex.height / 20) * yResize),c);
                tex.Apply();
            }

            rch.collider.GetComponent<MeshRenderer>().material.mainTexture = tex;
        }
    }
}
