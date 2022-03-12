using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class FileRestored : MonoBehaviour
{
    Text txt;
    public int sceneIndex;
    public AudioClip click;
    // Start is called before the first frame update
    void Start()
    {
        txt = GetComponentInChildren<Text>();
        txt.text = "";
        StartCoroutine(routine());
    }

    IEnumerator routine()
    {

        for (int i = 0; i < "File Restored\n:)".Length; i++)
        {
            txt.text += "File Restored\n:)"[i];
            GetComponent<AudioSource>().pitch = Random.Range(.75f,1.2f);
            GetComponent<AudioSource>().PlayOneShot(click,.2f);
            yield return new WaitForSeconds(.1f);
        }

        float timer = 0;
        while (true)
        {
            timer += Time.deltaTime;
            timer = Mathf.Clamp(timer,0,1);
            yield return new WaitForSeconds(Time.deltaTime);

            txt.color = new Color(0,0,0,1-timer);

            if (timer == 1) { break; }
        }

        SceneManager.LoadScene(sceneIndex);

    }
}
