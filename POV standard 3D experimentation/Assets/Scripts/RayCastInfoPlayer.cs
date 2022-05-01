using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class RayCastInfoPlayer : MonoBehaviour
{
    public UnityEvent rayEvent;
    public bool trigger;
    private void Start()
    {
        trigger = false;
    }

    void LateUpdate()
    {
        if (UpdateController.switcher.fpsMode) { return; }
        bool Ray = Physics.Raycast(UpdateController.imageCap.VisualCamera.ScreenPointToRay(UpdateController.cc2D.player.position - new Vector3(0,0,10)),out RaycastHit hit);
        //print(hit.collider.gameObject.name);
        if (hit.collider.tag == "INVOKE" && !trigger && hit.collider.gameObject==this.gameObject)
        {
            rayEvent.Invoke();
            trigger = true;
        }
    }
    public Transform nextFile;
    public Narrative2DObject narr;
    public void collectPage()
    {
        print("CollectedPage");
        UpdateController.speech.SpeakText(narr.textAsset.text,narr.volume, narr.pitch);

        try
        {
            nextFile = transform.parent.GetChild(transform.GetSiblingIndex() + 1);
            GoToCanvasFromWorld.g.AssignTransform(nextFile);
        }
        catch { }

        Destroy(this.gameObject);
    }


    public void SpeakText()
    {
        UpdateController.speech.SpeakText(narr.textAsset.text, narr.volume, narr.pitch);
    }
    public void DestroyMe()
    {
        Destroy(this.gameObject);
    }
    public void setnextFile()
    {
        try
        {
            nextFile = transform.parent.GetChild(transform.GetSiblingIndex() + 1);
            GoToCanvasFromWorld.g.AssignTransform(nextFile);
        }
        catch { }
    }


    public void finishLevel()
    {
        StartCoroutine(fadeScreen());
    }

    public GameObject fadePrefab;
    public int nextLvl;

    IEnumerator fadeScreen()
    {
        GameObject g = Instantiate(fadePrefab);
        Image img = g.GetComponentInChildren<Image>();
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime * 4;
            yield return new WaitForSeconds(Time.deltaTime);
            timer = Mathf.Clamp(timer,0,1);

            img.color = new Color(1,1,1,timer);
            if (timer == 1) { break; }
        }
        SceneChangerEasy.changeSceneSimple(nextLvl,true);
    }

    public static Vector3 RespawnCheckPoint;

    public void setPlayerCheckPoint()
    {
        //set check point 
        RespawnCheckPoint = transform.position;
    }

}
