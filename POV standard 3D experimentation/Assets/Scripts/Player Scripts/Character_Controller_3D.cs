using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Character_Controller_3D : MonoBehaviour
{
    CharacterController cc;

    Camera headCamera;

    public Transform head;
    public Transform headMast;
    public Transform headCamTransform;

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
    public float playerSpdMaxSprint;
    [HideInInspector]
    public bool invertY;
    
    Vector2 rot;

    public Vector3 position;
    Vector3 spawnPosition;
    Vector2 spawnRot;

    public float scrollSpd;
    public MeshRenderer bgQuad;
    Material bgMat;

    public float startDelay = 0;
    [HideInInspector]
    public float startDelayTimer = 0;

    private void Awake()
    {
        UpdateController.cc3D = this;
    }
    Quaternion lerpRot;
    // Start is called before the first frame update
    public void _Start()
    {
        cc = GetComponent<CharacterController>();
        headCamera = head.GetComponentInChildren<Camera>();
        headCamTransform = headCamera.transform;
        rot.x = head.transform.rotation.eulerAngles.x;
        rot.y = head.transform.rotation.eulerAngles.y;
        spawnPosition = transform.position;
        spawnRot = rot;
        //bgMat = bgQuad.material;
    }
    
    // Update is called once per frame
    public void manualUpdate()
    {
        if (startDelayTimer < startDelay) { return; }
        bool menuAnimating = (UpdateController.pause != null && UpdateController.pause.menuAnimating);
        if (!UpdateController.switcher.fpsMode || !UpdateController.SUL.fpsCharacterEnabled || !UpdateController.UC.windowSelected || menuAnimating) { return; }

        if (!invertY)
        {
            rot.x -= Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }
        else
        {
            rot.x += Input.GetAxis("Mouse Y") * mouseSensitivity * Time.deltaTime;
        }
        
        rot.y += Input.GetAxis("Mouse X") * mouseSensitivity * Time.deltaTime;

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

        if (!Input.GetKey(KeyCode.LeftShift))//not sprinting
        {
            playerZSpd = Mathf.Clamp(playerZSpd, 0, playerSpdMax);
            playerXSpd = Mathf.Clamp(playerXSpd, 0, playerSpdMax);
        }
        else
        {
            playerZSpd = Mathf.Clamp(playerZSpd, 0, playerSpdMaxSprint);
            playerXSpd = Mathf.Clamp(playerXSpd, 0, playerSpdMaxSprint);
        }

        movementVector = new Vector3(moveDirection.x * playerXSpd, moveDirection.y, moveDirection.z * playerZSpd);

        movementVector = movementVector.x * headMast.right + movementVector.y * headMast.up + movementVector.z * headMast.forward;

        cc.Move(movementVector * Time.deltaTime);

        lerpRot = Quaternion.Lerp(lerpRot, Quaternion.Euler(rot), Time.deltaTime * 25f);
        head.rotation = lerpRot;
        headMast.rotation = Quaternion.Euler(0, lerpRot.eulerAngles.y, 0);

        position = head.position;

        bobHead();

        checkFor2DCheckPoint();
    }
    float walkTimer;
    float stopTimer;
    void bobHead()
    {
        bool moving = new Vector2(playerXSpd,playerZSpd).magnitude>0;
        
        if (moving)
        {
            walkTimer += Time.deltaTime * new Vector2(playerXSpd, playerZSpd).magnitude * 2.75f;
            headCamTransform.localPosition = new Vector3(0, Mathf.Sin(walkTimer) * .06f, 0);
        }
        else
        {
            
            headCamTransform.localPosition = Vector3.Lerp(headCamTransform.localPosition, Vector3.zero,Time.deltaTime * 8f);
            walkTimer = 0;
        }
    }

    public void DIE()
    {
        rot = spawnRot;

        head.rotation = Quaternion.Euler(rot);
        headMast.rotation = Quaternion.Euler(0, rot.y, 0);

        cc.enabled = false;
        transform.position = spawnPosition;
        cc.enabled = true;
        //resetAllInteractables();
    }

    void checkFor2DCheckPoint()
    {
        bool b = Physics.Raycast(head.position,head.forward, out RaycastHit rch);

        if (Input.GetKey(KeyCode.Mouse1) && b && rch.collider.tag == "checkPoint2D")
        {
            UpdateController.qol.Toggle2DCharacter(true);
            //print(UpdateController.switcher.hitPosition);
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "killVolume")
        {
            DIE();
        }
        if(other.tag == "reposition")
        {
            string s = other.gameObject.name;
            string[] ss = s.Split(':');
            Vector3 v = new Vector3(float.Parse(ss[0]), float.Parse(ss[1]), float.Parse(ss[2]));

            UpdateController.qol.Toggle2DCharacter(true, v.x, v.y, v.z);
        }
    }

    void resetAllInteractables()
    {
        foreach (GameObject g in GameObject.FindGameObjectsWithTag("interact"))
        {
            g.GetComponent<InteractableObjectScript>().resetMe();
        }
    }
}
