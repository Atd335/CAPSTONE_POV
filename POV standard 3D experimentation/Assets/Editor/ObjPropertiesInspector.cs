using UnityEngine;
using System.Collections;
using UnityEditor;
using UnityEngine.UIElements;

[CustomEditor(typeof(objProperties)), CanEditMultipleObjects]

public class ObjPropertiesInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        objProperties myScript = (objProperties)target;

        myScript.spawnScript();

        if (GUILayout.Button("Set Custom Color"))
        {
            myScript.makeOverrideColor();
        }

        if (GUILayout.Button("Make Platform"))
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

        if (GUILayout.Button("Make Interactable"))
        {
            myScript.makeInteractable();
        }

        if (GUILayout.Button("Make Material Flat"))
        {
            myScript.makeFlat();
        }

        if (GUILayout.Button("Make Material White Shaded"))
        {
            myScript.makeWhiteShaded();
        }



        if (GUILayout.Button("Make Transparent"))
        {
            myScript.makeTransparent();
        }

        if (GUILayout.Button("Remove Component"))
        {
            myScript.removeThisComponent();
        }
    }
}