using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Echo_Anim_OS_1 : MonoBehaviour
{
    Echo_Functions echo;
    void Start()
    {
        echo = GetComponent<Echo_Functions>();
        echo.setPosition(new Vector2(475, 270));
        StartCoroutine(anim());
        echo.enableBox(0);

        echo.setPosition(new Vector2(Screen.width-100, 60));
    }

    IEnumerator anim()
    {
        yield return new WaitForSeconds(1);
        echo.squish();
        echo.switchDirection();

        echo.boxSize = new Vector2(150, 75);
        echo.setText("That seemed to dust off some of the cobwebs!",2.5f);
        yield return new WaitUntil(() => !echo.talking);
        
        echo.boxSize = new Vector2(150, 75);
        echo.setText("It's starting to come back to me. Brent... <b>Asphodel.</b>",2.5f);
        yield return new WaitUntil(() => !echo.talking);

        echo.translateToPosition(echo.rt.anchoredPosition3D, echo.rt.anchoredPosition3D + new Vector3(-10,10,0), .2f);
        yield return new WaitUntil(() => !echo.translating);

        echo.boxSize = new Vector2(150, 75);
        echo.setText("Brent wouldn't have left me here. How long has it been since I've been booted up?",3.5f);
        yield return new WaitUntil(() => !echo.talking);

        echo.translateToPosition(echo.rt.anchoredPosition3D, echo.rt.anchoredPosition3D + new Vector3(-10, 10, 0), .2f);
        yield return new WaitUntil(() => !echo.translating);

        echo.squish();
        echo.switchDirection();
        yield return new WaitForSeconds(.3f);
        echo.squish();
        echo.switchDirection();
        yield return new WaitForSeconds(.3f);

        echo.boxSize = new Vector2(150, 75);
        echo.setText("There must be some clues around here somewhere.", 3.5f);
        yield return new WaitUntil(() => !echo.talking);

        yield return new WaitForSeconds(.3f);
        echo.translateToPosition(echo.rt.anchoredPosition3D, new Vector3(115,225), 2f);

        StartCoroutine(putTextOnTimer("Maybe there's something I missed in the <b>memories folder.</b>", 10, 999, new Vector2(150,75)));
        yield return new WaitUntil(() => !echo.translating);
        echo.squish();
        echo.switchDirection();

        while (true)
        {
            yield return new WaitUntil(() => GameObject.Find("Error Window(Clone)") != null);
            echo.boxSize = new Vector2(220, 80);
            echo.setText("This one seems a little too far gone. If I can find a partially corrupted file, I'll have the option to launch the repair tool.", 6f);
            yield return new WaitUntil(() => !echo.talking);
        }
    }

    IEnumerator putTextOnTimer(string text, float timer, float textTime, Vector2 boxsize)
    {
        yield return new WaitForSeconds(timer);
        echo.boxSize = boxsize;
        echo.setText(text, textTime);
    }
}
