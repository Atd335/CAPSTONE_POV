using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerEasy : MonoBehaviour
{

    public static void changeSceneSimple(int i,bool b)
    {
        SceneManager.LoadScene(i);
    }

    public void changeSceneSimple(int i)
    {
        SceneManager.LoadScene(i);
    }

}
