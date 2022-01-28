using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BGAnim : MonoBehaviour
{
    public int xWidth = 26;
    public int yWidth = 21;
    int yCount;
    string fragSpaceString = "[ ]";
    string[] cellTypes = { "[*]", "[|]" , "[-]" , "[?]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "[ ]", "   ", "   ", "   ", "   ", "   ", "   ", "   " };

    //startup
    public bool inStartUp;
    public float startRate = .1f;
    float startTimer;

    //bganim
    float resetRate;
    public Vector2 resetRange;
    float resetTimer;
    Text text;

    // Start is called before the first frame update
    void Start()
    {
        text = GetComponent<Text>();
        if (!inStartUp)
        {
            for (int y = 0; y < yWidth; y++)
            {
                for (int x = 0; x < xWidth; x++)
                {
                    text.text += fragSpaceString;
                }
                text.text += '\n';
            }
        }

        resetRate = 0;
    }

    // Update is called once per frame
    void Update()
    {
        if (inStartUp)
        {
            startTimer += Time.deltaTime;
            startTimer = Mathf.Clamp(startTimer, 0, startRate);

            if (startTimer == startRate)
            {
                startTimer = 0;

                for (int i = 0; i < xWidth; i++){ text.text += fragSpaceString; }
                text.text += '\n';
                yCount++;
                if (yCount == yWidth) { inStartUp = false; }
            }
        }
        else
        {
            resetTimer += Time.deltaTime;
            resetTimer = Mathf.Clamp(resetTimer, 0, resetRate);
            if (resetTimer == resetRate)
            {
                resetTimer = 0;
                resetRate = Random.Range(resetRange.x, resetRange.y);

                text.text = string.Empty;

                for (int y = 0; y < yWidth; y++)
                {
                    for (int x = 0; x < xWidth; x++)
                    {
                        text.text += $"<color=#{111111*(Random.Range(1,5))}>{cellTypes[Random.Range(0, cellTypes.Length)]}</color>";
                    }
                    text.text += '\n';
                }
            }
        }
    }
}
