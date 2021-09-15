using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ImageCap : MonoBehaviour
{

    // A Material with the Unity shader you want to process the image with
    public Material mat;
    public Texture2D texture;
    public Texture2D textureSample;

    //Mouse Stuff
    public Vector2 onScreenPosition;
    public Vector2Int onScreenPositionInt;

    //Debugging stuff
    public RectTransform canvasCursor;
    public RawImage searchDebug;

    int debugBoxSize = 50;

    private void Start()
    {
        debugBoxSize = Mathf.RoundToInt((50 / 1920f) * Screen.width);
    }

    private void Update()
    {
        onScreenPosition = new Vector2(Input.mousePosition.x / Screen.width, Input.mousePosition.y / Screen.height);
        
        onScreenPosition.x *= 1920;
        onScreenPosition.y *= 1080;

        canvasCursor.localPosition = onScreenPosition - new Vector2(1920/2, 1080/2);

        onScreenPositionInt = new Vector2Int(Mathf.RoundToInt(onScreenPosition.x), Mathf.RoundToInt(onScreenPosition.y));

        readPixelsDebug();
    }

    void readPixelsDebug()
    {
        textureSample = new Texture2D(debugBoxSize, debugBoxSize);
        textureSample.SetPixels(texture.GetPixels(Mathf.RoundToInt(Input.mousePosition.x), Mathf.RoundToInt(Input.mousePosition.y), debugBoxSize, debugBoxSize));
        textureSample.filterMode = FilterMode.Point;
        textureSample.Apply();
        searchDebug.texture = textureSample;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Read pixels from the source RenderTexture, apply the material, copy the updated results to the destination RenderTexture
        Graphics.Blit(src, dest, mat);
        GrabCameraTexture(src);
        //print($"{src.width}, {src.height}");
    }

    void GrabCameraTexture(RenderTexture src)
    {
        Texture2D tex = new Texture2D(src.width, src.height);
        tex.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
        tex.Apply();
        texture = tex;
    }
}
