using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;
using UnityEngine;

public class GlitchIconPM : MonoBehaviour
{
    int fr;
    public Sprite[] sprites;
    public Vector2Int range = new Vector2Int(1,10);

    private void FixedUpdate()
    {
        fr++;
        if (fr % Random.Range(range.x, range.y)==0)
        {
            GetComponent<ParticleSystemRenderer>().material.SetTexture("_MainTex", sprites[Random.Range(0, sprites.Length)].texture);
        }
    }

}
