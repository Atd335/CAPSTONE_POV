using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Installer_SequenceController : MonoBehaviour
{
    public int sequenceID;
    public static Installer_SequenceController ISC;

    public GameObject gameWindow;

    public GameObject playerCanvas;

    void Start()
    {
        ISC = this;

        gameWindow = GameObject.Find("Transferral Manager");
        GameObject.Find("Transferral Manager").SetActive(false);
        playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas");
        sequenceID = 1;
    }

    private void Update()
    {
        if (sequenceID == 4)
        { 
            if(Input.GetKeyDown(KeyCode.V))
            {
                GameObject.Find("Animation Window").GetComponent<InstallerAnimation>().enabled = false;
                GameObject.Find("Folder 1").GetComponent<FolderController>().enabled = false;
                GameObject.Find("Paper.1").transform.position = UpdateController.cc2D.player.position;
                GameObject.Find("Paper.1").transform.parent = UpdateController.cc2D.player;
                GameObject.Find("Paper.1").transform.localScale = Vector3.one * 4.2f;
            }
        }
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
            case 3:
                step3();
                break;
            default:
                break;
        }

    }

    void step1()
    {
        GameObject.Find("Press Next Sprite").SetActive(false);
        
        gameWindow.SetActive(true);

        foreach (Image i in GameObject.Find("Eula Divider").GetComponentsInChildren<Image>())
        {
            i.enabled = true;
        }
        playerCanvas.SetActive(true);
        GameObject.Find("EulaBG").GetComponent<Image>().enabled = true;
        GameObject.Find("Cancel Button").GetComponent<Installer_Buttons>().disabled = true;
        GameObject.Find("Next Button").GetComponentInChildren<Text>().text = "Install";
        GameObject.Find("Progress Text").GetComponent<Text>().text = @"Installing to default programs folder C:\Program Files\POVvm...";
        GameObject.Find("DescText").GetComponent<Text>().text = @"Please press [Install] to begin installing the POVvm environment.";
        GameObject.Find("EULATEXT").GetComponent<EulaScroll>().enabled = true;
        GameObject.Find("Default Content BG").GetComponent<RawImage>().color = Color.gray;
        sequenceID++;
    }

    void step2()
    {
        GameObject.Find("Next Button").GetComponent<Installer_Buttons>().disabled = true;
        GameObject.Find("Bar Full").GetComponent<BarFillController>().fillUp = true;
        GameObject.Find("Animation Window").GetComponent<InstallerAnimation>().window.SetActive(true);
        GameObject.Find("Animation Window").GetComponent<InstallerAnimation>().enabled = true;
        sequenceID++;
    }


    void step3()
    {
        UpdateController.UC.windowSelected = true;
        sequenceID++;
    }
}
