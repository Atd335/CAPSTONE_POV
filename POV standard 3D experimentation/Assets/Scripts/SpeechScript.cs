using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SpeechScript : MonoBehaviour
{
    [HideInInspector]
    public AudioSource AS;
    public AudioClip[] vocalSamples;
    public float timeBetweenLetters;
    bool speaking;

    public RectTransform speechBubbleRT;
    Text textBox;

    private void Awake()
    {
        UpdateController.speech = this;
    }

    public void _Start()
    {
        AS = GetComponents<AudioSource>()[2];
        textBox = speechBubbleRT.GetComponentInChildren<Text>();
    }

    void LateUpdate()
    {
        if (Input.GetKeyDown(KeyCode.E)) { SpeakText("This is some text that I make the 2D character say. This is an example sentence."); }
        
        speechBubbleRT.rotation = Quaternion.Euler(0,0,0);
        textBox.rectTransform.sizeDelta = speechBubbleRT.sizeDelta - Vector2.one * 10;
    }

    public void SpeakText(string str, float volume = .15f, float pitch = 1)
    {
        if (speaking) { return; }
        StartCoroutine(speaker(str, volume, pitch));
    }

    IEnumerator speaker(string str, float volume, float pitch)
    {
        speaking = true;
        textBox.text = "";
        int currentChar = 0;
        timeBetweenLetters = Mathf.Clamp(timeBetweenLetters,0.01f,999);
        while (true)
        {
            AS.pitch = pitch;
            AS.PlayOneShot(vocalSamples[intFromChar(str[currentChar])],volume);
            textBox.text += str[currentChar].ToString();
            currentChar++;
            if (currentChar >= str.Length) { break; }
            yield return new WaitForSeconds(timeBetweenLetters);
        }     
        speaking = false;
    }

    public int intFromChar(char c)
    {
        char cc = c.ToString().ToUpper()[0];
        switch (cc)
        {
            case 'A':
                return 0;
            case 'B':
                return 1;
            case 'C':
                return 2;
            case 'D':
                return 3;
            case 'E':
                return 4;
            case 'F':
                return 5;
            case 'G':
                return 6;
            case 'H':
                return 7;
            case 'I':
                return 8;
            case 'J':
                return 9;
            case 'K':
                return 10;
            case 'L':
                return 11;
            case 'M':
                return 12;
            case 'N':
                return 13;
            case 'O':
                return 14;
            case 'P':
                return 15;
            case 'Q':
                return 16;
            case 'R':
                return 17;
            case 'S':
                return 18;
            case 'T':
                return 19;
            case 'U':
                return 20;
            case 'V':
                return 21;
            case 'W':
                return 22;
            case 'X':
                return 23;
            case 'Y':
                return 24;
            case 'Z':
                return 25;
            default:
                return 0;
        }
    }
}
