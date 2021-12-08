using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class IconScript : MonoBehaviour, IWindowButton
{
    int clickCount;
    bool higlighted;

    Image highlight;

    bool hovered;

    public GameObject windowToSpawn;

    // Start is called before the first frame update
    void Start()
    {
        highlight = GetComponentsInChildren<Image>()[1];
    }

    // Update is called once per frame
    void Update()
    {

        hovered = this.gameObject == DesktopAsset_Cursor.hoveredElement;
        higlighted = DesktopAsset_Cursor.selectedElement == this.gameObject;

        highlight.enabled = higlighted;
    }

    public void click()
    {
        clickCount++;
        if (DesktopAsset_Cursor.selectedElement == this.gameObject) { SpawnWindow(); }
        DesktopAsset_Cursor.selectedElement = this.gameObject;
    }

    public void SpawnWindow()
    {
        print("clicked icon");
        if (windowToSpawn && !GameObject.Find(windowToSpawn.name + "(Clone)"))
        {
            print($"Opened Window");    
            Instantiate(windowToSpawn);
        }
    }
}
