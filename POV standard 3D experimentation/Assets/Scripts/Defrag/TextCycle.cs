using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TextCycle : MonoBehaviour
{

    public string[] words;
    int currentWord;

    public float rate;
    float timer;

    Text text;

    // Start is called before the first frame update
    void Start()
    {
        currentWord = 0;
        text = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        if (words.Length <= 0) { return; }
        timer += Time.deltaTime;
        timer = Mathf.Clamp(timer, 0, rate);
        if (timer >= rate)
        {
            timer = 0;
            currentWord++;
            if (currentWord == words.Length) { currentWord = 0; }
        }
        text.text = words[currentWord];
    }
}
