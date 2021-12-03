using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PressNextBumpAnim : MonoBehaviour
{
    public AnimationCurve bounce;
    float startY;

    float bounceTimer;
    public float spd;
    public float height;

    int squashTic;

    // Start is called before the first frame update
    void Start()
    {
        startY = transform.localPosition.y;
        squashTic = 1;
    }

    // Update is called once per frame
    void Update()
    {
        bounceTimer += Time.deltaTime * spd;

        transform.localPosition = new Vector3(transform.localPosition.x,startY + (height*bounce.Evaluate(bounceTimer)),transform.localPosition.z);
        
        if (bounceTimer >= squashTic+2)
        {
            transform.localScale = new Vector3(1.06f,.95f,1);
            squashTic+=2;
        }

        transform.localScale = Vector3.Lerp(transform.localScale, Vector3.one, Time.deltaTime * 8);
    }
}
