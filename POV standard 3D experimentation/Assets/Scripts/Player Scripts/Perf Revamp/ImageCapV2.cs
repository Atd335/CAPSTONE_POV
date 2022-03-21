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


    public int captureSize = 100;

    Canvas playerCanvas;
    public RawImage rawImage;
    public RectTransform captureBox;

    public void Awake()
    {
        CollisionCamera = GetComponent<Camera>();
        playerCanvas = GameObject.FindGameObjectWithTag("PlayerCanvas").GetComponent<Canvas>();
        setCanvasScale();
    }

    void setCanvasScale()
    {
        playerCanvas.GetComponent<CanvasScaler>().referenceResolution = new Vector2(Screen.width,Screen.height);
    }

    void Update()
    {
        if (texture)
        {
            rawImage.texture = texture;
        }
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        // Read pixels from the source RenderTexture, apply the material, copy the updated results to the destination RenderTexture
        Graphics.Blit(src, dest, mat);
        GrabCameraTexture(src);
    }

    void GrabCameraTexture(RenderTexture src)
    {
        //Texture2D tex = new Texture2D(src.width, src.height);
        Texture2D tex = new Texture2D(captureSize, captureSize);

        //tex.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
        tex.ReadPixels(new Rect(captureBox.anchoredPosition.x - (captureSize/2), captureBox.anchoredPosition.y - (captureSize / 2), captureSize, captureSize), 0, 0);

        

        tex.Apply();

        texture = tex;
    }
}
