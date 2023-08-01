using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum RotationMode { Instant, Lerped, Slerped, Fixed }

public class PlayerSpotlight : MonoBehaviour
{
    [Header("Turret Movement")]
    public RotationMode rotationMode;

    [Tooltip("Rotation Speed only applies to Lerped, Slerped and Fixed Rotation Modes")]
    public float rotationSpeed = 5f;

    private Vector3 point;

    private Camera playerCamera;

    private void Awake()
    {
        if (!playerCamera) playerCamera = Camera.main;
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (playerCamera) CalculatePoint();
    }

    void CalculatePoint()
    {
        //point = playerCamera.ScreenToWorldPoint(playerCamera.transform.forward + (Vector3.forward * 250f));
        point = playerCamera.ScreenToWorldPoint(Input.mousePosition + (Vector3.forward * 250f));

        CalculateRotation(point);
    }

    void CalculateRotation(Vector3 point)
    {
        //I'm applying some code from - https://www.youtube.com/watch?v=ox8dvz6e6ws, So that I can freely rotate the turret in  any direction and it'll end up pointing in the right direction. Specifically the transform.InverseTransformPoint(point);

        //Variables
        Vector3 direction, rotation;
        Vector3 barrelDirection, barrelRotation;
        Quaternion lookRotation, barrelLookRotation, finalLookRotation, barrelFinalLookRotation;

        switch (rotationMode)
        {
            case RotationMode.Instant:

                //Instant Rotation - Turret & Barrel instantly rotates to where the camera is facing.

                direction = transform.InverseTransformPoint(point);
                lookRotation = Quaternion.LookRotation(direction);
                rotation = lookRotation.eulerAngles;
                transform.localRotation = Quaternion.Euler(rotation);

                break;
            case RotationMode.Lerped:

                //Lerped Rotation - Turret & Barrel interpolate to where the camera is facing. The time to rotate is the
                //same whether you rotate 1 degree or 100 degrees...

                direction = transform.InverseTransformPoint(point);
                lookRotation = Quaternion.LookRotation(direction);
                rotation = Quaternion.Lerp(transform.localRotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
                transform.localRotation = Quaternion.Euler(rotation);

                break;
            case RotationMode.Slerped:

                //Slerped Rotation - Similar to lerped rotation above, just changes Lerp to Slerp lol

                direction = transform.InverseTransformPoint(point);
                lookRotation = Quaternion.LookRotation(direction);
                rotation = Quaternion.Slerp(transform.localRotation, lookRotation, Time.deltaTime * rotationSpeed).eulerAngles;
                transform.localRotation = Quaternion.Euler(rotation);

                break;
            case RotationMode.Fixed:

                //Constant Rotation - Fixed no. of degrees per second/turn rate.

                direction = transform.InverseTransformPoint(point);
                //direction.y = 0.0f;

                lookRotation = Quaternion.LookRotation(direction);
                finalLookRotation = Quaternion.RotateTowards(transform.localRotation, lookRotation, Time.deltaTime * rotationSpeed);

                transform.localRotation = finalLookRotation;

                break;

            default:
                break;
        }
    }
}
