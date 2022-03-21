using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Window_Resizer : MonoBehaviour
{
    public bool transformEnabled = true;
    public bool freeTransform = true;
    
    public Image WindowBase;
    public Image contentSection;
    public RawImage contentSectionRT;
    public Image topBar;
    public bool maximized;

    public AnimationCurve maximizeCurve;
    public Vector3 maximizeStart;
    public Vector3 maximizeStartScale;
    public float maximizeTimer;

    Window_Content_Manager WCM;

    float ratio;
    public Vector2 spawnpos;

    //growInSize
    public bool growToSize;
    public float growSpeed;
    float growTime;
    Vector2 smallSize;

    private void Awake()
    {
        transform.SetParent(GameObject.FindGameObjectWithTag("WindowCanvas").transform);
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        WindowBase.rectTransform.anchoredPosition = spawnpos;
    }

    void Start()
    {
        WCM = GetComponent<Window_Content_Manager>();
        ratio = WindowBase.rectTransform.sizeDelta.y/ WindowBase.rectTransform.sizeDelta.x;
        
        smallSize = WindowBase.rectTransform.sizeDelta;
        growTime = 1;
        if (growToSize) { smallSize.y = 65; growTime = 0; }
    }

    void LateUpdate()
    {

        growTime += Time.deltaTime * growSpeed;

        if(maximized)
        {
            maximizeTimer += Time.deltaTime * 3;
            maximizeTimer = Mathf.Clamp(maximizeTimer,0,1);
            WindowBase.rectTransform.localPosition = Vector3.Lerp(maximizeStart, Vector3.zero, maximizeCurve.Evaluate(maximizeTimer));
            WindowBase.rectTransform.sizeDelta = Vector3.Lerp(maximizeStartScale, new Vector3(Screen.width,Screen.height), maximizeCurve.Evaluate(maximizeTimer));
            
        }
        else
        {
            maximizeTimer = 0;
        }

        if (!freeTransform)
        {
            WindowBase.rectTransform.sizeDelta = Vector2.Lerp(smallSize, new Vector2(WindowBase.rectTransform.sizeDelta.x, WindowBase.rectTransform.sizeDelta.x * ratio), growTime); ;
        }

        ResizeElements();
    }

    void ResizeElements()
    {
        Vector2 scl = WindowBase.rectTransform.sizeDelta;
        topBar.rectTransform.sizeDelta = new Vector2(scl.x, 32);
        contentSection.rectTransform.sizeDelta = new Vector2(WindowBase.rectTransform.sizeDelta.x-16, WindowBase.rectTransform.sizeDelta.y-40);
        if (contentSectionRT)
        {
            contentSectionRT.rectTransform.sizeDelta = new Vector2(WindowBase.rectTransform.sizeDelta.x - 16, WindowBase.rectTransform.sizeDelta.y - 40);
        }
        //WCM.viewCam.pixelRect = new Rect(contentSection.rectTransform.position, contentSection.rectTransform.sizeDelta);
    }
}
