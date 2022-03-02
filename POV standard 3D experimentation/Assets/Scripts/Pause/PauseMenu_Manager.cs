using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PauseMenu_Manager : MonoBehaviour
{

    public KeyCode pauseKey = KeyCode.Tab;

    bool menuAnimating;
    public bool menuOpen;
    float animTimer;
    public float animSpd;

    public RectTransform taskBar;
    public RectTransform startMenu;
    public RectTransform buttonRT;

    public AnimationCurve taskBarShift;
    public AnimationCurve menuShift;
    public AnimationCurve buttonEnter;

    public Image PauseFade;

    private void Awake()
    {
        UpdateController.pause = this;
    }

    public void manualUpdate()
    {
        if (!menuAnimating && Input.GetKeyDown(pauseKey))
        {
            toggleMenu();
        }

        setInGameVariables();

    }

    public Slider volumeSlider;
    public Slider sensitivitySlider;
    public PauseMenuButton muteButton;
    public PauseMenuButton invertButton;
   

    void setInGameVariables()
    {
        UpdateController.sfx.AS.volume = volumeSlider.value / 100f;
        UpdateController.music.volume = volumeSlider.value / 100f;
        UpdateController.cc3D.mouseSensitivity = (sensitivitySlider.value / 100f) * 1260f;
        if (muteButton.toggledOn) { UpdateController.music.volume = 0; }
        UpdateController.cc3D.invertY = invertButton.toggledOn;

    }

    public void toggleMenu()
    {
        animSpd = Mathf.Clamp(animSpd,1,999);
        menuOpen = !menuOpen;
        if (menuOpen) { openMenu(); }
        else {closeMenu();}
        menuAnimating = true;
    }

    public void openMenu()
    {
        StartCoroutine(openAnim());
    }
    public void closeMenu()
    {
        foreach (PauseMenuButton pmb in GetComponentsInChildren<PauseMenuButton>())
        {
            if (pmb != this)
            {
                try
                {
                    pmb.disableWindow();
                }
                catch (System.Exception) { }
            }
        }
        StartCoroutine(closeAnim());
    }

    IEnumerator openAnim()
    {
        animTimer = 0;
        while (true)
        {
            animTimer += Time.deltaTime * animSpd;
            animTimer = Mathf.Clamp(animTimer, 0, 1);
            yield return new WaitForSeconds(Time.deltaTime);

            taskBar.anchoredPosition = new Vector2(0, -26 + (taskBarShift.Evaluate(animTimer) * 25));
            startMenu.anchoredPosition = new Vector2(-245+menuShift.Evaluate(animTimer)*244,24);
            //buttonRT.anchoredPosition = new Vector2(-235 + buttonEnter.Evaluate(animTimer)*235,0);
            PauseFade.color = new Color(0,0,0,animTimer/2);
            if (animTimer == 1) { menuAnimating = false; break; }
        }

        print("opened.");
    }

    IEnumerator closeAnim()
    {
        animTimer = 1;
        while (true)
        {
            animTimer -= Time.deltaTime * animSpd;
            animTimer = Mathf.Clamp(animTimer, 0, 1);
            yield return new WaitForSeconds(Time.deltaTime);

            taskBar.anchoredPosition = new Vector2(0, -26 + (taskBarShift.Evaluate(animTimer) * 25));
            startMenu.anchoredPosition = new Vector2(-245 + menuShift.Evaluate(animTimer) * 244, 24);
            PauseFade.color = new Color(0, 0, 0, animTimer / 2);
            if (animTimer == 0) { menuAnimating = false; break; }
        }
        print("closed.");
    }

}
