using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using RealtimeCSG;
using InternalRealtimeCSG;
using CSGOperationType = RealtimeCSG.Foundation.CSGOperationType;

public class LevelBuilderMeshProperties : MonoBehaviour
{


    public Color modelColor = new Color(1,1,1,1);
    [HideInInspector]
    public string colString = "white";

    public void setColor(string color)
    {
        ColorContainer cc = GameObject.FindGameObjectWithTag("3D PLAYER").GetComponent<ColorContainer>();
        colString = color;
        this.gameObject.name = $"Model [{colString}]";
        switch (color)
        {
            case "black":
                modelColor = cc._black;
                break;
            case "white":
                modelColor = cc._white;
                break;
            case "red":
                modelColor = cc._red;
                break;
            case "purple":
                modelColor = cc._purple;
                break;
            case "green":
                modelColor = cc._green;
                break;
            case "orange":
                modelColor = cc._orange;
                break;
            case "blue":
                modelColor = cc._blue;
                break;
            default:
                break;
        }

        //CSG is so strange and finicky, before a build attempt, comment out the foreach loop below. this is an editor only tool anyways. 

        foreach (RealtimeCSG.Components.CSGBrush b in GetComponentsInChildren<RealtimeCSG.Components.CSGBrush>())
        {
            for (int i = 0; i < b.Shape.TexGens.Length; i++)
            {
                Material m = new Material(Shader.Find("Standard"));
                m.color = modelColor;
                b.Shape.TexGens[i].RenderMaterial = m;
                b.transform.position += Vector3.up * .0001f;
            }
        }
    }

    void Awake()
    {
        foreach (MeshFilter mf in GetComponentsInChildren<MeshFilter>())
        {
            mf.gameObject.AddComponent<objProperties>();
            //print(colString);
            switch (colString)
            {
                case "black":
                    mf.gameObject.GetComponent<objProperties>().makePlat();
                    break;
                case "white":
                    mf.gameObject.GetComponent<objProperties>().makeCutOut();
                    break;
                case "red":
                    mf.gameObject.GetComponent<objProperties>().makeDamage();
                    break;
                case "purple":
                    mf.gameObject.GetComponent<objProperties>().makePlatPurple();
                    break;
                case "green":
                    mf.gameObject.GetComponent<objProperties>().makePlatGreen();
                    break;
                case "orange":
                    mf.gameObject.GetComponent<objProperties>().makePlatOrange();
                    break;
                case "blue":
                    mf.gameObject.GetComponent<objProperties>().makePlatBlue();
                    break;
                case "yellow":
                    mf.gameObject.GetComponent<objProperties>().makePlatYellow();
                    break;
                default:
                    break;
            }
        }
        
    }
}
