using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PasswordCheckerScript : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        
    }

    public void checkPassword(InputField input)
    {
        Text t = GameObject.Find("Incorrect").GetComponent<Text>();

        input.text = string.Empty;
        Color c = t.color;
        c.a = 1;
        t.color = c;
        print("HEEHAW");
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
