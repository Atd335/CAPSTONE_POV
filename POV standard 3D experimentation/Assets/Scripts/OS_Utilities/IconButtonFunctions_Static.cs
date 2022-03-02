using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Events;

public class IconButtonFunctions_Static : MonoBehaviour
{

    public string iconName;
    public Sprite iconTexture;

    public Image FileIcon;
    public Text FileText;
    public Image TextHighlight;

    public UnityEvent clickEvent;
    //public UnityEvent holdEvent;
    //public UnityEvent upEvent;

    public bool selected;
    public bool hovered;

    RectTransform rtransform;
    Vector3 cursorDiff;

    public void clickDown()
    {     
        singleHighlight();
        if (timer < doubleClickThreshold) { clickEvent.Invoke(); SFX_Desktop.dsfx.playSound(0,.1f); }
        timer = 0;
    }

    public void singleHighlight()
    {
        selected = true;
    }

    void Start()
    {
        rtransform = GetComponent<RectTransform>();
    }

    float timer = 0;
    public float doubleClickThreshold = 0.3f;
    void Update()
    {
        if (Window_Canvas_Raycaster.hoveredElement == this.gameObject && Input.GetKeyDown(KeyCode.Mouse0)) { clickDown(); }
        if (Window_Canvas_Raycaster.hoveredElement == null) { selected = false; }
        hovered = this.gameObject == Window_Canvas_Raycaster.hoveredElement;
        timer += Time.deltaTime;
        updateSelection();
    }

    void updateSelection()
    {
        if (!selected) { TextHighlight.color = Color.clear; }
        else { TextHighlight.color = new Color(0, 0, 0, .2f); }
        if (!hovered && Input.GetKey(KeyCode.Mouse0)) { selected = false; }
    }



    private void OnValidate()
    {
        FileText.text = iconName;
        this.gameObject.name = $"{iconName}_icon";
        FileIcon.sprite = iconTexture;
    }

    //FUNCTIONS TO REFERENCE
    public void a_printText(string prnt)//test function.
    {
        print(prnt);
    }

    public void a_deselect()
    {
        selected = false;
    }

    public void a_spawnWindow(GameObject windowToSpawn)
    {
        if (windowToSpawn && !GameObject.Find(windowToSpawn.name + "(Clone)"))
        {  
            Instantiate(windowToSpawn);
        }
    }

    public void a_spawnWindowMultiple(GameObject windowToSpawn)
    {
        if (windowToSpawn)
        {
            Instantiate(windowToSpawn);
        }
    }
}
