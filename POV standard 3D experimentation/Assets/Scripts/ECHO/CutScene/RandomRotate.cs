using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RandomRotate : MonoBehaviour
{
    public Vector2 range;
    float randx;
    float randy;
    float randz;

    // Start is called before the first frame update
    void Start()
    {
        randx = Random.Range(range.x, range.y);
        randy = Random.Range(range.x, range.y);
        randz = Random.Range(range.x, range.y);
        transform.rotation = Quaternion.Euler(Random.Range(0,360f), Random.Range(0, 360f), Random.Range(0, 360f));
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(new Vector3(randx, randy, randz) * Time.deltaTime);
        foreach (SpriteRenderer sr in GetComponentsInChildren<SpriteRenderer>())
        {
            sr.transform.up = Vector3.up;
        }
    }
}
