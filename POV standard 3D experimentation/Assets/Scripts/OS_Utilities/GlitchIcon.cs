using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GlitchIcon : MonoBehaviour
{
    int fr;
    public Sprite[] sprites;
    private void FixedUpdate()
    {
        fr++;
        if (fr % Random.Range(1, 10)==0)
        {
            GetComponent<Image>().sprite = sprites[Random.Range(0,sprites.Length)]; 
        }
    }

}
