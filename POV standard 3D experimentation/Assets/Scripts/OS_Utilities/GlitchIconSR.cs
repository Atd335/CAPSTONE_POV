using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GlitchIconSR : MonoBehaviour
{
    int fr;
    public Sprite[] sprites;
    private void FixedUpdate()
    {
        fr++;
        if (fr % Random.Range(1, 10)==0)
        {
            GetComponent<SpriteRenderer>().sprite = sprites[Random.Range(0,sprites.Length)];
        }
    }

}
