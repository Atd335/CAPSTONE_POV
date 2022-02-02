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

        if (GUILayout.Button("Make Platform"))
        {
            myScript.makePlat();
        }

        //if (GUILayout.Button("Make Orange"))
        //{
        //    myScript.makePlatOrange();
        //}

        //if (GUILayout.Button("Make Blue"))
        //{
        //    myScript.makePlatBlue();
        //}

        //if (GUILayout.Button("Make Green"))
        //{
        //    myScript.makePlatGreen();
        //}

        //if (GUILayout.Button("Make Purple"))
        //{
        //    myScript.makePlatPurple();
        //}

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

        //if (GUILayout.Button("Make Door"))
        //{
        //    myScript.makeDoor();
        //}

        //if (GUILayout.Button("Make Button"))
        //{
        //    myScript.makeButton();
        //}

        if (GUILayout.Button("Make Material Flat"))
        {
            myScript.makeFlat();
        }

        if (GUILayout.Button("Make Material White Shaded"))
        {
            myScript.makeWhiteShaded();
        }

        if (GUILayout.Button("Remove Component"))
        {
            myScript.removeThisComponent();
        }
    }
}