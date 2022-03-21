using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Drawing;

public class VertexJitterScript : MonoBehaviour
{
    public Mesh meshReference;
    
    Vector3[] verts;
    int[] tris;

    public float jitterDegree = 12;

    public Color wireColor = Color.black;
    public float lineThickness = 1;
    public float skinWidth = .03f;

    public bool rotateMe = true;

    public float rotSpd = 90;

    public bool jitterGlitch;
    public Vector2 jitterRange;
    public float jitterinterval;
    public Vector2 jitterIntervalRange;
    float jitterTimer;

    void Start()
    {
        verts = meshReference.vertices;
        tris = meshReference.triangles;
    }

    public Camera cam;

    void Update()
    {
        var Drawg = DrawingManager.GetBuilder(true);
        Drawg.cameraTargets = new Camera[] { cam };
        using (Drawg.WithLineWidth(lineThickness))
        { 
            using (Drawg.WithColor(wireColor))
            {
                for (int i = 0; i < tris.Length; i++)
                {
                    try
                    {
                        var v1 = transform.rotation * ((verts[tris[i]]     + new Vector3(transform.position.x, transform.position.y, transform.position.z)) * (transform.lossyScale.x + skinWidth));
                        var v2 = transform.rotation * ((verts[tris[i + 1]] + new Vector3(transform.position.x, transform.position.y, transform.position.z)) * (transform.lossyScale.x + skinWidth));
                        v1 = VectorRound(v1);
                        v2 = VectorRound(v2);
                        Drawg.Line(v1, v2);
                    }
                    catch (System.Exception) { }
                }
            }
        }

        Drawg.Dispose();

        if (rotateMe)
        {
            transform.Rotate(0,rotSpd*Time.deltaTime,0);
        }

        if(jitterGlitch)
        {
            jitterTimer += Time.deltaTime;
            if (jitterTimer >= jitterinterval)
            {
                jitterDegree = Random.Range(jitterRange.x, jitterRange.y);
                jitterinterval = Random.Range(jitterIntervalRange.x,jitterIntervalRange.y);
                jitterTimer = 0;
            }
        }
    }

    Vector3 VectorRound(Vector3 v)
    {
        v.x = Mathf.Round(v.x * jitterDegree) / jitterDegree;
        v.y = Mathf.Round(v.y * jitterDegree) / jitterDegree;
        v.z = Mathf.Round(v.z * jitterDegree) / jitterDegree;

        return v;
    }
}
