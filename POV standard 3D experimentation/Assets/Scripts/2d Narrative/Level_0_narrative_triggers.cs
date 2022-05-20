using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Level_0_narrative_triggers : MonoBehaviour
{

    DocumentationEnabler Docs;
    SpeechScript Speech;


    void Start()
    {
        StartCoroutine(narrativeTriggers());
        Docs = GameObject.FindObjectOfType<DocumentationEnabler>();
        Speech = UpdateController.speech;
    }

    public Narrative2DObject[] lines;

    IEnumerator narrativeTriggers()
    {
        yield return new WaitForSeconds(1);
        
        yield return new WaitUntil(() => !Docs.isDocUp && Docs.currentDoc == 0);

        int lineID = 0;
        
        Speech.SpeakText(lines[lineID].textAsset.text, lines[lineID].volume, lines[0].pitch);
        yield return new WaitUntil(() => !Speech.speaking);
        yield return new WaitForSeconds(.3f);

        lineID = 1;
        Speech.SpeakText(lines[lineID].textAsset.text, lines[lineID].volume, lines[0].pitch);
        yield return new WaitUntil(() => !Speech.speaking);
        yield return new WaitForSeconds(.3f);

        lineID = 2;
        Speech.SpeakText(lines[lineID].textAsset.text, lines[lineID].volume, lines[0].pitch);
        yield return new WaitUntil(() => !Speech.speaking);
        yield return new WaitForSeconds(.3f);

        lineID = 3;
        Speech.SpeakText(lines[lineID].textAsset.text, lines[lineID].volume, lines[0].pitch);
        yield return new WaitUntil(() => !Speech.speaking);
        yield return new WaitForSeconds(.3f);



    }

}
