using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FadeOut : MonoBehaviour
{
    Image fadeOut;
    float timer;

    public int sceneToTransferTo = -1;
    public float fadeSpd = 4;
    void Awake()
    {
        timer = 0;
        transform.parent = GameObject.Find("FADE").transform;
        GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
    }

    // Update is called once per frame
    void Update()
    {
        timer += Time.deltaTime * fadeSpd;
        timer = Mathf.Clamp(timer,0,1);

        GetComponent<Image>().color = new Color(0,0,0,timer);

        if(timer==1)
        {
            if (sceneToTransferTo != -1)
            {
            SceneManager.LoadScene(sceneToTransferTo);
            }

        }
    }
}
