using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GlitchIconPM : MonoBehaviour
{
    int fr;
    public Sprite[] sprites;
    private void FixedUpdate()
    {
        fr++;
        if (fr % Random.Range(1, 10)==0)
        {
            GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", sprites[Random.Range(0, sprites.Length)].texture);
        }
    }

}
