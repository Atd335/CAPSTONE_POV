using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.Rendering;

public class ImageCap : MonoBehaviour
{

    //Debug Stuff
    bool startSim;
    int whileChecker;
    int collisionTimeOut = 100;

    //Cameras
    Camera CollisionCamera;
    Camera VisualCamera;

    // A Material with the Unity shader you want to process the image with
    public Material mat;
    public Texture2D texture;

    //Simple Character
    List<Vector3> collisionVectors;

    public Transform player;
    float playerRadius = 25;
    float playerRadiusScaled;
    Vector3 moveDirection;
    bool grounded;
    bool groundedForJump;
    bool roofed;
    float inputDirection;
    bool risingJump;
    float gravityMultiplier = 1;

    //screen units for movement
    public float jumpHeight;
    public float playerSpd;
    public float grav;
    public float acceleration;
    public float decelleration;
    public float maxSpd;

    float playerGravScaled;
    float scaledPixelSize;
    float scaledJumpHeight;
    float scaledAccel;
    float scaledDeccel;
    float scaledMaxSpd;
    

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
        if (Input.GetKeyDown(KeyCode.Space))
        {
            startSim = true;
        }
    }

    private void LateUpdate()
    {
        if (!startSim) { return; }
        if (!texture) { return; }
        updateRelativeUnits();
        movePlayer();
    }

    void updateRelativeUnits()
    {
        scaledPixelSize = Screen.width / 1920f;

        playerRadiusScaled = playerRadius * scaledPixelSize;
        playerGravScaled = grav*scaledPixelSize;
        scaledJumpHeight = jumpHeight * scaledPixelSize;
        scaledAccel = acceleration * scaledPixelSize;
        scaledDeccel = decelleration * scaledPixelSize;
        scaledMaxSpd = maxSpd * scaledPixelSize;
    }

    void movePlayer()
    {

        moveDirection = new Vector3(moveDirection.x, moveDirection.y, 0);
        
        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            playerSpd += scaledAccel * Time.deltaTime;
            inputDirection = Input.GetAxisRaw("Horizontal");
        }
        else
        {
            playerSpd -= scaledDeccel * Time.deltaTime;
        }

        playerSpd = Mathf.Clamp(playerSpd, 0, scaledMaxSpd);

        groundedForJump = isGroundedForjump();
        grounded = isGrounded();
        roofed = isRoofed();

        if (grounded)
        {
            gravityMultiplier = 1;
            moveDirection.y = -.05f;
            
        }
        else
        {
            moveDirection.y -= playerGravScaled * Time.deltaTime * gravityMultiplier;
            if (moveDirection.y > 0)
            {
                if (Input.GetButtonUp("Jump"))
                {
                    gravityMultiplier = 2.75f;
                }
                risingJump = true;
            }
            if (risingJump && moveDirection.y <= 0)
            {
                risingJump = false;
            }
        }

        if (Input.GetButtonDown("Jump") && groundedForJump) { moveDirection.y = scaledJumpHeight; };

        if (roofed && moveDirection.y > 0) { moveDirection.y = 0; }

        moveDirection.x = playerSpd * inputDirection;
        player.position += moveDirection  * Time.deltaTime;

        whileChecker = 0;
        while (isOverlappingBlack(player.position, playerRadius))
        {
            whileChecker++;
            foreach (Vector3 cv in collisionVectors)
            {
                player.position -= cv * scaledPixelSize;
            }
            if (whileChecker > collisionTimeOut) { Destroy(player.gameObject); }
        }
    }

    bool isGroundedForjump()
    {
        Vector3Int v = roundVectorToInt(player.position);
        Color[] groundPixels = texture.GetPixels(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled) / 2, 1);

        return groundPixels.Contains<Color>(Color.black);
    }
    bool isGrounded()
    {
        Vector3Int v = roundVectorToInt(player.position);
        Color[] groundPixels = texture.GetPixels(v.x-(Mathf.RoundToInt(playerRadiusScaled)/2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3*scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled)/2, 1);
        
        return groundPixels.Contains<Color>(Color.black);
    }
    bool isRoofed()
    {
        Vector3Int v = roundVectorToInt(player.position);
        Color[] groundPixels = texture.GetPixels(v.x - (Mathf.RoundToInt(playerRadiusScaled)), v.y + (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled), 1);

        return groundPixels.Contains<Color>(Color.black);
    }

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
