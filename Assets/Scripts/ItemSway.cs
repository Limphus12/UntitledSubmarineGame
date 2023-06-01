using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemSway : MonoBehaviour
{
    [Header("Attributes - Item Default Settings")]
    [SerializeField] protected Vector3 defaultPosition;
    [SerializeField] protected Quaternion defaultRotation;

    [Space]
    [SerializeField] protected float defaultSwaySmooth = 8.0f;
    [SerializeField] protected float defaultSwayAmount = 2.0f, defaultSwayMaximum = 2.0f;

    [Space]
    [SerializeField] protected float defaultTiltSmooth = 8.0f;
    [SerializeField] protected float defaultTiltAmount = 2.0f, defaultTiltMaximum = 2.0f;

    protected Quaternion initialRotation;

    protected virtual void Awake()
    {
        initialRotation = defaultRotation;
    }

    protected virtual void Update()
    {
        CheckSway();
        CheckTilt();
    }

    protected virtual void CheckSway()
    {
        Sway(defaultSwayAmount, defaultSwayMaximum, defaultSwaySmooth, defaultPosition);
    }

    protected virtual void CheckTilt()
    {
        Tilt(defaultTiltAmount, defaultTiltMaximum, defaultTiltSmooth, defaultRotation);
    }

    protected void Sway(float amount, float maximum, float smooth, Vector3 position)
    {
        //calculate inputs
        Vector2 inputs = Inputs();

        //calculate sway positions
        float swayPositionX = Mathf.Clamp(inputs.x * amount, -maximum, maximum) / 100;
        float swayPositionY = Mathf.Clamp(inputs.y * amount, -maximum, maximum) / 100;

        //calculate final target position
        Vector3 targetPosition = new Vector3(swayPositionX, swayPositionY);

        //apply target position
        transform.localPosition = Vector3.Lerp(transform.localPosition, targetPosition + position, smooth * Time.deltaTime);
    }

    protected void Tilt(float amount, float maximum, float smooth, Quaternion rotation)
    {
        //calculate inputs
        Vector2 inputs = Inputs();

        //calculate tilt rotations
        float tiltRotationY = Mathf.Clamp(inputs.x * amount, -maximum, maximum);
        float tiltRotationX = Mathf.Clamp(inputs.y * amount, -maximum, maximum);
        float tiltRotationZ = Mathf.Clamp(Input.GetAxis("Horizontal") * amount, -maximum, maximum);

        //calculate target rotation
        Quaternion tiltRotation = Quaternion.Euler(new Vector3(tiltRotationX, -tiltRotationY, -tiltRotationZ));

        Quaternion targetRotation = rotation * tiltRotation;

        //apply target rotation
        transform.localRotation = Quaternion.Lerp(transform.localRotation, targetRotation * initialRotation, smooth * Time.deltaTime);
    }

    private Vector2 Inputs()
    {
        return new Vector2(Input.GetAxis("Mouse X"), Input.GetAxis("Mouse Y"));
    }
}