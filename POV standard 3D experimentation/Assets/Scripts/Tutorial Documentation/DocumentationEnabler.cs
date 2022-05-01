using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.Video;
using UnityEngine.UI;

public class DocumentationEnabler : MonoBehaviour
{

    public static DocumentationEnabler de;

    public TutorialDocContainer docs;

    public Vector2 defaultWindowScale;
    public Vector2 defaultWindowPosition;
    
    public Image Window;
    public Image WindowImage;
    public Text descText;
    public Text panelText;
    public Text docTitle;

    public RawImage videoPlayer;
    public VideoPlayer vp;

    public bool isDocUp;
    bool animating;

    public int itemToBringUp;

    public AudioSource AS;

    public RawImage bg;

    public UnityEvent startEvent;

    // Start is called before the first frame update
    void Start()
    {
        de = this;
        Window = GameObject.FindGameObjectWithTag("DocWindow").GetComponent<Image>();
        Window.rectTransform.sizeDelta = new Vector2(0,0);
        //Window.rectTransform.anchoredPosition = defaultWindowPosition;
        isDocUp = false;
        animating = false;
        Window.gameObject.SetActive(false);
        AS = GetComponent<AudioSource>();

        startEvent.Invoke();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Q) && isDocUp)
        {
            ToggleWindow();
        }

        if (isDocUp)
        {
            bg.color = Color.Lerp(bg.color, new Color(1,1,1,.5f), Time.deltaTime * 15);
        }
        else
        {
            bg.color = Color.Lerp(bg.color, new Color(1, 1, 1, 0), Time.deltaTime * 15);
        }
    }

    public DocumentWindowContent defaultDWC;
    public void setWindowContent(int id = 0)
    {
        DocumentWindowContent content = docs.docs[id];
        WindowImage.sprite = content.img;
        docTitle.text = content.text.text.Split('$')[0];
        descText.text = content.text.text.Split('$')[1];
        panelText.text = $"<color=grey>{content.text.text.Split('$')[2]}</color>";

        videoPlayer.gameObject.SetActive(content.isVideo);
        if (content.isVideo) { vp.clip = content.videoClip;}
    }

    public void ToggleWindow()
    {
        if (animating) { return; }
        
        UpdateController.switcher.fpsMode = true;

        if (!isDocUp)
        {
            AS.pitch = 1;
            AS.PlayOneShot(AS.clip);
            StartCoroutine(bringUpDoc(Window.rectTransform.sizeDelta, defaultWindowScale, defaultWindowPosition, true));
        }
        else
        {
            AS.pitch = .5f;
            AS.PlayOneShot(AS.clip);
            StartCoroutine(bringUpDoc(Window.rectTransform.sizeDelta, Vector2.zero, defaultWindowPosition, false));
        }
    }

    IEnumerator bringUpDoc(Vector2 startSize, Vector2 endSize, Vector3 pos, bool isUp)
    {
        if (isUp) { Window.gameObject.SetActive(true); }
        animating = true;
        float timer = 0;

        while (true)
        {
            timer += Time.deltaTime * 8;
            timer = Mathf.Clamp(timer,0,1);
            Window.rectTransform.sizeDelta = Vector2.Lerp(startSize,endSize,timer);

            yield return new WaitForSeconds(Time.deltaTime);
            if (timer == 1) { break; }
        }

        isDocUp = isUp;
        if (!isUp) { Window.gameObject.SetActive(false); }
        else { vp.Play(); }
        animating = false;
    }


}
