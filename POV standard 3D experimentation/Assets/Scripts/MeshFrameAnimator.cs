using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeshFrameAnimator : MonoBehaviour
{

    public Mesh[] meshFrames;
    MeshFilter mf;
    void Start()
    {
        mf = GetComponent<MeshFilter>();
    }
    int fr;
    public int spdMin;
    public int spdMax;
    int spd;
    public bool playSound;
    void FixedUpdate()
    {
        fr++;
        spd = Random.Range(spdMin,spdMax);
        if (fr%spd==0)
        {
            mf.mesh = meshFrames[Random.Range(0,meshFrames.Length)];
            if (SFX_Desktop.dsfx!=null && spd>2) { SFX_Desktop.dsfx.playSound(1,.1f,.7f,1.2f); }
        }
    }
}
