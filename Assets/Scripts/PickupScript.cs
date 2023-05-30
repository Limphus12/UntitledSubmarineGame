using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PickupScript : MonoBehaviour
{
    // The pickup point where the object will move towards
    [SerializeField] private Transform pickupPoint, playerCamera;

    // The force at which the object will move towards the pickup point
    [SerializeField] private float moveForce = 5f, raycastLength = 10f;

    // The rigidbody of the object that is currently being held
    public Rigidbody heldObject;

    bool mouseInput;

    private void LateUpdate()
    {
        Inputs(); Pickup();
    }

    private void FixedUpdate()
    {
        MovePickup();
    }

    private void Inputs()
    {
        mouseInput = Input.GetMouseButton(0);
    }

    void Pickup()
    {
        if (mouseInput && !heldObject)
        {
            // Try to pick up an object
            RaycastHit hit;
            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out hit, 10f))
            {
                Rigidbody rb = hit.collider.GetComponent<Rigidbody>();

                if (rb != null && rb.isKinematic == false)
                {
                    // Pick up the object
                    heldObject = rb;
                    heldObject.useGravity = false;
                    heldObject.drag = 10;
                    heldObject.constraints = RigidbodyConstraints.FreezeRotation;

                    return;
                }

                rb = hit.collider.GetComponentInParent<Rigidbody>();

                if (rb != null && rb.isKinematic == false)
                {
                    // Pick up the object
                    heldObject = rb;
                    heldObject.useGravity = false;
                    heldObject.drag = 10;
                    heldObject.constraints = RigidbodyConstraints.FreezeRotation;

                    return;
                }
            }
        }

        if (!mouseInput && heldObject)
        {
            // Try to place down the held object
            heldObject.useGravity = true;
            heldObject.drag = 1;
            heldObject.constraints = RigidbodyConstraints.None;

            heldObject = null;
        }
    }

    void MovePickup()
    {
        // Move the held object towards the hold position using Lerp
        if (heldObject != null)
        {
            if (Vector3.Distance(heldObject.transform.position, pickupPoint.position) > 0.1f)
            {
                Vector3 moveDirection = (pickupPoint.position - heldObject.transform.position);
                heldObject.AddForce(moveDirection * moveForce * 1000 * Time.fixedDeltaTime);
            }

            //Vector3 newPosition = Vector3.Lerp(heldObject.transform.position, pickupPoint.position, moveForce * Time.deltaTime);
            //Quaternion newRotation = Quaternion.Lerp(heldObject.transform.rotation, pickupPoint.rotation, moveForce * Time.deltaTime);
            //heldObject.position = newPosition;
            //heldObject.rotation = newRotation;
        }
    }
}
