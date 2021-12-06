using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class InstallerAnimation : MonoBehaviour
{
    FolderController[] folders;
    float timer;

    int timeID;
    float[] stepTimes = { .5f, .5f, .5f, .5f, .5f, .5f, .5f, .5f, .5f, .5f};


    int currentLoop;
    public int loops;

    public GameObject window;
    public Vector3 windowPos;


    void Update()
    {
        folders = GetComponentsInChildren<FolderController>();
        timer += Time.deltaTime;
        if (timeID >= 6) 
        {
            currentLoop++;
            if (currentLoop >= loops)
            {

            }
            else
            {
                timeID = 0;
            }
        }

        shakeController();
        window.transform.localPosition = Vector3.Lerp(window.transform.localPosition, windowPos, Time.deltaTime * 10);

        if (timeID >= stepTimes.Length) { return; }
        if (timer >= stepTimes[timeID])
        {
            step();
            timeID++;
            timer = 0;
        }
    }

    void step()
    {
        switch (timeID)
        {
            case 0:
                folders[0].open();
                folders[0].setPaperVisible(true);
                folders[0].ChangeSize(new Vector3(1,.2f,1));
                break;
            case 1:
                folders[0].ChangeLocalPaperPos(new Vector3(0,35,0));
                break;
            case 2:
                folders[0].close();
                folders[0].ChangeLocalPaperPos(new Vector3(300, 35, 0));
                folders[0].ChangeRot(new Vector3(0,0,15));
                break;
            case 3:
                folders[1].setPaperVisible(false);
                folders[1].open();
                break;
            case 4:
                folders[1].PaperPosSet(new Vector3(0, 35, 0));
                folders[0].setPaperVisible(false);
                folders[1].setPaperVisible(true);
                folders[0].resetPosition();
                folders[1].ChangeLocalPaperPos(new Vector3(0, -15, 0));
                break;
            case 5:
                folders[1].close();
                folders[1].setPaperVisible(false);
                break;
            case 6:
                folders[0].open();
                folders[0].setPaperVisible(true);
                folders[0].ChangeSize(new Vector3(1, .2f, 1));
                break;
            case 7:
                folders[0].ChangeLocalPaperPos(new Vector3(0, 35, 0));
                break;
            case 8:
                folders[0].close();
                folders[0].ChangeLocalPaperPos(new Vector3(150, 35, 0));
                folders[0].ChangeRot(new Vector3(0, 0, 15));
                break;
            case 9:
                //print("hee hee" + Time.deltaTime);
                if (GameObject.Find("EULATEXT")) { Destroy(GameObject.Find("EULATEXT")); }
                if (GameObject.Find("EULATEXT")) { Destroy(GameObject.Find("EULATEXT")); }
                tryWarn();
                break;
            default:
                break;
        }
    }


    bool shaking;
    void shakeWindow(float mag, float dur)
    {
        shakeDur = dur;
        shakeMag = mag;
        shaking = true;
    }

    float shakeTimer;
    float shakeMag;
    float shakeDur;

    void shakeController()
    {
        if (shaking)
        {
            shakeTimer += Time.deltaTime;
            window.transform.localPosition = windowPos + (new Vector3(Random.Range(-1, 1f), Random.Range(-1, 1f))*shakeMag);
            if (shakeTimer >= shakeDur) { shakeTimer = 0; shaking = false; }
        }
    }

    void tryWarn()
    {
        shakeWindow(5, .06f);
        if (GameObject.Find("WARNTEXT")) 
        {
            GameObject.Find("WARNTEXT").GetComponent<Text>().text = "<size=30>*Install Error*</size>\nPlease install components manually using WASD.";
        }
    }
}
