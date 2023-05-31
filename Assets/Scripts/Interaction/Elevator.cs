using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Elevator : MonoBehaviour
{
    [SerializeField] float positionSmooth;
    bool isMoving;

    Vector3 targetPos;

    private void Awake()
    {
        isMoving = false;
        targetPos = transform.position;
    }

    public void SetTargetPos(float y)
    {
        targetPos = new Vector3(transform.position.x, y, transform.position.z);
    }

    private void Update()
    {
        if (isMoving) Move(targetPos);
    }

    public void StartMove()
    {
        moveI = 0;
        isMoving = true;
    }

    float moveI = 0;
    void Move(Vector3 position) 
    {
        moveI += (positionSmooth / 1000) * Time.deltaTime;

        //apply target rotation
        transform.localPosition = Vector3.Lerp(transform.localPosition, position, moveI);

        if (moveI >= 1)
        {
            EndMove();
        }
    }

    void EndMove()
    {
        moveI = 0;
        isMoving = false;
    }
}
