using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MoveCameraTransition : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }
    
    bool go;

    float yRot;
    public AnimationCurve AC;

    float timer;
    public float timeSpd;

    // Update is called once per frame
    void LateUpdate()
    {
        if (!go) { return; }
        
        timer += Time.deltaTime * timeSpd;
        yRot = AC.Evaluate(timer);
        yRot*=-90;
        transform.rotation = Quaternion.Euler(0,yRot,0);

        UpdateController.SUL.platformerCharacterEnabled = timer >= 1;

    }

    public void triggerMe()
    {
        go = true;
    }
}
