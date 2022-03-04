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
        fade.color = new Color(fade.color.r, fade.color.b, fade.color.g, 1);
    }

    void Update()
    {
        fade.color = Color.Lerp(fade.color, new Color(fade.color.r, fade.color.g, fade.color.b, -1f),Time.deltaTime*3);
        if (fade.color.a <= 0) { Destroy(this.gameObject); }
    }
}
