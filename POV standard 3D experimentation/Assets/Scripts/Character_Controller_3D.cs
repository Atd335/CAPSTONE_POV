using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller_3D : MonoBehaviour
{
    CharacterController cc;

    Camera headCamera;

    public Transform head;
    public Transform headMast;

    Vector3 moveDirection;
    Vector3 movementVector;

    public float acceleration;     //30
    public float decceleration;    //45
    public float jumpHeight;       //13
    public float gravity;          //45
    public float mouseSensitivity; //630
    public float playerZSpd; 
    public float playerXSpd;
    public float playerSpdMax;

    Vector2 rot;

    private void Awake()
    {
        UpdateController.cc3D = this;
    }

    // Start is called before the first frame update
    public void _Start()
    {
        cc = GetComponent<CharacterController>();
        headCamera = head.GetComponentInChildren<Camera>();
        rot.x = head.transform.rotation.eulerAngles.x;
        rot.y = head.transform.rotation.eulerAngles.y;
    }

    // Update is called once per frame
    public void manualUpdate()
    {
        if (!UpdateController.switcher.fpsMode) { return; }
        rot.x -= Input.GetAxisRaw("Mouse Y") * mouseSensitivity * Time.deltaTime;
        rot.y += Input.GetAxisRaw("Mouse X") * mouseSensitivity * Time.deltaTime;

        rot.x = Mathf.Clamp(rot.x,-90,90);

        if (Input.GetAxisRaw("Vertical") != 0)
        {
            moveDirection.z = Input.GetAxisRaw("Vertical");
            playerZSpd += acceleration * Time.deltaTime;
        }
        else
        {
            playerZSpd -= decceleration * Time.deltaTime;
        }

        if (Input.GetAxisRaw("Horizontal") != 0)
        {
            moveDirection.x = Input.GetAxisRaw("Horizontal");
            playerXSpd += acceleration * Time.deltaTime;
        }
        else
        {
            playerXSpd -= decceleration * Time.deltaTime;
        }

        if (cc.isGrounded)
        {
            moveDirection.y = -.1f;
        }
        else
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        if (Input.GetKey(KeyCode.Space) && cc.isGrounded)
        {
            moveDirection.y = jumpHeight;
        }

        playerZSpd = Mathf.Clamp(playerZSpd, 0, playerSpdMax);
        playerXSpd = Mathf.Clamp(playerXSpd, 0, playerSpdMax);

        movementVector = new Vector3(moveDirection.x * playerXSpd, moveDirection.y, moveDirection.z * playerZSpd);

        movementVector = movementVector.x * headMast.right + movementVector.y * headMast.up + movementVector.z * headMast.forward;

        cc.Move(movementVector * Time.deltaTime);

        head.rotation = Quaternion.Euler(rot);
        headMast.rotation = Quaternion.Euler(0, rot.y, 0);

    }

    public void DIE()
    {
        UpdateController.switcher.fpsMode = false;
    }
}
