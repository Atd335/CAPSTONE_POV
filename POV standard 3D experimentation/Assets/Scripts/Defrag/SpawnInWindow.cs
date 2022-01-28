using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnInWindow : MonoBehaviour
{
    public GameObject window;
    public Transform parent;
    public float timeDelay = 0;
    

    public void spawnWindow()
    {
        StartCoroutine(spawnMeIn());
    }

    IEnumerator spawnMeIn()
    {
        yield return new WaitForSeconds(timeDelay);
        Instantiate(window, parent);

    }

}
