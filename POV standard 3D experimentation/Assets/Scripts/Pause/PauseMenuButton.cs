using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Events;

public class PauseMenuButton : MonoBehaviour
{

    public UnityEvent onClick;

    public void printTest(string s)
    {
        print(s);
    }

    private void Update()
    {
        if (this.gameObject == PauseMenuRayCaster.hoveredObject)
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1.025f, Time.deltaTime * 20);
            if (Input.GetKey(KeyCode.Mouse0)) { transform.localScale = Vector3.one; }
        }
        else
        {
            transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one * 1, Time.deltaTime * 20);
        }

        
    }

    public GameObject window;


    public void toggleWindow()
    {
        window.SetActive(!window.activeInHierarchy);
        foreach (PauseMenuButton pmb in transform.parent.GetComponentsInChildren<PauseMenuButton>())
        {
            if (pmb != this)
            {
                try
                {
                    pmb.disableWindow();
                }
                catch (System.Exception){}
            }
        }
    }
    public void enableWindow()
    {
        window.SetActive(true);
    }
    public void disableWindow()
    {
        window.SetActive(false);
    }

    [HideInInspector]
    public bool toggledOn = false;
    public void toggleCheck()
    {
        toggledOn = !toggledOn;
        GetComponentInChildren<Text>().enabled = toggledOn;
    }

    public void GoToDesktop(string level)
    {        
        try
        {
            SceneManager.LoadScene(level);
        }
        catch (System.Exception)
        {

        }
    }

}
