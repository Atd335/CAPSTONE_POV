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
    public bool loopPlat = true;
    public bool loopOuch = true;

    public bool restartOuch = false;
    public bool restartPlat = false;


    int fr;
    public int animSpd = 5;

    public Texture2D[] platTextureFrames;
    int platFrame = 0;

    public Texture2D[] ouchTextureFrames;
    int ouchFrame = 0;


    private void Start()
    {
        ouchTexture = ouchTextureFrames[0];
        platTexture = platTextureFrames[0];
    }

    private void FixedUpdate()
    {
        animateTextures = !UpdateController.switcher.fpsMode;
        if (!animateTextures || !effectOn) 
        {
            if (restartOuch) { ouchFrame = 0; ouchTexture = ouchTextureFrames[ouchFrame]; }
            if (restartPlat) { platFrame = 0; platTexture = platTextureFrames[platFrame]; }
            return; 
        }
        
        fr++;

        if (fr % animSpd==0)
        {
            platFrame++;
            ouchFrame++;
            if (loopOuch)
            {
                if (ouchFrame >= ouchTextureFrames.Length) { ouchFrame = 0; }
            }
            else
            {
                if (ouchFrame >= ouchTextureFrames.Length) { ouchFrame = ouchTextureFrames.Length-1; }
            }

            if (loopPlat)
            {
                if (platFrame >= platTextureFrames.Length) { platFrame = 0; }
            }
            else
            {
                if (platFrame >= platTextureFrames.Length) { platFrame = platTextureFrames.Length - 1; }
            }
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
