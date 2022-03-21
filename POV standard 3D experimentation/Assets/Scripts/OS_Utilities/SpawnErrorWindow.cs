using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnErrorWindow : MonoBehaviour
{
    public bool startImmediately;
    public float startTime;
    public float waitTime;
    public GameObject errorWindow;
    void Start()
    {
        if (startImmediately) { StartCoroutine(spawnError()); }
        else { StartCoroutine(waitToSpawnError()); }
    }
    IEnumerator spawnError()
    {
        yield return new WaitForSeconds(waitTime);
        IconButtonFunctions.spawnWindowStatic(errorWindow);
        SFX_Desktop.dsfx.playSound(2,.2f);
    }
    IEnumerator waitToSpawnError()
    {
        yield return new WaitForSeconds(startTime);
        StartCoroutine(spawnError());
        yield return null;
    }

}
