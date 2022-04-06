using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DocumentationEnabler : MonoBehaviour
{

    public TutorialDocContainer docs;

    public Vector2 defaultWindowScale;
    public Vector2 defaultWindowPosition;
    
    public Image Window;
    public Image WindowImage;
    public Text descText;
    public Text panelText;

    bool isDocUp;
    bool animating;

    // Start is called before the first frame update
    void Start()
    {

        Window = GameObject.FindGameObjectWithTag("DocWindow").GetComponent<Image>();
        Window.rectTransform.sizeDelta = new Vector2(0,0);
        //Window.rectTransform.anchoredPosition = defaultWindowPosition;
        isDocUp = false;
        animating = false;
        Window.gameObject.SetActive(false);
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.E))
        {
            setWindowContent(1);
            ToggleWindow();
        }
    }

    public DocumentWindowContent defaultDWC;
    public void setWindowContent(int id = 0)
    {
        DocumentWindowContent content = docs.docs[id];
        WindowImage.sprite = content.img;
        WindowImage.GetComponent<DocAnimator>().enabled = content.imageAnimated;
        WindowImage.GetComponent<DocAnimator>().frames = content.animFrames;
        descText.text = content.text.text.Split('$')[0];
        panelText.text = content.text.text.Split('$')[1];
        
    }

    public void ToggleWindow()
    {
        if (animating) { return; }
        
        if (!isDocUp)
        {
            StartCoroutine(bringUpDoc(Window.rectTransform.sizeDelta, defaultWindowScale, defaultWindowPosition, true));
        }
        else
        {
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
        animating = false;
    }


}
