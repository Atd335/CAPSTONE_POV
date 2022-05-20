using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class ScreenScrollLoadIn : MonoBehaviour
{
    int amt;
    public float spd;
    public int rows;
    // Start is called before the first frame update
    void Start()
    {
        amt = Screen.height;
        amt /= rows;
        StartCoroutine(LOADIN());
    }
    IEnumerator LOADIN()
    {
        for (int i = 0; i < rows; i++)
        {
            GetComponent<Image>().rectTransform.offsetMax = new Vector2(GetComponent<Image>().rectTransform.offsetMax.x, GetComponent<Image>().rectTransform.offsetMax.y-amt);
            yield return new WaitForSeconds(spd);
        }
    }
}
