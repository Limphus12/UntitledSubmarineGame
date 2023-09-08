using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum FishMovementType { WANDER, CHASE }

public class FishController : MonoBehaviour
{
    [Header("Movement")]
    [SerializeField] private float movementSpeed;
    [SerializeField] private float forceMulti;

    [Header("Rotation")]
    [SerializeField] private float rotateSpeed;

    [Header("Prediction")]
    [SerializeField] private float maxDistancePredict = 100;
    [SerializeField] private float minDistancePredict = 5;
    [SerializeField] private float maxTimePrediction = 5;
    private Vector3 standardPrediction, deviatedPrediction;

    [Header("Deviation")]
    [SerializeField] private float deviationAmount = 50;
    [SerializeField] private float deviationSpeed = 2;

    [Header("Wandering")]
    [SerializeField] private float wanderTargetRadius;

    public GameObject testObject;

    [SerializeField] private FishMovementType movementType;

    private Vector3 wanderTargetPos;

    [Space, SerializeField] private Transform target;

    private Rigidbody rb, targetRb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (target) targetRb = target.GetComponent<Rigidbody>();

        if (movementType == FishMovementType.WANDER)
        {
            CalculateWanderTarget();
        }
    }

    protected void SetTarget(Transform newTarget)
    {
        target = newTarget;
    }

    float CalculateLeadTimePercentage(Vector3 targetPos) => Mathf.InverseLerp(minDistancePredict, maxDistancePredict, Vector3.Distance(transform.position, targetPos));

    private void FixedUpdate()
    {
        CheckState(); Rotate(); Move();
    }

    private void CheckState()
    {
        float leadTimePercentage;

        switch (movementType)
        {
            case FishMovementType.WANDER:

                Wander();

                leadTimePercentage = CalculateLeadTimePercentage(wanderTargetPos);

                AddDeviation(wanderTargetPos);

                break;

            case FishMovementType.CHASE:

                leadTimePercentage = CalculateLeadTimePercentage(target.transform.position);

                PredictMovement(leadTimePercentage);

                AddDeviation(leadTimePercentage);

                break;

            default:
                break;
        }
    }

    private void Wander()
    {
        CheckDistance();
    }

    private void CheckDistance()
    {
        float distance = Vector3.Distance(wanderTargetPos, transform.position);

        //Debug.Log(distance);

        if (distance < 10f)
        {
            CalculateWanderTarget();
        }
    }

    private void CalculateWanderTarget()
    {
        Vector3 newTargetPos = UnityEngine.Random.insideUnitSphere * wanderTargetRadius + transform.position;

        float distance = Vector3.Distance(transform.position, newTargetPos);

        if (Physics.Raycast(transform.position, newTargetPos - transform.position, distance))
        {
            CalculateWanderTarget(); return;
        }

        else if (newTargetPos.y > -25f)
        {
            CalculateWanderTarget(); return;
        }

        wanderTargetPos = newTargetPos;
        if (testObject) testObject.transform.position = wanderTargetPos;
    }

    private void PredictMovement(float leadTimePercentage)
    {
        if (!targetRb) return;

        var predictionTime = Mathf.Lerp(0, maxTimePrediction, leadTimePercentage);

        standardPrediction = targetRb.position + targetRb.velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * deviationAmount * leadTimePercentage;

        deviatedPrediction = standardPrediction + predictionOffset;
    }

    private void AddDeviation(Vector3 target)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * deviationAmount;

        deviatedPrediction = target + predictionOffset;
    }

    private void Rotate()
    {
        switch (movementType)
        {
            case FishMovementType.WANDER:

                var wanderHeading = wanderTargetPos - transform.position;

                var wanderRotation = Quaternion.LookRotation(wanderHeading);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, wanderRotation, rotateSpeed * Time.fixedDeltaTime));

                break;
            case FishMovementType.CHASE:

                var chaseHeading = deviatedPrediction - transform.position;

                var chaseRotation = Quaternion.LookRotation(chaseHeading);
                rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, chaseRotation, rotateSpeed * Time.fixedDeltaTime));

                break;
            default:
                break;
        }
    }

    private void Move()
    {
        rb.AddRelativeForce(Vector3.forward * movementSpeed * forceMulti * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
