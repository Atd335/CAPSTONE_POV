using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Linq;
public class SplashScreenAnim : MonoBehaviour
{
    public Text loadBar;

    SplashScreenSceneTransfer timer;
    float animCurveEval;

    public AnimationCurve loadCurve;

    public int test;

    string povtitle;

    float tmLerp;
    float jargonTimer;

    bool expanding;

    float randomTic;

    string[] jargons = {"Extracting Metadata",
                        "Creating Temp SYS Directory",
                        "Restoring Last State Attepmt (1/3)",
                        "Emulating License" };
    string jargonText;

    int elipID;
    float ellipseTimer;
    string[] ellipses = {"",".","..","..." };

    // Start is called before the first frame update
    void Start()
    {
        timer = GetComponent<SplashScreenSceneTransfer>();
        tmLerp = 0;
        expanding = false;

        povtitle = loadBar.text;
        loadBar.text = "";
        loadBar.color = Color.black;
        lerpSpd = 5;
    }

    float lerpSpd;

    // Update is called once per frame
    void Update()
    {

        ellipseTimer += Time.deltaTime;
        if (ellipseTimer>=.1f)
        {
            ellipseTimer = 0;
            elipID++;
            if (elipID >= ellipses.Length) { elipID = 0; }
        }

        jargonTimer += Time.deltaTime;
        if (jargonTimer >= randomTic)
        {
            randomTic = Random.Range(.05f, .3f);
            jargonText = jargons[Random.Range(0, jargons.Length)];
            jargonTimer = 0;
        }

        if (timer.timer >= 0) { loadBar.color = Color.white; }

        animCurveEval = timer.timer / timer.timeToSceneTransfer;
        test = Mathf.RoundToInt(loadCurve.Evaluate(animCurveEval) * 40);
        loadBar.text = $"{povtitle}{ellipses[elipID]}\n\n{jargonText}...\n\n[ {string.Concat(Enumerable.Repeat("¤", test))}{string.Concat(Enumerable.Repeat(" ", 40-test))} ]";

        tmLerp += Time.deltaTime * 10;

        if (expanding)
        {
            if (tmLerp>=1) { SceneManager.LoadScene(timer.sceneToGoTo); return; }
            Vector2Int v = new Vector2Int();
            v.x = Mathf.RoundToInt(Mathf.Lerp(500, Screen.currentResolution.width, tmLerp));
            v.y = Mathf.RoundToInt(Mathf.Lerp(400, Screen.currentResolution.height, tmLerp));
            Screen.SetResolution(v.x, v.y, false);
        }
        else
        {
            if (tmLerp >= 1) { lerpSpd = 10; return; }
            Vector2Int v = new Vector2Int();
            v.x = Mathf.RoundToInt(Mathf.Lerp(700, 500, tmLerp));
            v.y = Mathf.RoundToInt(Mathf.Lerp(583, 400, tmLerp));
            Screen.SetResolution(v.x, v.y, false);
        }
    }

    public void switchToFull()
    {
        expanding = true;
        tmLerp = 0;
    }
}
