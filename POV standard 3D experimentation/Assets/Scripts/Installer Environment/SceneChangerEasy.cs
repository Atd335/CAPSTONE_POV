using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChangerEasy : MonoBehaviour
{

    public void changeSceneSimple(int i)
    {
        SceneManager.LoadScene(i);
    }

}
