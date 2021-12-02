using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class objProperties : MonoBehaviour
{
    [HideInInspector]
    public string objType = "";

    ColorContainer cc;

    public void makePlat()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
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
        mat.color = cc._black;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._black;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;

        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makePlatGreen()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "green") { return; }
        objType = "green";
        transform.tag = "green";
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
        mat.color = cc._green;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._green;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;

        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makePlatPurple()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "purple") { return; }
        objType = "purple";
        transform.tag = "purple";
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
        mat.color = cc._purple;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._purple;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;

        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makePlatBlue()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "blue") { return; }
        objType = "blue";
        transform.tag = "blue";
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
        mat.color = cc._blue;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._blue;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;

        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makePlatOrange()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "orange") { return; }
        objType = "orange";
        transform.tag = "orange";
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
        mat.color = cc._orange;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._orange;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;

        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makePlatYellow()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "yellow") { return; }
        objType = "yellow";
        transform.tag = "yellow";
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
        mat.color = cc._yellow;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._yellow;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;

        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }


    public void makeCutOut()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
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
        mat.color = cc._white;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._white;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;
        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makeDamage()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
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
        mat.color = cc._red;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._red;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;
        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makeInteractable()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "interact") { return; }
        objType = "interact";
        transform.tag = "interact";
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
        mat.color = cc._yellow;

        Material mat2 = new Material(Shader.Find("Standard"));
        mat2.color = cc._yellow;

        GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;
        this.gameObject.AddComponent<InteractableObjectScript>();
    }

    public void makeDoor()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "door") { return; }
        objType = "door";
        transform.tag = "door";
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
        mat.color = cc._doorColor;

        //Material mat2 = new Material(Shader.Find("Standard"));
        //mat2.color = cc._doorColor;

        //GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;
        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makeButton()
    {
        cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        if (objType == "2dbutton") { return; }
        objType = "2dbutton";
        transform.tag = "2dbutton";
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
        mat.color = cc._buttonColor;

        //Material mat2 = new Material(Shader.Find("Standard"));
        //mat2.color = cc._doorColor;

        //GetComponent<MeshRenderer>().material = mat2;
        vis.GetComponent<MeshRenderer>().material = mat;
        DestroyImmediate(GetComponent<InteractableObjectScript>());
    }

    public void makeFlat()
    {
        Material mat2 = new Material(Shader.Find("Unlit/Color"));
        mat2.color = GetComponent<MeshRenderer>().sharedMaterial.color;

        GetComponent<MeshRenderer>().material = mat2;
    }
    public void removeThisComponent()
    {
        transform.tag = "Untagged";
        clearChildColliders();
        DestroyImmediate(this);
    }

    void clearChildColliders()
    {
        if (transform.childCount > 0 && transform.GetChild(0).tag == "colliderVisual")
        {
            DestroyImmediate(transform.GetChild(0).gameObject);
        }
    }
}
