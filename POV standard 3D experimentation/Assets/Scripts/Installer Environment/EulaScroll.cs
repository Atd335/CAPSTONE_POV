using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EulaScroll : MonoBehaviour
{
    public string eulaText;
    string[] eulaWords;

    public float scrollSpeed;
    float timer;

    Text textBox;
    int wordID;

    public TextAsset EULAasset;

    //-8100
    //-0145

    int siblingID;

    // Start is called before the first frame update
    void _Start()
    {
        spawnPoint = -145;
        spawnedCopy = false;
        eulaText = EULAasset.text;
        eulaWords = eulaText.Split('\n');
        textBox = GetComponent<Text>();

        textBox.text = eulaText;
        yy = -8100;
        GetComponent<RectTransform>().anchoredPosition = new Vector2(216.5f, yy);
    }

    bool pseudoStart;
    public float yy;

    bool spawnedCopy = false;

    float spawnPoint;

    // Update is called once per frame
    void Update()
    {
        if (!pseudoStart) 
        { 
            _Start(); 
            pseudoStart = true; 
        }

        yy += Time.deltaTime * scrollSpeed;
        yy = Mathf.Clamp(yy,-8100,spawnPoint);

        GetComponent<RectTransform>().anchoredPosition = new Vector2(216.5f, yy);

        if (yy==-145 && !spawnedCopy) 
        {
            print("hmm...");
            GameObject g = Instantiate(this.gameObject, transform.parent);
            g.GetComponent<EulaScroll>()._Start();
            g.transform.SetSiblingIndex(transform.GetSiblingIndex());

            spawnedCopy = true;
            spawnPoint = 170;
        }

        if (yy == 170) { Destroy(this.gameObject); }
    }
}
