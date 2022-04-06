using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BootUp_Demo : MonoBehaviour
{
    public Text[] loadingTexts;
    public Image BG;

    public GameObject[] playerStuff;
    public Text instructions;
    public AnimationCurve instructionCurve;

    // Start is called before the first frame update
    void Start()
    {
        foreach (GameObject g in playerStuff)
        {
            g.SetActive(false);
        }

        StartCoroutine(ConsoleSpew());
        instructions.color = new Color(1,1,1,0);
        instructions.rectTransform.anchoredPosition = new Vector2((Screen.width / 2) - 85, -300);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    IEnumerator ConsoleSpew()
    {
        yield return new WaitForSeconds(.5f);
        for (int i = 0; i < 30; i++)
        {
            float f = Random.Range(0f, .25f);
            yield return new WaitForSeconds(f);
            GameObject.FindObjectOfType<ASCII_ANIMATOR>().additionalString += $"\n> Completing process ({i+1}/30) || completed in {f*1000} ms...\n";
        }
    }

    public void fadeOutText()
    {
        StartCoroutine(FadeOutTextCRTN());
    }

    IEnumerator FadeOutTextCRTN()
    {
        foreach (GameObject g in playerStuff)
        {
            g.SetActive(true);
        }

        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime * 5;
            timer = Mathf.Clamp(timer,0,1);

            foreach (Text t in loadingTexts)
            {
                t.color = new Color(1, 1, 1, 1 - timer);
            }
            BG.color = new Color(0, 0, 0, 1 - timer);
            if (timer == 1) { break; }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        timer = 0;
        while (true)
        {
            timer += Time.deltaTime * 4;
            timer = Mathf.Clamp(timer, 0, 1);

            instructions.color = new Color(1,1,1,timer);

            if (timer == 1) { break; }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        timer = 0;
        while (true)
        {
            timer += Time.deltaTime * 3;
            timer = Mathf.Clamp(timer, 0, 1);

            instructions.rectTransform.anchoredPosition = Vector2.Lerp(new Vector2((Screen.width/2) - 85,-300),new Vector2(20,-20), instructionCurve.Evaluate(timer));

            if (timer == 1) { break; }
            yield return new WaitForSeconds(Time.deltaTime);
        }

        for (int i = 0; i < "\n> use WASD to navigate.".Length; i++)
        {
            instructions.text += "\n> use WASD to navigate."[i].ToString();
            yield return new WaitForSeconds(.015f);
        }

        //timer = 0;
        //while (true)
        //{
        //    timer += Time.deltaTime * 5;
        //    timer = Mathf.Clamp(timer, 0, 1);

        //    if (timer == 1) { break; }
        //    yield return new WaitForSeconds(Time.deltaTime);
        //}
    }

}
