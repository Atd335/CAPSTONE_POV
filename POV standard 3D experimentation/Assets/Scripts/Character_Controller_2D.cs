using System.Collections;
using System.Linq;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Character_Controller_2D : MonoBehaviour
{
    //Debugging 
    public bool startSim;
    int whileChecker;
    int collisionTimeOut = 100;

    //Simple Character
    public Transform player;

    List<Vector3> collisionVectors;
    Vector3 moveDirection;

    float playerRadius = 25;
    float playerRadiusScaled;
    float inputDirection;
    float gravityMultiplier = 1;

    bool grounded;
    bool groundedForJump;
    bool roofed;
    bool risingJump;

    //Movement Variables
    public float jumpHeight = 800;     //800
    public float playerSpd;            //0
    public float grav = 2400;          //2400
    public float acceleration = 1400;  //1400
    public float decelleration = 1200; //1200
    public float maxSpd = 400;         //400

    //Scaled Variables
    float playerGravScaled;
    float scaledJumpHeight;
    float scaledAccel;
    float scaledDeccel;
    float scaledMaxSpd;

    //image capture class
    ImageCap imageCap;

    public Image playerImage;

    private void Awake()
    {
        UpdateController.cc2D = this;
    }

    public void _Start()
    {
        imageCap = UpdateController.imageCap;
        player = GameObject.FindGameObjectWithTag("Player2D").transform;
        UpdateController.switcher.assign3DPoint(roundVectorToInt(player.position));
    }

    public void manualUpdate()
    {        
        updateColor();
        if (UpdateController.switcher.fpsMode) 
        {
            player.position = UpdateController.imageCap.VisualCamera.WorldToScreenPoint(UpdateController.switcher.hitPosition);
            UpdateController.switcher.spawnPosition = UpdateController.switcher.hitPosition;
            moveDirection = Vector3.zero;
            playerSpd = 0;
            return; 
        }
        if (!imageCap.texture) { return; }
        updateRelativeUnits();
        movePlayer();
        if (!withinBoundsOfTexture(roundVectorToInt(player.position), UpdateController.imageCap.texture)) { DIE(); }
    }

    void updateColor()
    {
        playerImage.color = Color.white;
        if (UpdateController.switcher.colliderBetween)
        {
            playerImage.color = Color.red;
        }
        if (!UpdateController.switcher.playerOnScreen)
        {
            playerImage.color = Color.clear;
        }
    }
    void updateRelativeUnits()
    {
        playerRadiusScaled = playerRadius * imageCap.scaledPixelSize;
        playerGravScaled = grav * imageCap.scaledPixelSize;
        scaledJumpHeight = jumpHeight * imageCap.scaledPixelSize;
        scaledAccel = acceleration * imageCap.scaledPixelSize;
        scaledDeccel = decelleration * imageCap.scaledPixelSize;
        scaledMaxSpd = maxSpd * imageCap.scaledPixelSize;
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
        player.position += moveDirection * Time.deltaTime;

        whileChecker = 0;
        while (isOverlappingBlack(player.position, playerRadius))
        {
            whileChecker++;
            foreach (Vector3 cv in collisionVectors)
            {
                player.position -= cv * imageCap.scaledPixelSize;
            }
            if (whileChecker > collisionTimeOut) { DIE(); break; }
        }

        UpdateController.switcher.assign3DPoint(roundVectorToInt(player.position));
    }

    bool isGroundedForjump()
    {

        Vector3Int v = roundVectorToInt(player.position);

        Vector3Int p1 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * imageCap.scaledPixelSize))), 0);
        Vector3Int p2 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2) + Mathf.RoundToInt(playerRadiusScaled) / 2, v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * imageCap.scaledPixelSize))), 0);

        if (!withinBoundsOfTexture(p1, imageCap.texture) || !withinBoundsOfTexture(p2, imageCap.texture)) { return false; }

        Color[] groundPixels = imageCap.texture.GetPixels(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * imageCap.scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled) / 2, 1);

        return groundPixels.Contains<Color>(Color.black);
    }

    bool isGrounded()
    {

        Vector3Int v = roundVectorToInt(player.position);

        Vector3Int p1 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * imageCap.scaledPixelSize))), 0);
        Vector3Int p2 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2) + Mathf.RoundToInt(playerRadiusScaled) / 2, v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), 0);

        if (!withinBoundsOfTexture(p1, imageCap.texture) || !withinBoundsOfTexture(p2, imageCap.texture)) { return false; }

        Color[] groundPixels = imageCap.texture.GetPixels(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled) / 2, 1);

        return groundPixels.Contains<Color>(Color.black);
    }

    bool isRoofed()
    {

        Vector3Int v = roundVectorToInt(player.position);

        Vector3Int p1 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled)), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * imageCap.scaledPixelSize))), 0);
        Vector3Int p2 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled)) + Mathf.RoundToInt(playerRadiusScaled), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), 0);

        if (!withinBoundsOfTexture(p1, imageCap.texture) || !withinBoundsOfTexture(p2, imageCap.texture)) { return false; }

        Color[] groundPixels = imageCap.texture.GetPixels(v.x - (Mathf.RoundToInt(playerRadiusScaled)), v.y + (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled), 1);

        return groundPixels.Contains<Color>(Color.black);
    }

    bool isOverlappingBlack(Vector3 centerPos, float radius)
    {
        bool touchedBlackPixel = false;
        collisionVectors = new List<Vector3>();

        for (int i = 0; i < 12; i++)// length of for loop = the amount of resolution of the collision. 
        {
            Vector3Int v = roundVectorToInt(centerPos) + roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled));
            if (withinBoundsOfTexture(v, imageCap.texture) && imageCap.texture.GetPixel(v.x, v.y) != Color.white)
            {
                if (imageCap.texture.GetPixel(v.x, v.y) == Color.red) { DIE(); }
                touchedBlackPixel = true;
                collisionVectors.Add(roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled).normalized));
            }
        }
        return touchedBlackPixel;
    }

    public Vector3Int roundVectorToInt(Vector3 v)
    {
        Vector3Int v2 = new Vector3Int(Mathf.RoundToInt(v.x), Mathf.RoundToInt(v.y), Mathf.RoundToInt(v.z));
        return v2;
    }

    public bool withinBoundsOfTexture(Vector3Int v, Texture2D tex)
    {
        return v.x > 0 && v.x < tex.width && v.y > 0 && v.y < tex.height;
    }

    public void DIE()
    {
        UpdateController.switcher.fpsMode = true;
        UpdateController.switcher.hitPosition = UpdateController.switcher.spawnPosition;
        moveDirection = Vector3.zero;
    }
}
