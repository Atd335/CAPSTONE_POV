using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class RunDefragTool : MonoBehaviour, IWindowButton
{

    bool hovered;
    Image buttonImage;

    public GameObject windowToSpawn;
    public GameObject thisWindow;
    public GameObject fade;
    void Start()
    {
        buttonImage = GetComponent<Image>();
    }

    void Update()
    {
        hovered = this.gameObject == Window_Canvas_Raycaster.hoveredElement;

        if (hovered)
        {
            buttonImage.color = Color.Lerp(buttonImage.color, Color.grey, Time.deltaTime * 5);
        }
        else
        {
            buttonImage.color = Color.Lerp(buttonImage.color, Color.white, Time.deltaTime * 5);
        }
    }

    public void click()
    {
        buttonImage.color = new Color(.6f, .6f, .6f, 1);
        if (windowToSpawn)
        {
            Instantiate(windowToSpawn);
        }
        GameObject g = Instantiate(fade);
        g.GetComponentInChildren<FadeOut>().sceneToTransferTo = 4;
        Destroy(thisWindow);
    }
}
