using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class ImageCap : MonoBehaviour
{
    public Camera CollisionCamera;
    public Camera VisualCamera;

    public Material mat;
    public Texture2D texture;

    public float scaledPixelSize;

    public void Awake()
    {
        UpdateController.imageCap = this;
    }

    public void _Start()
    {
        CollisionCamera = GetComponentsInChildren<Camera>()[0];
        VisualCamera    = GetComponentsInChildren<Camera>()[1];
    }

    public void manualUpdate()
    {
        CollisionCamera.enabled = !UpdateController.switcher.fpsMode;
        if (UpdateController.switcher.fpsMode) { return; }
        if (!texture) { return; }
        updateRelativeUnits();
    }

    void updateRelativeUnits()
    {
        scaledPixelSize = Screen.width / 1920f;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
        if (UpdateController.switcher.fpsMode) { return; }
        // Read pixels from the source RenderTexture, apply the material, copy the updated results to the destination RenderTexture
        Graphics.Blit(src, dest, mat);
        GrabCameraTexture(src);
    }

    void GrabCameraTexture(RenderTexture src)
    {
        Texture2D tex = new Texture2D(src.width, src.height);
        tex.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);
        tex.Apply();
        texture = tex;
    }

    public Vector3Int roundVectorToInt(Vector3 v)
    {
        Vector3Int v2 = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        return v2;
    }

    public bool withinBoundsOfTexture(Vector3Int v, Texture tex)
    {
        return v.x > 0 && v.x < tex.width && v.y > 0 && v.y < tex.height;
    }
}
