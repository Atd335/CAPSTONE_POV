using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DrawWindow : MonoBehaviour
{
    public GameObject pentip;
    public GameObject penbridge;
    GameObject prevTip;

    public static Color penColor;
    public static float Size;

    void Start()
    {
        penColor = Color.black;
        Size = 5;
    }

    private void Update()
    {
        if (Input.GetKeyUp(KeyCode.Mouse0)) { prevTip = null; }
    }

    int fr;
    public int drawTickSpd;
    void FixedUpdate()
    {
        fr++;
        if (fr % drawTickSpd == 0)
        {
            if (Window_Canvas_Raycaster.hoveredElement == this.gameObject.gameObject && Input.GetKey(KeyCode.Mouse0))
            {
                GameObject pt = Instantiate(pentip);
                pt.transform.SetParent(transform);
                pt.transform.position = Input.mousePosition;
                pt.GetComponent<Image>().color = penColor;
                if (prevTip != null)
                {
                    GameObject pb = Instantiate(penbridge);
                    pb.transform.SetParent(transform);
                    pb.transform.position = Input.mousePosition;
                    pb.transform.up = prevTip.transform.position - pt.transform.position;
                    pb.GetComponent<RectTransform>().sizeDelta = new Vector2(5, Vector2.Distance(pt.transform.position, prevTip.transform.position));
                    pb.GetComponent<Image>().color = penColor;
                }

                prevTip = pt;
            }
        }
    }
}
