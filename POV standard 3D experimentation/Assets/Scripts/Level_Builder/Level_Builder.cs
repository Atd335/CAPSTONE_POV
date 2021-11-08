using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_Builder : MonoBehaviour
{
    GameObject modelGO;

    public void spawnModel(string name = "Model")
    {
        if (!GameObject.FindGameObjectWithTag("ModelContainer")) { GameObject container = new GameObject("Model_Container"); container.tag = "ModelContainer"; container.transform.parent = transform; }

        modelGO = Resources.Load<GameObject>("Model");
        GameObject g = Instantiate(modelGO, GameObject.FindGameObjectWithTag("ModelContainer").transform);
        g.name = name;
    }
}
