using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;
using UnityEngine.Events;

public class NarrativeTrigger : MonoBehaviour
{

    float range;

    bool enteredRange;

    bool played;
    bool repeat = false;

    public Narrative2DObject narrative_2D_input;

    void Start()
    {
        range = transform.localScale.x;
        played = false;



    }

    void Update()
    {
        if ((!played || narrative_2D_input.repeat) && (Vector3.Distance(UpdateController.cc3D.position,transform.position)<=range) && !enteredRange)
        {
            enteredRange = true;
            UpdateController.speech.SpeakText(narrative_2D_input.textAsset.text, narrative_2D_input.volume, narrative_2D_input.pitch);
            played = true;
        }
        if (Vector3.Distance(UpdateController.cc3D.position, transform.position) > range) { enteredRange = false; }
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = new Color(1,0,0,1f);
        Gizmos.DrawWireSphere(transform.position, transform.localScale.x);
    }

}
