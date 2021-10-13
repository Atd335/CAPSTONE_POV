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
    public RectTransform playerRect;

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

    public Color platformColor;
    public Color ouchColor;
    public Color cutOutColor;
    public Color objectColor;

    public Vector3 respawnPosition;

    private void Awake()
    {
        UpdateController.cc2D = this;
    }

    public void _Start()
    {
        imageCap = UpdateController.imageCap;
        player = GameObject.FindGameObjectWithTag("Player2D").transform;
        playerRect = GameObject.FindGameObjectWithTag("Player2D").GetComponent<RectTransform>();
        playerRadius = playerRect.sizeDelta.x / 2;
        UpdateController.switcher.assign3DPoint(roundVectorToInt(player.position));

        platformColor = ColorContainer.black;
        ouchColor = ColorContainer.red;
        cutOutColor = ColorContainer.white;
        objectColor = ColorContainer.yellow;
    }

    public void manualUpdate()
    {
        //UpdateController.qol.debugText.color = platformColor;
        //UpdateController.qol.debugPrint(platformColor.ToString());

        player.localScale = Vector3.Lerp(player.localScale,Vector3.one,Time.deltaTime * 6);

        updateColor();
        if (UpdateController.switcher.fpsMode) 
        {
            player.position = UpdateController.imageCap.VisualCamera.WorldToScreenPoint(UpdateController.switcher.hitPosition);
            UpdateController.switcher.spawnPosition = UpdateController.switcher.hitPosition;
            moveDirection = Vector3.zero;
            playerSpd = 0;
            interacting = false;
            return; 
        }
        player.localScale = Vector3.one;
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

        bool b = false;
        foreach (Color c in groundPixels)
        {
            if (CheckColorApproximate(c, platformColor))
            {
                b = true;
            }
        }
        return b;
        //return groundPixels.Contains<Color>(platformColor);
    }

    bool isGrounded()
    {

        Vector3Int v = roundVectorToInt(player.position);

        Vector3Int p1 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * imageCap.scaledPixelSize))), 0);
        Vector3Int p2 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2) + Mathf.RoundToInt(playerRadiusScaled) / 2, v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), 0);

        if (!withinBoundsOfTexture(p1, imageCap.texture) || !withinBoundsOfTexture(p2, imageCap.texture)) { return false; }

        Color[] groundPixels = imageCap.texture.GetPixels(v.x - (Mathf.RoundToInt(playerRadiusScaled) / 2), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled) / 2, 1);

        bool b = false;
        foreach (Color c in groundPixels)
        {
            if (CheckColorApproximate(c, platformColor))
            {
                b = true;
            }
        }
        return b;
        //return groundPixels.Contains<Color>(platformColor);
    }

    bool isRoofed()
    {

        Vector3Int v = roundVectorToInt(player.position);

        Vector3Int p1 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled)), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(8 * imageCap.scaledPixelSize))), 0);
        Vector3Int p2 = new Vector3Int(v.x - (Mathf.RoundToInt(playerRadiusScaled)) + Mathf.RoundToInt(playerRadiusScaled), v.y - (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), 0);

        if (!withinBoundsOfTexture(p1, imageCap.texture) || !withinBoundsOfTexture(p2, imageCap.texture)) { return false; }

        Color[] groundPixels = imageCap.texture.GetPixels(v.x - (Mathf.RoundToInt(playerRadiusScaled)), v.y + (Mathf.RoundToInt(playerRadiusScaled) + (Mathf.RoundToInt(3 * imageCap.scaledPixelSize))), Mathf.RoundToInt(playerRadiusScaled), 1);

        bool b = false;
        foreach (Color c in groundPixels)
        {
            if (CheckColorApproximate(c, platformColor))
            {
                b = true;
            }
        }
        return b;
        //return groundPixels.Contains<Color>(platformColor);
    }

    bool isOverlappingBlack(Vector3 centerPos, float radius)
    {
        bool touchedBlackPixel = false;
        collisionVectors = new List<Vector3>();

        for (int i = 0; i < 12; i++)// length of for loop = the amount of resolution of the collision. 
        {
            Vector3Int v = roundVectorToInt(centerPos) + roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled));
            if (withinBoundsOfTexture(v, imageCap.texture) && CheckColorApproximate(imageCap.texture.GetPixel(v.x, v.y), platformColor)) //check for ground
            {
                touchedBlackPixel = true;
                collisionVectors.Add(roundVectorToInt((new Vector3(Mathf.Sin((i / 12f) * (Mathf.PI * 2)), Mathf.Cos((i / 12f) * (Mathf.PI * 2)), 0) * playerRadiusScaled).normalized));
            }
            else if (withinBoundsOfTexture(v, imageCap.texture) && CheckColorApproximate(imageCap.texture.GetPixel(v.x, v.y), ouchColor)) //check for damage 
            {
                DIE();
            }
            else if (withinBoundsOfTexture(v, imageCap.texture) && CheckColorApproximate(imageCap.texture.GetPixel(v.x, v.y), objectColor)) //check for interactable object
            {
                overlappedInteractable(v);
            }
        }
        return touchedBlackPixel;
    }

    bool interacting;
    public InteractableObjectScript heldObj2D;
    void overlappedInteractable(Vector3 point)
    {
        if (interacting) { return; }
        //execute commands
        print("hm");
        Physics.Raycast(UpdateController.imageCap.VisualCamera.ScreenPointToRay(point),out RaycastHit rch);
        heldObj2D = rch.collider.gameObject.GetComponent<InteractableObjectScript>();
        if (heldObj2D)
        {
            heldObj2D.ToggleResizeItem();
        }
        //execute commands
        interacting = true;
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
        player.localScale = Vector3.zero;
        UpdateController.switcher.fpsMode = true;
        UpdateController.switcher.hitPosition = respawnPosition;
        if (UpdateController.cc2D.heldObj2D)
        {
            heldObj2D.resetMe();
            UpdateController.cc2D.heldObj2D.ToggleResizeItem("false");
            UpdateController.cc2D.heldObj2D = null;
        }
        moveDirection = Vector3.zero;
    }

    void resetAllInteractables()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("interact"))
        {
            g.GetComponent<InteractableObjectScript>().resetMe();
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
