using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Window_Corner_Buttons : MonoBehaviour, IWindowButton
{

    Image button;
    GameObject buttonGO;
    public Color hoverColor;
    public Color clickColor;
    public Color normalColor;

    Color currentColor;

    bool hovered;

    public int cornerButtonType;

    Window_Resizer windowResizer;
    public GameObject othertodestroy;

    public UnityEvent clickEvent;
    void Start()
    {
        button = GetComponent<Image>();
        buttonGO = this.gameObject;
        currentColor = Color.white;
        try
        {
            windowResizer = transform.parent.parent.parent.GetComponent<Window_Resizer>();
        }
        catch
        { 
        
        }
    }

    public void click()
    {
        button.color = clickColor;
        switch (cornerButtonType)
        {
            case 0:
                windowResizer.maximized = !windowResizer.maximized;
                if (windowResizer.maximized)
                {
                    windowResizer.maximizeStart = windowResizer.WindowBase.rectTransform.localPosition;
                    windowResizer.maximizeStartScale = windowResizer.WindowBase.rectTransform.sizeDelta;
                }
                else
                {
                    windowResizer.WindowBase.rectTransform.localPosition = windowResizer.maximizeStart;
                    windowResizer.WindowBase.rectTransform.sizeDelta = windowResizer.maximizeStartScale;
                }
                break;
            case 1:
                Destroy(windowResizer.gameObject);
                SFX_Desktop.dsfx.playSound(0, .1f, .8f, .8f);
                if (othertodestroy) { Destroy(othertodestroy); }
                break;
            default:
                break;
        }
        clickEvent.Invoke();
    }

    void Update()
    {
        hovered = buttonGO == Window_Canvas_Raycaster.hoveredElement;

        if (hovered)
        {
            currentColor = hoverColor;
        }
        else
        {
            currentColor = normalColor;
        }

        button.color = Color.Lerp(button.color, currentColor, Time.deltaTime * 10);
    }


    public Sprite[] toggleSprites;
    bool play;
    public void playAndPauseAudio()
    {
        play = !play;
        if (play)
        {
            GetComponentsInChildren<Image>()[1].sprite = toggleSprites[0];
            GetComponent<AudioSource>().Play();
        }
        else
        {
            GetComponentsInChildren<Image>()[1].sprite = toggleSprites[1];
            GetComponent<AudioSource>().Pause();
        }
    }

    public void setPenColor()
    {
        DrawWindow.penColor = normalColor;
    }

    public void clearPenCanvas()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("penTips"))
        {
            Destroy(g);
        }
    }

    public void a_spawnWindow(GameObject windowToSpawn)
    {
        if (windowToSpawn && !GameObject.Find(windowToSpawn.name + "(Clone)"))
        {
            Instantiate(windowToSpawn);
        }
    }

    public void emailButton(int id)
    {
        GameObject.FindGameObjectWithTag("emailText").GetComponent<Text>().text = emails[id];
    }

    string[] emails = {
    "<size=14>Subject: Ominous Warning!</size>\n\nTo: bWatters@cove.com\nFrom: jasher@cove.com\n\nThey're watching you, you have to be more careful. I don't blame you for doing what you're doing, but you know more than anyone that these people are not messing around. That bonsai file on your desktop is vulnerable, it won't be enough to cover your tracks. Tuck it away somewhere; the company is doing sweeps of our hard drives.\n\n- Jasher",
    "ha ha",
    "boo hoo"
    };
}
