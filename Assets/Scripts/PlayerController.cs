using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

// Code sourced from: https://sharpcoderblog.com/blog/unity-3d-fps-controller

// Edits & Additions made by Limphus

public class PlayerController : MonoBehaviour
{
    #region Variables

    #region Public Variables

    [Header("Scripts")]
    public UIManager uiManager;


    [Header("Movement")]
    public float walkingSpeed = 4.0f;
    public float crouchingSpeed = 2.0f;
    public float proningSpeed = 1.0f;

    [Space]
    public float characterControllerStandingHeight = 1.8f;
    public float characterControllerCrouchingHeight = 0.9f;
    public float characterControllerProningHeight = 0.45f;

    [Space]
    public float stanceSpeed; //stanceSpeed is how quickly the character controller changes height.
    public float upSpeed; //upSpeed is used to countreract the stuttering when un-crouching/proning, by moving the player upwards.

    [Header("Jumping + Gravity")]
    public float jumpAmount = 8.5f;
    public float gravity = 20.0f;

    [Header("Climbing")]
    public float ladderClimbSpeed;
    public float ladderStrafeSpeed;
    public float ladderMaxAngle = 45.0f;

    [Header("Camera")]
    public Camera playerCamera;
    public float lookSpeed = 1.0f;
    public float lookStandLimit = 90f, lookCrouchLimit = 90f, lookProneLimit = 45f;

    [Space]
    public bool cameraLean = true;
    public float cameraLeanAmount = 4.0f;

    [Space]
    public Vector3 cameraStandingPos;
    public Vector3 cameraCrouchingPos;
    public Vector3 cameraProningPos;

    [Header("Raycasting")]
    public float toStandRaycastLength = 1.0f;
    public float toCrouchRaycastLength = 0.5f;

    [Space]
    public float pickupRaycastLength = 1.5f;
    public GameObject PickupUI, GoldUI;

    [Header("FX")]
    public bool useFX;

    public GameObject hands;
    public GameObject vignette, speedLines;
    public GameObject shadow;

    [HideInInspector]
    public bool canMove = true, canStand = true, canCrouch = true, isCrouching = false, isProning = false;

    #endregion

    #region Private Variables

    private CharacterController characterController;
    private Vector3 moveDirection = Vector3.zero;
    private float rotationX = 0, lookXLimit = 0;

    #endregion

    #endregion

    #region BuiltInMethods

    void Start()
    {
        characterController = GetComponent<CharacterController>();

        lookXLimit = lookStandLimit;

        // Lock cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;
    }

    void Update()
    {
        Raycasting();

        CalculateMovement();
    }

    void LateUpdate()
    {
        if (useFX)
        {
            RaycastHit shadowHit;
            if (Physics.Raycast(transform.position, Vector3.down, out shadowHit, Mathf.Infinity))
            {
                shadow.transform.position = shadowHit.point;

                float distance = Vector3.Distance(transform.position, shadow.transform.position);

                shadow.transform.localScale = new Vector3(-1 / (distance), 1, -1 / (distance));
            }
        }
    }

    #endregion

    #region CustomMethods

    #region Movement

    //Calculates player Movement
    void CalculateMovement()
    {
        // We are grounded, so recalculate move direction based on axes
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);
        // Checks which state the player is in, and sets speed to corresponding speed value - Limphus
        float speed;

        if (isCrouching) speed = crouchingSpeed;

        else if (isProning) speed = proningSpeed;

        else speed = walkingSpeed;

        float curSpeedX = canMove ? speed * Input.GetAxis("Horizontal") : 0;
        float curSpeedZ = canMove ? speed * Input.GetAxis("Vertical") : 0;

        float movementDirectionY = moveDirection.y;
        moveDirection = (forward * curSpeedZ) + (right * curSpeedX);

        //speed lines check - Limphus
        if (moveDirection.x > 0 || moveDirection.z > 0 || moveDirection.x < 0 || moveDirection.z < 0)
        {
            if (useFX)
            {
                if (!isCrouching && !isProning)
                {
                    speedLines.SetActive(true);
                }

                else if (isCrouching || isProning)
                {
                    speedLines.SetActive(false);
                }
            }
        }

        else if (useFX) speedLines.SetActive(false);


        // Edited by Limphus, seperated the check for isGrounded from the input check
        // Now the y velocity is reset to 0 when on the ground
        // Before, if you didnt jump and you fell from a ledge, the velocity wasnt reset
        // So the y velocity would build up over continuous falls without jumping
        if (characterController.isGrounded)
        {
            if (Input.GetButton("Jump") && canMove)
            {
                moveDirection.y = jumpAmount;
            }
        }

        else
        {
            moveDirection.y = movementDirectionY;
        }

        // Apply gravity. Gravity is multiplied by deltaTime twice (once here, and once below
        // when the moveDirection is multiplied by deltaTime). This is because gravity should be applied
        // as an acceleration (ms^-2)
        if (!characterController.isGrounded)
        {
            moveDirection.y -= gravity * Time.deltaTime;
        }

        Move();

    }

    //Deals with Movement
    void Move()
    {
        // Move the controller
        characterController.Move(moveDirection * Time.deltaTime);

        // Player and Camera rotation
        if (canMove)
        {
            rotationX += -Input.GetAxis("Mouse Y") * lookSpeed;
            rotationX = Mathf.Clamp(rotationX, -lookXLimit, lookXLimit);

            // Edited by Limphus, added in some camera tilt.
            if (!cameraLean)
            {
                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, 0);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }

            else
            {
                float currentX = Input.GetAxis("Horizontal");

                playerCamera.transform.localRotation = Quaternion.Euler(rotationX, 0, currentX * cameraLeanAmount);
                transform.rotation *= Quaternion.Euler(0, Input.GetAxis("Mouse X") * lookSpeed, 0);
            }
        }
    }

    #endregion

    #region Stance

    // Changes the character controller height - Limphus
    void ChangeStance(float height, Vector3 cameraPos, float cameraXLimit)
    {
        characterController.height = Mathf.Lerp(characterController.height, height, stanceSpeed * Time.deltaTime);

        Vector3 newCameraPos;

        newCameraPos.x = Mathf.Lerp(playerCamera.transform.localPosition.x, cameraPos.x, stanceSpeed * Time.deltaTime);
        newCameraPos.y = Mathf.Lerp(playerCamera.transform.localPosition.y, cameraPos.y, stanceSpeed * Time.deltaTime);
        newCameraPos.z = Mathf.Lerp(playerCamera.transform.localPosition.z, cameraPos.z, stanceSpeed * Time.deltaTime);

        playerCamera.transform.localPosition = newCameraPos;

        //playerCamera.transform.localPosition = cameraPos;

        lookXLimit = cameraXLimit;
    }
    #endregion

    #region Other

    // All your raycasting needs! - Limphus
    void Raycasting()
    {
        // This raycasting is used to find items that can be picked up.
        RaycastHit pickupHit;
        if (Physics.Raycast(playerCamera.transform.position, playerCamera.transform.forward, out pickupHit, pickupRaycastLength))
        {
          
        }
    }

    #endregion

    #endregion
}