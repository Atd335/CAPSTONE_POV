using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(LevelBuilderMeshProperties))]
public class LevelBuilderMeshPropertiesInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        LevelBuilderMeshProperties myScript = (LevelBuilderMeshProperties)target;

        if (GUILayout.Button("black"))
        {
            myScript.setColor("black");
        }
        if (GUILayout.Button("white"))
        {
            myScript.setColor("white");
        }
        if (GUILayout.Button("red"))
        {
            myScript.setColor("red");
        }
        if (GUILayout.Button("orange"))
        {
            myScript.setColor("orange");
        }
        if (GUILayout.Button("purple"))
        {
            myScript.setColor("purple");
        }
        if (GUILayout.Button("green"))
        {
            myScript.setColor("green");
        }
        if (GUILayout.Button("blue"))
        {
            myScript.setColor("blue");
        }
        if (GUILayout.Button("yellow"))
        {
            myScript.setColor("yellow");
        }
    }
}

