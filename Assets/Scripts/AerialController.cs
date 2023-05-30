using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum AerialType { Plane, Heli, Hover }

[RequireComponent(typeof(Rigidbody))]
public class AerialController : MonoBehaviour
{
    [Header("Aerial Types")]
    [SerializeField] private AerialType aerialType;

    [Header("Aerial Stats")]
    [Tooltip("Right Thrust, Up Thrust, Forward Thrust")] [SerializeField] private Vector3 thrust;

    [Tooltip("Pitch, Yaw, Roll")] [SerializeField] private Vector3 turnTorque = new Vector3(90f, 25f, 45f);

    [Tooltip("Smooth Rate for transitioning between Plane and Hover Mode")] [SerializeField] private float planeToHoverSmooth;
    [Tooltip("Hover turning smooth rate")] [SerializeField] private float hoverTurnSmooth;

    private Rigidbody rb;
    private float horizontalInput, verticalInput, jumpInput, rollInput, y;

    public Transform mouseAim;

    // Start is called before the first frame update
    void Start()
    {
        //Lock Cursor
        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false;

        if (!rb) rb = gameObject.GetComponent<Rigidbody>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.LeftShift))
        {
            if (aerialType == AerialType.Plane)
            {
                aerialType = AerialType.Hover;
                return;
            }

            else if (aerialType == AerialType.Hover)
            {
                aerialType = AerialType.Plane;
                return;
            }
        }
    }

    private void FixedUpdate()
    {
        CalculateInputs();
        HandleMovement();
    }

    void CalculateInputs()
    {
        verticalInput = Input.GetAxis("Vertical");
        horizontalInput = Input.GetAxis("Horizontal");
        rollInput = Input.GetAxis("Roll");
        jumpInput = Input.GetAxis("Jump");
    }

    void HandleMovement()
    {
        Vector3 forward = transform.TransformDirection(Vector3.forward);
        Vector3 right = transform.TransformDirection(Vector3.right);

        switch (aerialType)
        {
            case AerialType.Plane:

                //Constant Forward Thrust
                rb.AddForce(forward * thrust.z * Time.fixedDeltaTime, ForceMode.Acceleration);

                //Pitching, Yawing and Rolling
                //rb.AddRelativeTorque(new Vector3(turnTorque.x * -verticalInput, turnTorque.y * horizontalInput, turnTorque.z * rollInput) * Time.fixedDeltaTime, ForceMode.Acceleration);
                rb.AddRelativeTorque(new Vector3(turnTorque.x * -verticalInput, 0, turnTorque.z * -horizontalInput) * Time.fixedDeltaTime, ForceMode.Acceleration);

                break;

            case AerialType.Heli:

                //Add helicopter movement

                break;

            case AerialType.Hover:

                /*
                //Movement

                //rb.AddRelativeTorque(right * rollInput * Time.fixedDeltaTime);

                //y -= rollInput * turnTorque.y * Time.fixedDeltaTime;

                //Rotation - Calculating rotation based on camera.
                Vector3 turretDirection, turretRotation;
                Quaternion targetRotation, turretLookRotation;

                Vector3 point = playerCamera.ScreenToWorldPoint(Input.mousePosition + (Vector3.forward * 250f));

                turretDirection = transform.InverseTransformPoint(point);
                turretLookRotation = Quaternion.LookRotation(turretDirection);
                turretRotation = Quaternion.Lerp(transform.localRotation, turretLookRotation, Time.deltaTime * turnTorque.y).eulerAngles;
                //transform.localRotation = Quaternion.Euler(transform.rotation.x, turretRotation.y, transform.rotation.z);

                //Zero in the X and Z rotation
                //if (transform.rotation.x != 0 || transform.rotation.z != 0) targetRotation = Quaternion.Euler(0, y, 0);
                //if (transform.rotation.x != 0 || transform.rotation.z != 0) targetRotation = Quaternion.Euler(0, tgtRotation.y, 0);

                //else targetRotation = Quaternion.Euler(transform.rotation.x, y, transform.rotation.z);
                //else targetRotation = Quaternion.Euler(transform.rotation.x, tgtRotation.y, transform.rotation.z);

                if (transform.rotation.x != 0 || transform.rotation.z != 0) targetRotation = Quaternion.Euler(0, turretRotation.y, 0);

                else targetRotation = Quaternion.Euler(transform.rotation.x, turretRotation.y, transform.rotation.z);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, planeToHoverSmooth * Time.fixedDeltaTime);
                */


                //Calculating the move direction based on input.
                Vector3 moveDirection = (forward * thrust.z * verticalInput) + (right * thrust.x * horizontalInput) + (Vector3.up * thrust.y * jumpInput);

                //Adding force.
                rb.AddForce(moveDirection * Time.fixedDeltaTime, ForceMode.Acceleration);


                //Rotation

                Quaternion targetRotation;

                //Zero in the X and Z rotation
                if (transform.rotation.x != 0 || transform.rotation.z != 0) targetRotation = Quaternion.Euler(0, mouseAim.rotation.y, 0);

                else targetRotation = Quaternion.Euler(transform.rotation.x, mouseAim.rotation.y, transform.rotation.z);

                transform.rotation = Quaternion.Lerp(transform.rotation, targetRotation, planeToHoverSmooth * Time.fixedDeltaTime);

                break;

            default:
                return;
        }
    }
}