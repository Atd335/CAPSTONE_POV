using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FadeIn : MonoBehaviour
{

    public Image fade;
    void Awake()
    {
        fade.enabled = true;
        fade.color = Color.black;
    }

    void Update()
    {
        fade.color = Color.Lerp(fade.color, new Color(0,0,0,-1f),Time.deltaTime*3);
        if (fade.color.a <= 0) { Destroy(this.gameObject); }
    }
}
