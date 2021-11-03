using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(Level_Builder))]
public class Level_Builder_Inspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Level_Builder myScript = (Level_Builder)target;

        if (GUILayout.Button("Spawn Model"))
        {
            myScript.spawnModel("Model [white]");
        }

    }
}