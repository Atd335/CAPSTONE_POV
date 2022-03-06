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

    public void bringUpWindow()
    {
        GameObject.FindObjectOfType<TransferToMainGame>().bringUpWindow();    
    }

    public void emailButton(int id)
    {
        GameObject.FindGameObjectWithTag("emailText").GetComponent<Text>().text = emails[id];
    }

    string[] emails = {
    "<size=14>Subject: REACHING OUT</size>\n\nTo: bWatters@cove.com\nFrom: jasher@cove.com\n\nHi Brent!\n\nI tried to speak with you this afternoon, but you disappeared into the executive offices and I didn't see you again.\nI'll get right to it: The team and I are concerned about you. It must be difficult to be a single father with the amount of hours you're putting in on this project––please let me know if you need someone to take some tasks off your plate.\nIt's good to see you spending more of your time working from home. Do you have source control set up? There are some corrupted files <color=red>in the 'Memories' folder</color> that we could use your help with––it should be smoother sailing once they're sorted out.\nPlease be careful––remember that we're toying with forces we don't fully understand. No one wants to see you get hurt.\n\n- Judith",
    "<size=14>Subject: On the passing of Brent Waters</size>\n\nTo: ALLDEVS@cove.com, MGMT@cove.com\nFrom: jasher@cove.com\n\nTo The C.O.V.E. Family,\n\nIt is with heavy heart that we have learned of the passing of team member and esteemed colleague Brent Waters. After Brent left the office on Wednesday, he drove seven hours to a hotel in Spokane where he passed away peacefully in his sleep.\nBrent was an integral part of our community; his warm presence will be remembered in the coming weeks. Brent was a devoted father, a disciplined worker, and most of all a loyal member of our C.O.V.E. family.\nAs we honor Brent, we ask that employees remain focused on their responsibilities. Rest assured that C.O.V.E. is complying with authorities; we will remain in communication with all of you should more information come to light.\n\nEmployees will not be granted additional time off.",
    "Email not saved to memory, cannot connect to email server..."
    };
}
