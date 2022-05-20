using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

public class Boot_Fade : MonoBehaviour
{
    Image img;
    
    public Color colorStart;
    public Color colorEnd;

    public float fadeSpeed;

    public UnityEvent startEvent;
    public UnityEvent transparentEvent;
    public UnityEvent opaqueEvent;

    float startVol;

    void Start()
    {
        img = GetComponent<Image>();
        startEvent.Invoke();
        if (AS != null) { startVol = AS.volume; }
    }

    public void setToColorStart() {img.color = colorStart;}
    public void setToColorEnd() {img.color = colorEnd;}

    public void fadeOpaque() { StartCoroutine(fadeToOpaque()); }
    public void fadeTransparent() { StartCoroutine(fadeToTransparent()); }
    public AudioSource AS;
    IEnumerator fadeToOpaque()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime * fadeSpeed;
            timer = Mathf.Clamp(timer, 0, 1);

            img.color = Color.Lerp(colorEnd, colorStart, timer);
            if (AS != null) { AS.volume = (1*startVol) - (timer*startVol); }
            if (timer == 1) { opaqueEvent.Invoke(); break; }
            yield return new WaitForSeconds(Time.deltaTime);
            
        }
    }

    IEnumerator fadeToTransparent()
    {
        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime * fadeSpeed;
            timer = Mathf.Clamp(timer, 0, 1);

            img.color = Color.Lerp(colorStart, colorEnd, timer);

            if (timer == 1) { transparentEvent.Invoke(); break; }
            yield return new WaitForSeconds(Time.deltaTime);
        }
    }
}
