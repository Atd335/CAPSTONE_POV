using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CamEffect : MonoBehaviour
{
    public bool effectOn;
    public Material material;

    public Texture2D platTexture; 
    public Texture2D ouchTexture;


    //animation variables
    [Header("Animation Variables")]
    public bool animateTextures;
    int fr;
    public int animSpd = 5;

    public Texture2D[] platTextureFrames;
    int platFrame = 0;

    public Texture2D[] ouchTextureFrames;
    int ouchFrame = 0;

    private void FixedUpdate()
    {
        if (!animateTextures || !effectOn) { return; }
        
        fr++;

        if (fr % animSpd==0)
        {
            platFrame++;
            ouchFrame++;
            if (ouchFrame >= ouchTextureFrames.Length) { ouchFrame = 0; }
            if (platFrame >= platTextureFrames.Length) { platFrame = 0; }
        }

        ouchTexture = ouchTextureFrames[ouchFrame];
        platTexture = platTextureFrames[platFrame];
    }

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!effectOn)
        {
            Graphics.Blit(source,destination);
            return;
        }

        material.SetTexture("_OverlayTexture_1", platTexture);
        material.SetTexture("_OverlayTexture_2", ouchTexture);

        Graphics.Blit(source, destination, material);
    }
}
