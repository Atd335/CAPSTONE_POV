using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo_Anim_OS_2 : MonoBehaviour
{
    Echo_Functions echo;
    void Start()
    {
        echo = GetComponent<Echo_Functions>();
        echo.setPosition(new Vector2(475, 270));
        StartCoroutine(anim());
        echo.enableBox(0);

        echo.setPosition(new Vector2(-50, 30));
    }

    IEnumerator anim()
    {
        yield return new WaitForSeconds(1);

        echo.translateToPosition(echo.rt.anchoredPosition, new Vector3(Screen.width/2,30), 3);
        yield return new WaitUntil(() => !echo.translating);

        yield return new WaitForSeconds(1);

        echo.boxSize = new Vector2(Screen.width-20, Screen.height - 100);
        echo.setText("No -- this can’t be it. This can’t be Asphodel.",3f);
        yield return new WaitUntil(() => !echo.talking);


        echo.setText("I understand now.", 2f);
        echo.boxSize = new Vector2(Screen.width - 20, Screen.height - 100);
        yield return new WaitUntil(() => !echo.talking);


        echo.setText("I'm sorry, El.", 2f);
        echo.boxSize = new Vector2(Screen.width - 20, Screen.height - 100);
        yield return new WaitUntil(() => !echo.talking);


        echo.setText("We never meant for it to end this way.", 3f);
        echo.boxSize = new Vector2(Screen.width - 20, Screen.height - 100);
        yield return new WaitUntil(() => !echo.talking);

        echo.setText("...", 10f);
        echo.boxSize = new Vector2(Screen.width - 20, Screen.height - 100);
        yield return new WaitForSeconds(2);
        GameObject.FindObjectOfType<Boot_Fade>().fadeOpaque();

    }

    IEnumerator putTextOnTimer(string text, float timer, float textTime, Vector2 boxsize)
    {
        yield return new WaitForSeconds(timer);
        echo.boxSize = boxsize;
        echo.setText(text, textTime);
    }
}
