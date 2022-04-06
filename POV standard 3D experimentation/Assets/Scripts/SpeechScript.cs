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

        speechBubbleRT.gameObject.SetActive(false);
    }

    float hideTimer;
    float timeOut = 9999999999;
    void LateUpdate()
    {
        if (!speechBubbleRT.gameObject.activeInHierarchy) { return; }
        if (!speaking)
        {
            hideTimer += Time.deltaTime;
        }
        hideTimer = Mathf.Clamp(hideTimer,0,timeOut);
        if ((Input.GetKeyDown(KeyCode.Q)&&!speaking) || hideTimer==timeOut) { speechBubbleRT.gameObject.SetActive(false); hideTimer = 0;}
        
        speechBubbleRT.rotation = Quaternion.Euler(0,0,0);
    }

    public void SpeakText(string str, float volume = .15f, float pitch = 1)
    {
        if (speaking) { return; }
        StartCoroutine(speaker(str, volume, pitch));
    }

    IEnumerator speaker(string str, float volume, float pitch)
    {
        //enable bubble
        speechBubbleRT.gameObject.SetActive(true);

        speaking = true;
        textBox.text = "";
        
        //textbox size;
        //int rows = str.Split('\n').Length;
        //foreach (string s in str.Split('\n')) 
        //{
        //    rows += Mathf.FloorToInt(s.Length / 27f);
        //}

        //rows = (22 * rows) + 20;
        speechBubbleRT.sizeDelta = new Vector2(400,(22*1)+20);
        // textbox size
        
        int currentChar = 0;
        timeBetweenLetters = Mathf.Clamp(timeBetweenLetters,0.01f,999);

        float rowCount = 1;
        float charCount = 0;
        while (true)
        {
            float timeMod = 0;
            char chr = str[currentChar];
            charCount++;

            if (chr == '\n' || charCount==27) { rowCount++; charCount = 0; }
            speechBubbleRT.sizeDelta = new Vector2(400, (22 * rowCount) + 20);

            AS.pitch = pitch;
            AS.PlayOneShot(vocalSamples[intFromChar(chr)],volume);//play sound

            
            
            textBox.text += chr.ToString();//add character

            if (char.IsPunctuation(chr)) { timeMod = timeBetweenLetters; }//give pauses for punctuation.
            currentChar++;


            if (currentChar >= str.Length) { break; }           
            yield return new WaitForSeconds(timeBetweenLetters+timeMod);
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
                return 26;
        }
    }
}
