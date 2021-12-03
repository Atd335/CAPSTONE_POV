using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EulaScroll : MonoBehaviour
{
    public string eulaText;
    string[] eulaWords;

    public float timeBetweenWords;
    float timer;

    Text textBox;
    int wordID;


    // Start is called before the first frame update
    void Start()
    {
        eulaWords = eulaText.Split(' ');
        textBox = GetComponent<Text>();
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime;
        if (timer >= timeBetweenWords)
        {
            timer = 0;
            textBox.text += eulaWords[wordID];
            textBox.text += ' ';
            wordID++;
            if (wordID >= eulaWords.Length) { wordID = 0; textBox.text += '\n'; }
        }
    }
}
