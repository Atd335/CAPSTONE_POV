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

    //Simple Character
    public Transform player;
    float playerRadius = 25;
    public float playerRadiusScaled;

    private void Start()
    {

    }

    private void Update()
    {
        playerRadiusScaled = (playerRadius / 1920f) * Screen.width;

        movePlayer();
    }

    void movePlayer()
    {
        if (isOverlappingBlack(player.position, playerRadius))
        {
            print("hey");
        }
    }
    bool isOverlappingBlack(Vector3 centerPos, float radius)
    {
        bool touchedBlackPixel = false;

        for (int i = 0; i < 12; i++)// length of for loop = the amount of resolution of the collision. 
        {
            Vector3Int v = roundVectorToInt(centerPos+ (new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled)); 
            touchedBlackPixel = texture.GetPixel(v.x,v.y) != Color.white;
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

    private void OnDrawGizmos()
    {
        //Vector3 v = Vector3.zero;
        //float rad = 20;
        //Gizmos.color = Color.red;
        //for (int i = 0; i < 12; i++)
        //{
        //    Gizmos.DrawWireSphere(v+roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * rad)),1);
        //}
    }

    Vector2 screenSpaceScaler(Vector2 v)
    {
        return Vector2.zero;
    }

    public Vector3Int roundVectorToInt(Vector3 v)
    {
        Vector3Int v2 = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        return v2;
    }
}
