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

    //Simple Character
    public Transform player;
    float playerRadius = 25;
    float playerRadiusScaled;
    public Color[] playerColors;

    Vector3 moveDirection;

    //screen units for movement
    public float playerSpd = 20;
    float playerSpdScaled;

    private void Start()
    {

    }

    private void Update()
    {
        updateRelativeUnits();

        movePlayer();
    }

    void updateRelativeUnits()
    {
        playerRadiusScaled = (playerRadius / 1920f) * Screen.width;
        playerSpdScaled = (playerSpd/1920f) * Screen.width;
    }

    void movePlayer()
    {

        moveDirection = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);

        player.GetComponent<Image>().color = playerColors[0];
        if (isOverlappingBlack(player.position, playerRadius))
        {
            player.GetComponent<Image>().color = playerColors[1];
        }

        player.position += moveDirection * playerSpd * Time.deltaTime;
    }

    bool isOverlappingBlack(Vector3 centerPos, float radius)
    {
        bool touchedBlackPixel = false;

        for (int i = 0; i < 12; i++)// length of for loop = the amount of resolution of the collision. 
        {
            Vector3Int v = roundVectorToInt(centerPos) + roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled));
            if (texture.GetPixel(v.x, v.y) != Color.white) { touchedBlackPixel = true; }
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
