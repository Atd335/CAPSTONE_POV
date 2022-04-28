using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Echo_Functions : MonoBehaviour
{
    public Image img;

    public RectTransform rt;

    public int animSpd;
    int currentFrame;
    int fr;
    

    Sprite[] currentAnim;
    public Sprite[] idleFrames;
    public Sprite[] walkFrames;

    public bool translating;
    public bool talking;

    public Text text;
    public Image bubbleBox;
    public Image bubbleTail;

    public AudioClip speakSound;
    public AudioSource AS;

    public Vector2 boxSize = new Vector2(150, 55);

    private void Awake()
    {
        img = GetComponent<Image>();
        rt = GetComponent<RectTransform>();
        switchAnimation(0);
    }

    public void setPosition(Vector2 pos)
    {
        rt.anchoredPosition = pos;
    }

    public void switchEmotion(int id)
    { 
    
    }

    public void switchDirection(int dir = 0)
    {
        if (dir == 0) { transform.Rotate(0, 180, 0); }
        else if (dir == 1) { transform.rotation = Quaternion.Euler(0, 0, 0); }
        else if (dir == -1) { transform.rotation = Quaternion.Euler(0, 180, 0); }
    }

    public void squish()
    {
        transform.localScale = new Vector3(1.2f,.6f,1);
    }

    public void stretch()
    {
        transform.localScale = new Vector3(.6f, 1.2f, 1);
    }

    public void translateToPosition(Vector3 startPoint, Vector3 endPoint, float time)
    {
        StartCoroutine(translateToPos(startPoint,endPoint,time));
    }

    IEnumerator translateToPos(Vector3 startPoint, Vector3 endPoint, float time)
    {
        float timer = 0;
        switchAnimation(1);
        translating = true;
        while (true)
        {
            timer += Time.deltaTime / time;
            timer = Mathf.Clamp(timer,0,1f);
            rt.anchoredPosition = Vector2.Lerp(startPoint,endPoint,timer);
            yield return new WaitForSeconds(Time.deltaTime);
            if (timer == 1) {break; }
        }
        switchAnimation(0);
        translating = false;
    }

    public void setText(string txt, float time)
    {
        text.text = txt;
        enableBox(1);
        bubbleBox.rectTransform.sizeDelta = new Vector2(boxSize.x,5);
        StartCoroutine(timeText(time));
    }

    public void enableBox(int enabled)
    {
        switch (enabled)
        {
            case 0:
                bubbleBox.color = Color.clear;
                bubbleTail.color = Color.clear;
                text.color = Color.clear;
                break;
            case 1:
                AS.PlayOneShot(speakSound);
                bubbleBox.color = Color.white;
                bubbleTail.color = Color.white;
                text.color = Color.black;
                break;
            default:
                break;
        }
    }


    //0 = idle
    //1 = walking
    public void switchAnimation(int id)
    {
        currentFrame = 0;
        switch (id)
        {
            case 0:
                animSpd = 4;
                currentAnim = idleFrames;
                break;
            case 1:
                animSpd = 2;
                currentAnim = walkFrames;
                break;
            default:
                break;
        }  
    }

    IEnumerator timeText(float time)
    {
        talking = true;
        yield return new WaitForSeconds(time);
        enableBox(0);
        talking = false;
    }

    
    void Update()
    {
        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 12f);
        bubbleBox.rectTransform.sizeDelta = Vector2.Lerp(bubbleBox.rectTransform.sizeDelta,
                                                         boxSize,
                                                         Time.deltaTime * 12f);
    }

    private void FixedUpdate()
    {
        animationController();
    }

    void animationController()
    {
        fr++;
        if (fr % animSpd == 0)
        {
            img.sprite = currentAnim[currentFrame];
            currentFrame++;
            if (currentFrame >= currentAnim.Length) { currentFrame = 0; }
        }
    }
}
