using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ImageCap : MonoBehaviour
{

    //Cameras
    Camera CollisionCamera;
    Camera VisualCamera;

    // A Material with the Unity shader you want to process the image with
    public Material mat;
    public Texture2D texture;

    //Simple Character
    public Transform player;
    float playerRadius = 25;
    float playerRadiusScaled;
    public Color[] playerColors;

    Vector3 moveDirection;

    //screen units for movement
    public float playerSpd = 20;
    float playerSpdScaled;
    float scaledPixelSize;

    private void Start()
    {
        CollisionCamera = GetComponentsInChildren<Camera>()[0];
        VisualCamera    = GetComponentsInChildren<Camera>()[1];
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Tab))
        {
            VisualCamera.enabled = !VisualCamera.enabled;
        }
    }

    private void LateUpdate()
    {
        if (!texture) { return; }
        updateRelativeUnits();
        movePlayer();
    }

    void updateRelativeUnits()
    {
        scaledPixelSize = Screen.width / 1920f;

        playerRadiusScaled = (playerRadius / 1920f) * Screen.width;
        playerSpdScaled = (playerSpd/1920f) * Screen.width;
    }

    void movePlayer()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        player.position += moveDirection * playerSpd * Time.deltaTime;

        while (isOverlappingBlack(player.position, playerRadius))
        {
            foreach (Vector3 cv in collisionVectors)
            {
                player.position -= cv * scaledPixelSize;
            }
        }
    }

    List<Vector3> collisionVectors;

    bool isOverlappingBlack(Vector3 centerPos, float radius)
    {
        bool touchedBlackPixel = false;
        collisionVectors = new List<Vector3>();

        for (int i = 0; i < 12; i++)// length of for loop = the amount of resolution of the collision. 
        {
            Vector3Int v = roundVectorToInt(centerPos) + roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled));
            if (texture.GetPixel(v.x, v.y) != Color.white) 
            { 
                touchedBlackPixel = true;
                collisionVectors.Add(roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled).normalized));
            }
        }

        return touchedBlackPixel;
    }

    void OnRenderImage(RenderTexture src, RenderTexture dest)
    {
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
}
