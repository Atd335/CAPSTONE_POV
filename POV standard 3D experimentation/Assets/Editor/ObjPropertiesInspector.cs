using UnityEngine;
using System.Collections;
using UnityEditor;

[CustomEditor(typeof(objProperties))]
public class ObjPropertiesInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        objProperties myScript = (objProperties)target;

        if (GUILayout.Button("Make Solid"))
        {
            myScript.makePlat();
        }

        if (GUILayout.Button("Make Cut Out"))
        {
            myScript.makeCutOut();
        }

        if (GUILayout.Button("Make Damage"))
        {
            myScript.makeDamage();
        }
    }
}