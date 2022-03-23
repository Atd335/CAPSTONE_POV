using System.Collections;
using System.Collections.Generic;
using UnityEngine.Events;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using UnityEngine;

public class TransferToMainGame : MonoBehaviour
{
    public Image window;
    public Image window_black;
    public Image windowBG;
    public Image fallBackBG;

    public Image barFill;

    public AnimationCurve loadUp;

    public Text logs;

    private void Start()
    {
        window.gameObject.SetActive(false);
        window_black.color = new Color(1,1,1,0);
        window_black.gameObject.SetActive(false);
        windowBG.gameObject.SetActive(false);
        barFill.rectTransform.localScale = new Vector3(0,1,1);
    }

    public void bringUpWindow()
    {
        print("Bring Up Window");
        StartCoroutine(bringUpWindowCoroutine());
    }

    string[] logJargon = {
        "scanning disk for related meta-data...",
        "reformatting index...",
        "checking file location...",
        "attempting to extrapolate missing or corrupted data..."
    };

    IEnumerator bringUpWindowCoroutine()
    {
        float timer = 0;

        window.rectTransform.sizeDelta = new Vector2(64,64);
        window.gameObject.SetActive(true);

        while (true)
        {
            timer += Time.deltaTime * 10;
            timer = Mathf.Clamp(timer,0,1);
            yield return new WaitForSeconds(Time.deltaTime);
            window.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(64,64), new Vector2(Screen.width,Screen.height),timer);
            if (timer == 1) { break; }
        }

        timer = 0;
        window_black.gameObject.SetActive(true);
        windowBG.gameObject.SetActive(true);
        while (true)
        {
            timer += Time.deltaTime * 6;
            timer = Mathf.Clamp(timer, 0, 1);
            yield return new WaitForSeconds(Time.deltaTime);
            window_black.color = new Color(1,1,1,timer);
            if (timer == 1) { break; }
        }

        timer = 0;
        
        while (true)
        {
            timer += Time.deltaTime * 2;
            timer = Mathf.Clamp(timer, 0, 1);
            yield return new WaitForSeconds(Time.deltaTime);
            logs.text += $">{logJargon[Random.Range(0, logJargon.Length)]}\n";
            barFill.rectTransform.localScale = new Vector3(loadUp.Evaluate(timer),1,1);
            if (timer == 1) { break; }
        }

        timer = 0;

        while (true)
        {
            timer += Time.deltaTime * 9;
            timer = Mathf.Clamp(timer, 0, 1);
            yield return new WaitForSeconds(Time.deltaTime);
            fallBackBG.rectTransform.sizeDelta = Vector2.Lerp(new Vector2(645, 363), new Vector2(960,540), timer);
            fallBackBG.rectTransform.anchoredPosition = Vector2.Lerp(new Vector2(-5, 5), new Vector2(0,0), timer);

            if (timer == 1) { break; }
        }

        SceneManager.LoadScene(2);

        yield return null;
    }
}
