using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class AgaveLogoFade : MonoBehaviour
{
    public Vector2 endPos;

    public float spd;
    Image img;
    // Start is called before the first frame update
    void Start()
    {
        img = GetComponent<Image>();
        img.color = new Color(1,1,1,0);
    }

    // Update is called once per frame
    void Update()
    {
        img.color = Color.Lerp(img.color,Color.white, Time.deltaTime * spd);
        img.rectTransform.anchoredPosition = Vector2.Lerp(img.rectTransform.anchoredPosition, endPos, Time.deltaTime * spd);
    }
}
