using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum DoorMovement { Move, Rotate }

public class Door : MonoBehaviour, IInteractable
{
    [Header("Attributes")]
    [SerializeField] private DoorMovement movementType;

    [Space, SerializeField] private Vector3 closedPosition;
    [SerializeField] private Vector3 openPositionA, openPositionB;

    [Space, SerializeField] private float positionSmooth;

    [Space, SerializeField] private Vector3 closedRotation;
    [SerializeField] private Vector3 openRotationA, openRotationB;

    [Space, SerializeField] private float rotationSmooth;

    private bool isOpen, isRotating, isMoving;

    private bool AorB;

    public void SetAorB(bool c) => AorB = c;

    private void Update()
    {
        switch (movementType)
        {
            case DoorMovement.Move:

                if (isMoving)
                {
                    if (isOpen)
                    {
                        if (AorB) Move(openPositionA);
                        else if (!AorB) Move(openPositionB);
                    }

                    else if (!isOpen) Move(closedPosition);
                }

                break;

            case DoorMovement.Rotate:

                if (isRotating)
                {
                    if (isOpen)
                    {
                        if (AorB) Rotate(openRotationA);
                        else if (!AorB) Rotate(openRotationB);
                    }

                    else if (!isOpen) Rotate(closedRotation);
                }

                break;

            default:
                break;
        }
    }

    public void Interact()
    {
        isOpen = !isOpen;
        StartRotate();
        StartMove();
    }

    void StartRotate()
    {
        rotateI = 0;
        isRotating = true;
    }

    void EndRotate()
    {
        rotateI = 0;
        isRotating = false;
    }

    void StartMove()
    {
        moveI = 0;
        isMoving = true;
    }

    void EndMove()
    {
        moveI = 0;
        isMoving = false;
    }

    float rotateI = 0, moveI = 0;

    void Rotate(Vector3 rotation)
    {
        rotateI += Time.deltaTime * rotationSmooth;

        //apply target rotation
        transform.localRotation = Quaternion.Slerp(transform.localRotation, Quaternion.Euler(rotation), rotateI);

        if (rotateI >= 1)
        {
            EndRotate();
        }
    }

    void Move(Vector3 position)
    {
        moveI += Time.deltaTime * positionSmooth;

        //apply target rotation
        transform.localPosition = Vector3.Lerp(transform.localPosition, position, moveI);

        if (moveI >= 1)
        {
            EndMove();
        }
    }
}