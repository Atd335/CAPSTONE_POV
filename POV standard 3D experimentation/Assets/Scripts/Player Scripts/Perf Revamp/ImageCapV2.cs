using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ImageCapV2 : MonoBehaviour
{
    public Camera CollisionCamera;

    public Material mat;
    public Texture2D texture;

    Canvas playerCanvas;

    bool canvasSet;

    public void Awake()
    {
        CollisionCamera = GetComponent<Camera>();
        playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>();
        canvasSet = false;
    }

    void Update()
    {
        if (texture)
        {
            //print($"{texture.width}, {texture.height}");

            playerCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(texture.width, texture.height);

        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Read pixels from the source RenderTexture, apply the material, copy the updated results to the destination RenderTexture
        Graphics.Blit(src, dest, mat);
        GrabCameraTexture(src);
    }

    public int maxPixel = 500;
    void GrabCameraTexture(RenderTexture src)
    {
        Texture2D tex = new Texture2D(src.width, src.height);

        tex.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);

        //rescale
        Vector2Int resizedRez = new Vector2Int();
        resizedRez.x = maxPixel;
        resizedRez.y = (maxPixel * tex.height) / tex.width;

        tex = TextureScaler.scaled(tex, resizedRez.x, resizedRez.y, FilterMode.Point);
        //rescale

        tex.Apply();

        texture = tex;
    }
}
