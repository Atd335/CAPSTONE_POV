using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PopUpInfo_GUI : MonoBehaviour
{
    public List<popUpBox> popUps;



    private void Awake()
    {
        UpdateController.POPUP = this;
    }

    void Start()
    {
        popUps = new List<popUpBox>();
        UpdateController.cc2D.deathEvent.AddListener(incrementDeath);
        StartCoroutine(remindPlayerOfRespawn());
    }

    public void spawnPopUp(string txt, Vector2 dim, Vector2 loc, float dur, int fon = 14)
    {
        popUpBox pb = new popUpBox(txt,dim,loc,dur*10,fon);
        popUps.Add(pb);
    }

    void Update()
    {
        for (int i = 0; i < popUps.Count; i++)
        {
            popUps[i].Duration -= Time.deltaTime * 10;
            if (popUps[i].Duration <= 0) { popUps.Remove(popUps[i]); }
        }
    }

    private void OnGUI()
    {
        var centeredStyle = GUI.skin.GetStyle("Box");
        centeredStyle.alignment = TextAnchor.MiddleCenter;
        centeredStyle.wordWrap = true;
        foreach (popUpBox p in popUps)
        {
            Rect r = new Rect(p.BoxLocation.x,p.BoxLocation.y,p.BoxDimensions.x,p.BoxDimensions.y);
            centeredStyle.fontSize = p.FontSize;
            GUI.color = new Color(GUI.color.r, GUI.color.g, GUI.color.b, p.Duration);
            centeredStyle.normal.textColor = new Color(1,1,1,p.Duration);
            GUI.Box(r,p.Content,centeredStyle);
        }
    }

    public class popUpBox
    {
        public string Content;
        public Vector2 BoxDimensions;
        public Vector2 BoxLocation;
        public float Duration;
        public int FontSize;

        public popUpBox(string content, Vector2 boxDimensions, Vector2 boxLocation, float duration, int fontSize)
        {
            this.Content = content;
            this.BoxDimensions = boxDimensions;
            this.BoxLocation = boxLocation;
            this.Duration = duration;
            this.FontSize = fontSize;
        }

    }

    int deathCounter = 0;

    void incrementDeath()
    {
        deathCounter++;    
    }

    IEnumerator remindPlayerOfRespawn()
    {
        while (true)
        {
            yield return new WaitUntil(() => (deathCounter>=8));
            deathCounter = 0;
            spawnPopUp("If you get stuck, you can press 'R' to respawn to the last checkpoint.", new Vector2(200,70), new Vector2((Screen.width/2)-100, (Screen.height - 90)), 2.5f);
        }

    }
}
