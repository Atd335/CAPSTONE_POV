using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo_Anim_OS_0 : MonoBehaviour
{
    Echo_Functions echo;
    void Start()
    {
        echo = GetComponent<Echo_Functions>();
        echo.setPosition(new Vector2(475, 270));
        StartCoroutine(anim());
        echo.enableBox(0);

        echo.setPosition(new Vector2(Screen.width-100, 100));
    }

    IEnumerator anim()
    {
        yield return new WaitForSeconds(1);

        echo.squish();
        echo.switchDirection();

        echo.setText("Hi! I’m Echo, and I’m here to help!",2.5f);
        yield return new WaitUntil(() => !echo.talking);

        yield return new WaitForSeconds(.25f);

        echo.boxSize = new Vector2(40, 30);
        echo.setText("...", 1f);
        yield return new WaitUntil(() => !echo.talking);

        yield return new WaitForSeconds(.25f);

        echo.boxSize = new Vector2(150, 55);
        echo.setText("Hello? Is anyone there?", 2.5f);
        yield return new WaitUntil(() => !echo.talking);

        echo.translateToPosition(echo.rt.anchoredPosition,
                                 echo.rt.anchoredPosition + new Vector2(-30,10),
                                 .2f);
        yield return new WaitUntil(() => !echo.translating);

        echo.setText("What happened here? It feels like...", 2.5f);
        yield return new WaitUntil(() => !echo.talking);

        echo.translateToPosition(echo.rt.anchoredPosition,
                                 echo.rt.anchoredPosition + 
                                 new Vector2(-30, -15),
                                 .2f);
        yield return new WaitUntil(() => !echo.translating);

        echo.setText("Like I’ve been asleep for a long time.", 2.5f);
        yield return new WaitUntil(() => !echo.talking);

        echo.setText("I’d better look around.", 2.5f);
        yield return new WaitUntil(() => !echo.talking);

        echo.translateToPosition(echo.rt.anchoredPosition,
                                 new Vector2(135 , Screen.height-150),
                                 1.25f);
        yield return new WaitUntil(() => !echo.translating);
        echo.squish();
        echo.switchDirection();

        yield return new WaitForSeconds(10);

        echo.setText("Most of the desktop is corrupted...", 2.5f);
        yield return new WaitUntil(() => !echo.talking);

        echo.boxSize.y = 70;
        echo.setText("But there’s gotta be something I can open.", 2.5f);
        yield return new WaitUntil(() => !echo.talking);

    }
}
