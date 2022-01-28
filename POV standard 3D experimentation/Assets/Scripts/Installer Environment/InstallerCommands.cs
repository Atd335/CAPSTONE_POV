using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class InstallerCommands : MonoBehaviour
{

    Installer_Buttons[] buttons;
    Text descText;
    BarFillController barController;
    RawImage installerImage;


    public GameObject folderAnimation;
    public GameObject guys;
    public GameObject gameWorld;
    public Texture2D installingBGImage;
    public Text errorEula;
    public Text normalEula;
    public Text installText;
    public Text installText2;

    public RawImage glitch;
    public Texture2D[] glitches;

    void Start()
    {
        nextCount = 0;
        cancelCount = 0;

        GameObject.Find("Dest Text").GetComponent<Text>().text = $"Destination Folder: {Application.persistentDataPath.ToString().Substring(0,24)}...";
        buttons = GameObject.Find("Buttons").GetComponentsInChildren<Installer_Buttons>();
        descText = GameObject.Find("DescText").GetComponent<Text>();
        barController = GameObject.Find("Loading Bar").GetComponentInChildren<BarFillController>();
        installerImage = GameObject.Find("Installer Image").GetComponentInChildren<RawImage>();
        glitch = GameObject.Find("GLITCH").GetComponentInChildren<RawImage>();
        glitch.enabled = false;

        gameWorld.SetActive(false);
        folderAnimation.SetActive(false);
        guys.SetActive(false);
        errorEula.enabled = false;
        installText.enabled = false;
        installText2.enabled = false;
    }

    public int nextCount;
    public void Next()
    {
        switch (nextCount)
        {
            case 0:
                buttons[1].disabled = true;
                descText.text = "Installing POV-vm... Please do not close the installer.";
                barController.fillUp = true;
                installerImage.texture = installingBGImage;
                folderAnimation.SetActive(true);
                guys.SetActive(true);
                installerImage.uvRect = new Rect(0,0,2.115f, 1.76f);
                installerImage.gameObject.AddComponent<Installer_BG_Scroll>();
                installerImage.gameObject.GetComponent<Installer_BG_Scroll>().scrollDir = Vector2.one*.1f;
                installText.enabled = true;
                installText2.enabled = true;
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:
                break;
        }
        nextCount++;
    }

    public int cancelCount;
    public void Cancel()
    {
        switch (cancelCount)
        {
            case 0:
                quitApplication();
                break;
            case 1:

                break;
            case 2:

                break;
            case 3:

                break;
            case 4:

                break;
            case 5:

                break;
            default:
                break;
        }
        cancelCount++;
    }

    bool stageEnter;
    public Transform stage;

    void Update()
    {
        if (stageEnter)
        {
            installerImage.color = Color.Lerp(installerImage.color, Color.clear, Time.deltaTime * 5);
            stage.localScale = Vector3.Lerp(stage.localScale, Vector3.one, Time.deltaTime * 5);

            foreach (Image i in installerImage.gameObject.GetComponentsInChildren<Image>())
            {
                i.color = Color.Lerp(installerImage.color, Color.clear, Time.deltaTime * 5);
                i.rectTransform.localScale = Vector3.Lerp(stage.localScale, Vector3.one, Time.deltaTime * 5);
            }
        }
    }

    public void EnableGame()
    {
        print("ding!");
        StartCoroutine(enableGame());

    }

    public void quitApplication()
    {
        Application.Quit();
    }

    IEnumerator enableGame()
    {
        buttons[0].disabled = true; //set 'next' button to disabled
                                    //installerImage.color = Color.clear; //remove installer Image

        installText.enabled = false;
        installText2.enabled = false;

        glitch.enabled = true;
        glitch.texture = glitches[0];
        yield return new WaitForSeconds(.03f);
        glitch.texture = glitches[1];
        yield return new WaitForSeconds(.03f);
        glitch.texture = glitches[2];
        yield return new WaitForSeconds(.03f);
        glitch.texture = glitches[1];
        yield return new WaitForSeconds(.03f);
        glitch.texture = glitches[2];
        yield return new WaitForSeconds(.03f);
        glitch.texture = glitches[0];
        glitch.transform.localScale = Vector3.one * 3f;
        glitch.texture = glitches[1];
        yield return new WaitForSeconds(.1f);
        glitch.texture = glitches[2];
        yield return new WaitForSeconds(.1f);
        glitch.texture = glitches[0];
        glitch.transform.localScale = Vector3.one;
        yield return new WaitForSeconds(.03f);

        GameObject.Find("Folder 1").GetComponent<FolderController>().enabled = false;
        GameObject.Find("Folder 2").GetComponent<FolderController>().enabled = false;
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("animpaper"))
        {
            Destroy(g);
        }
        

        glitch.enabled = false;


        stageEnter = true;
        gameWorld.SetActive(true); //activate player.

        //
        GameObject.Find("CamRaw").GetComponent<RectTransform>().sizeDelta = new Vector2(423, 352);
        GameObject.Find("CanvasRaw").GetComponent<RectTransform>().sizeDelta = new Vector2(423, 352);
        //

        errorEula.enabled = true;
        normalEula.enabled = false;

        yield return new WaitForSeconds(.1f);//reset Pos?
        
        UpdateController.cc3D.head.rotation = Quaternion.Euler(0, 0, 0);
        UpdateController.SUL.fpsCharacterEnabled = false;
        UpdateController.switcher.fpsMode = false;

        yield return null;
    }

    int currentLvl;
    public GameObject lava;
    public GameObject stairs;
    public void ResetPlayerInstaller()
    {
        currentLvl++;
        switch (currentLvl)
        {
            case 1:
                lava.SetActive(true);
                stairs.SetActive(false);
                UpdateController.cc2D.DIE();
                break;
            case 2:
                GameObject.Find("SceneChanger").GetComponent<SceneChangerEasy>().changeSceneSimple(1);
                break;
            default:
                break;
        }
    }

}
