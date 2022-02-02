using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[ExecuteInEditMode]
public class CamEffect : MonoBehaviour
{
    public bool effectOn;
    public Material material;

    private void OnRenderImage(RenderTexture source, RenderTexture destination)
    {
        if (!effectOn)
        {
            Graphics.Blit(source,destination);
            return;
        }
        //material.SetFloat("_effectFloat", intensity);
        Graphics.Blit(source, destination, material);
    }
}
