using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum Axis { X, Y, Z }

public class Elevator : MonoBehaviour
{
    [SerializeField] Axis movementAxis;

    [SerializeField] float speed;

    [SerializeField] PlatformParent platform;

    bool isMoving; Vector3 targetPos;

    private void Awake()
    {
        isMoving = false;
        targetPos = transform.position;
    }

    public void SetTargetPos(float a)
    {
        switch (movementAxis)
        {
            case Axis.X:

                targetPos = new Vector3(a, transform.position.y, transform.position.z);

                break;
            case Axis.Y:

                targetPos = new Vector3(transform.position.x, a, transform.position.z);

                break;
            case Axis.Z:

                targetPos = new Vector3(transform.position.z, transform.position.y, a);

                break;

            default:
                break;
        }
    }

    private void Update()
    {
        if (isMoving) Move(targetPos);
    }

    public void StartMove()
    {
        isMoving = true;
    }

    void Move(Vector3 position) 
    {
        transform.localPosition = Vector3.MoveTowards(transform.localPosition, targetPos, speed * Time.deltaTime);

        //if (platform) platform.MoveChildren(targetPos, speed * Time.deltaTime);

        //Transform[] children = transform.GetComponentsInChildren<Transform>();

        //foreach (Transform child in children)
        {
            //child.localPosition = Vector3.MoveTowards(child.localPosition, targetPos, speed * Time.deltaTime);
        }

        if (Vector3.Distance(transform.localPosition, targetPos) < 0.1f) EndMove();
    }

    void EndMove()
    {
        transform.localPosition = targetPos;

        isMoving = false;
    }
}
