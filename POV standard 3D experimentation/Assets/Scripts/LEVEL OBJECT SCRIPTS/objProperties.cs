using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objProperties : MonoBehaviour
{
    [HideInInspector]
    public string objType = "";

    public void makePlat()
    {
        if (objType == "solid") { return; }
        objType = "solid";
        transform.tag = "plat";
        clearChildColliders();

        GameObject vis = new GameObject(this.gameObject.name+" 2D");
        vis.tag = "colliderVisual";
        vis.layer = 6;
        vis.transform.parent = transform;
        vis.transform.localScale = Vector3.one;
        vis.transform.localPosition = Vector3.zero;
        vis.transform.localRotation = Quaternion.Euler(Vector3.zero);
        vis.AddComponent<MeshFilter>();
        vis.GetComponent<MeshFilter>().sharedMesh = transform.GetComponent<MeshFilter>().sharedMesh;
        vis.AddComponent<MeshRenderer>();

        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = Color.black;

        vis.GetComponent<MeshRenderer>().material = mat;


    }

    public void makeCutOut()
    {
        if (objType == "cutout") { return; }
        objType = "cutout";
        transform.tag = "cut";
        clearChildColliders();

        GameObject vis = new GameObject(this.gameObject.name + " 2D");
        vis.tag = "colliderVisual";
        vis.layer = 6;
        vis.transform.parent = transform;
        vis.transform.localScale = Vector3.one;
        vis.transform.localPosition = Vector3.zero;
        vis.transform.localRotation = Quaternion.Euler(Vector3.zero);
        vis.AddComponent<MeshFilter>();
        vis.GetComponent<MeshFilter>().sharedMesh = transform.GetComponent<MeshFilter>().sharedMesh;
        vis.AddComponent<MeshRenderer>();

        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = Color.white;

        vis.GetComponent<MeshRenderer>().material = mat;
    }

    public void makeDamage()
    {
        if (objType == "damage") { return; }
        objType = "damage";
        transform.tag = "ouch";
        clearChildColliders();

        GameObject vis = new GameObject(this.gameObject.name + " 2D");
        vis.tag = "colliderVisual";
        vis.layer = 6;
        vis.transform.parent = transform;
        vis.transform.localScale = Vector3.one;
        vis.transform.localPosition = Vector3.zero;
        vis.transform.localRotation = Quaternion.Euler(Vector3.zero);
        vis.AddComponent<MeshFilter>();
        vis.GetComponent<MeshFilter>().sharedMesh = transform.GetComponent<MeshFilter>().sharedMesh;
        vis.AddComponent<MeshRenderer>();

        Material mat = new Material(Shader.Find("Unlit/Color"));
        mat.color = Color.red;

        vis.GetComponent<MeshRenderer>().material = mat;
    }

    void clearChildColliders()
    {
        if (transform.childCount > 0 && transform.GetChild(0).tag == "colliderVisual")
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
