using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishGenerator : MonoBehaviour
{
    [Header("Body Parts")]
    [SerializeField] private GameObject[] heads;
    [SerializeField] private GameObject[] segments, tails;
    [SerializeField] private float segmentSeperation;

    [SerializeField] private Vector2Int segmentsCountMinMax;

    private void Awake()
    {
        Generate();
    }

    private Rigidbody previousRigidbody;

    private void Generate()
    {
        GenerateHead();

        GenerateSegments();

        GenerateTail();
    }

    private void GenerateHead()
    {
        //pick a random head
        //head determines if it's passive or agressive

        previousRigidbody = GetComponent<Rigidbody>();
    }

    private void GenerateSegments()
    {
        int length = UnityEngine.Random.Range(segmentsCountMinMax.x, segmentsCountMinMax.y);

        float currentSeperation = 0f;

        for (int i = 0; i < length; i++)
        {
            currentSeperation -= segmentSeperation;

            Vector3 position = new Vector3(transform.position.x, transform.position.y, transform.position.z + currentSeperation);

            GameObject segment = Instantiate(segments[0], position, Quaternion.identity);

            //grab and assign the previous rigidbody to the joint's connected rigidbody
            Joint joint = segment.GetComponent<Joint>(); if (joint) joint.connectedBody = previousRigidbody;

            //grab and assign the segment's rigidbody to the previous rigidbody reference
            Rigidbody rb = segment.GetComponent<Rigidbody>(); if (rb) previousRigidbody = rb;
        }
    }

    private void GenerateTail()
    {
        //pick a random tail
    }
}
