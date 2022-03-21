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
            foreach (objProperties obj in targets)
            {
                obj.makeOverrideColor();
            }
        }

        if (GUILayout.Button("Make Platform"))
        {
            foreach (objProperties obj in targets)
            {
                obj.makePlat();
            }
        }

        if (GUILayout.Button("Make Cut Out"))
        {
            foreach (objProperties obj in targets)
            {
                obj.makeCutOut();
            }
        }

        if (GUILayout.Button("Make Damage"))
        {
            foreach (objProperties obj in targets)
            {
                obj.makeDamage();
            }
        }

        if (GUILayout.Button("Make Interactable"))
        {
            foreach (objProperties obj in targets)
            {
                obj.makeInteractable();
            }
        }

        if (GUILayout.Button("Make Material Flat"))
        {
            foreach (objProperties obj in targets)
            {
                obj.makeFlat();
            }
        }

        if (GUILayout.Button("Make Material White Shaded"))
        {
            foreach (objProperties obj in targets)
            {
                obj.makeWhiteShaded();
            }
        }



        if (GUILayout.Button("Make Transparent"))
        {
            foreach (objProperties obj in targets)
            {
                obj.makeTransparent();
            }
        }

        if (GUILayout.Button("Remove Component"))
        {
            foreach (objProperties obj in targets)
            {
                obj.removeThisComponent();
            }
        }
    }
}