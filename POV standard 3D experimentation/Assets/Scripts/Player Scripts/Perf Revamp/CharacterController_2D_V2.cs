using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using System.Linq;

public class CharacterController_2D_V2 : MonoBehaviour
{
    public int playerRadius;

    public Vector2 screenPos;

    public Image playerColliderImage;

    public ImageCapV2 imgcap;
    Texture2D tex;

    bool pseudoStarted;

    //collision
    public Vector2[] directionalVectorReference;
    int collisionResolution = 12;
    float collisionResolutionf = 12;
    public Color[] rimPixels;


    void Start()
    {
        playerColliderImage = GetComponent<Image>();
        playerRadius = Mathf.RoundToInt(playerColliderImage.rectTransform.sizeDelta.x)/2;

        directionalVectorReference = new Vector2[collisionResolution];
        for (int i = 0; i < collisionResolution; i++)
        {
            directionalVectorReference[i] = new Vector2(Mathf.Sin((i / collisionResolutionf) * (Mathf.PI * 2)), Mathf.Cos((i / collisionResolutionf) * (Mathf.PI * 2))).normalized;
        }


    }

    void LateUpdate()
    {
        //playerColliderImage.rectTransform.anchoredPosition = new Vector2(250 + (Mathf.Sin(Time.time) * 50), 180);
        //return;
        if (!imgcap.texture) { return; }

        tex = imgcap.texture;

        if (!pseudoStarted)
        {
            playerColliderImage.rectTransform.anchoredPosition = new Vector2(tex.width/2,tex.height/2);
            pseudoStarted = true;
        }

        MovePlayer();//move character

        screenPos = playerColliderImage.rectTransform.anchoredPosition;
        Vector3Int spi = roundVector(screenPos);


        CollectPixels(); //get collision data
        PerformCollisionActions(); //perform collision actions

    }

    [Header("==Movement Variables==")]
    public float moveSpd;
    
    Vector2 inputDirection;
    Vector2 moveDirection;
    void MovePlayer()
    {
        inputDirection = new Vector2(Input.GetAxisRaw("Horizontal"), Input.GetAxisRaw("Vertical"));

        moveDirection = inputDirection;

        playerColliderImage.rectTransform.anchoredPosition += moveDirection * moveSpd * Time.deltaTime;
    }

    void CollectPixels()
    {
        Color[] pixels = new Color[collisionResolution];
        for (int i = 0; i < collisionResolution; i++)
        {
            Vector3Int v = roundVector(screenPos) + roundVector((new Vector3(Mathf.Sin((i / collisionResolutionf) * (Mathf.PI * 2)), Mathf.Cos((i / collisionResolutionf) * (Mathf.PI * 2)), 0) * playerRadius));
            pixels[i] = tex.GetPixel(v.x,v.y);
        }

        rimPixels = pixels;
    }

    void PerformCollisionActions()
    {
        for (int i = 0; i < rimPixels.Length; i++)
        {
            checkAllColors(rimPixels[i],i);
        }
    }

    Vector3Int roundVector(Vector3 v)
    {
        Vector3Int vi = new Vector3Int();
        vi.x = Mathf.RoundToInt(v.x);
        vi.y = Mathf.RoundToInt(v.y);
        vi.z = Mathf.RoundToInt(v.y);
        return vi;
    }

    public void checkAllColors(Color col, int arrayID) //YOU NEED TO SET THIS TO THE APPROPRIATE COLORS
    {
        bool plat = CheckColorApproximate(col,Color.black);
        bool ouch = CheckColorApproximate(col, Color.red); 
        //bool cutOut = CheckColorApproximate(col, Color.white);

        if (plat) //perform collision readjustment
        {
            playerColliderImage.rectTransform.anchoredPosition -= directionalVectorReference[arrayID];
        }
        if (ouch) //kill
        {
            //print("touching red");
        }
    }

    public bool CheckColorApproximate(Color c1, Color c2)
    {
        return WithinRange(c1.r, c2.r) && WithinRange(c1.g, c2.g) && WithinRange(c1.b, c2.b);
    }

    public bool WithinRange(float f1, float f2, float thresh = .015f)
    {
        return Mathf.Abs(f1 - f2) <= thresh;
    }
}
