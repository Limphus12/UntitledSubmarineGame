using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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


    [Space, SerializeField] private Transform target;

    private Rigidbody rb, targetRb;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();

        if (target) targetRb = target.GetComponent<Rigidbody>();
    }

    float CalculateLeadTimePercentage() => Mathf.InverseLerp(minDistancePredict, maxDistancePredict, Vector3.Distance(transform.position, target.transform.position));

    private void FixedUpdate()
    {
        var leadTimePercentage = CalculateLeadTimePercentage();

        PredictMovement(leadTimePercentage);

        AddDeviation(leadTimePercentage);

        Rotate();

        Move();
    }

    private void PredictMovement(float leadTimePercentage)
    {
        var predictionTime = Mathf.Lerp(0, maxTimePrediction, leadTimePercentage);

        standardPrediction = targetRb.position + targetRb.velocity * predictionTime;
    }

    private void AddDeviation(float leadTimePercentage)
    {
        var deviation = new Vector3(Mathf.Cos(Time.time * deviationSpeed), 0, 0);

        var predictionOffset = transform.TransformDirection(deviation) * deviationAmount * leadTimePercentage;

        deviatedPrediction = standardPrediction + predictionOffset;
    }

    private void Rotate()
    {
        var heading = deviatedPrediction - transform.position;

        var rotation = Quaternion.LookRotation(heading);
        rb.MoveRotation(Quaternion.RotateTowards(transform.rotation, rotation, rotateSpeed * Time.fixedDeltaTime));
    }

    private void Move()
    {
        rb.AddRelativeForce(Vector3.forward * movementSpeed * forceMulti * Time.fixedDeltaTime, ForceMode.VelocityChange);
    }
}
