using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Installer_SequenceController : MonoBehaviour
{

    // installer opens, asks you to click next
    // install button appears, next button is disabled, space is split to allow for animation and EULA
    // press Install, bar moves slowly, animation begins to play
    // animation goes like this:
    //paper exits manilla folder, the camera follows the paper, passes our 2d character, then enters into another folder. do this x3 times or so, then stop.
    // animation stops

    public int sequenceID;
    public static Installer_SequenceController ISC;
    void Start()
    {
        ISC = this;
        sequenceID = 1;
    }


    public void next()
    {
        switch (sequenceID)
        {
            case 1:
                step1();
                break;
            case 2:
                step2();
                break;
            default:
                break;
        }

    }

    void step1()
    {
        GameObject.Find("Press Next Sprite").SetActive(false);
        
        foreach (Image i in GameObject.Find("Eula Divider").GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }
        GameObject.Find("EulaBG").GetComponent<Image>().enabled = true;
        GameObject.Find("Cancel Button").GetComponent<Installer_Buttons>().disabled = true;
        GameObject.Find("Next Button").GetComponentInChildren<Text>().text = "Install";
        GameObject.Find("Progress Text").GetComponent<Text>().text = @"Installing to default programs folder C:\Program Files\POVvm...";
        GameObject.Find("DescText").GetComponent<Text>().text = @"Please press [Install] to begin installing the POVvm environment.";

        sequenceID++;
    }

    void step2()
    {
        GameObject.Find("Next Button").GetComponent<Installer_Buttons>().disabled = true;
        GameObject.Find("Bar Full").GetComponent<BarFillController>().fillUp = true;

        sequenceID++;
    }

}
