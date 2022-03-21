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
        if (UpdateController.switcher.fpsMode || UpdateController.pause.menuOpen) { return; }
        // Read pixels from the source RenderTexture, apply the material, copy the updated results to the destination RenderTexture
        Graphics.Blit(src, dest, mat);
        GrabCameraTexture(src);
    }

    int maxPixel = 100;
    void GrabCameraTexture(RenderTexture src)
    {
        Texture2D tex = new Texture2D(src.width, src.height);
        tex.ReadPixels(new Rect(0, 0, src.width, src.height), 0, 0);

        ////rescale
        //Vector2Int resizedRez = new Vector2Int();
        //resizedRez.x = maxPixel;
        //resizedRez.y = (maxPixel * tex.height) / tex.width;

        //tex = TextureScaler.scaled(tex, resizedRez.x, resizedRez.y, FilterMode.Point);
        ////rescale

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

public class TextureScaler
{

    public static Texture2D scaled(Texture2D src, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0, 0, width, height);
        _gpu_scale(src, width, height, mode);

        //Get rendered data back to a new texture
        Texture2D result = new Texture2D(width, height, TextureFormat.ARGB32, true);
        result.Resize(width, height);
        result.ReadPixels(texR, 0, 0, true);
        return result;
    }

    public static void scale(Texture2D tex, int width, int height, FilterMode mode = FilterMode.Trilinear)
    {
        Rect texR = new Rect(0, 0, width, height);
        _gpu_scale(tex, width, height, mode);

        // Update new texture
        tex.Resize(width, height);
        tex.ReadPixels(texR, 0, 0, true);
        tex.Apply(true);    //Remove this if you hate us applying textures for you :)
    }

    // Internal unility that renders the source texture into the RTT - the scaling method itself.
    static void _gpu_scale(Texture2D src, int width, int height, FilterMode fmode)
    {
        //We need the source texture in VRAM because we render with it
        src.filterMode = fmode;
        src.Apply(true);

        //Using RTT for best quality and performance. Thanks, Unity 5
        RenderTexture rtt = new RenderTexture(width, height, 32);

        //Set the RTT in order to render to it
        Graphics.SetRenderTarget(rtt);

        //Setup 2D matrix in range 0..1, so nobody needs to care about sized
        GL.LoadPixelMatrix(0, 1, 1, 0);

        //Then clear & draw the texture to fill the entire RTT.
        GL.Clear(true, true, new Color(0, 0, 0, 0));
        Graphics.DrawTexture(new Rect(0, 0, 1, 1), src);
    }
}