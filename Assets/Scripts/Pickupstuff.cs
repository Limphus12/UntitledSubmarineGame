using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pickupstuff : MonoBehaviour
{
    [Header("Pickup Settings")]
    [SerializeField] Transform holdArea;
    private GameObject heldObj;
    private Rigidbody heldObjrb;

    [Header("Physics Parameters")]
    [SerializeField] private float PickupRange = 5.0f;
    [SerializeField] private float PickupForce = 150.0f;

    // Update is called once per frame
    private void FixedUpdate()
    {
         if (heldObj == null)
        {
            RaycastHit hit;
            if (Physics.Raycast(transform.position, transform.TransformDirection(Vector3.forward), out hit, PickupRange))
            {
                PickupObject(hit.transform.gameObject);
            }
        }
         else
        {
            DropObject();
        }

        if (heldObj != null)
        {
            MoveObject();
        }
    }

    void MoveObject()
    {
        if(Vector3.Distance(heldObj.transform.position, holdArea.position) > 0.1f)
        {
            Vector3 moveDirection = (holdArea.position - heldObj.transform.position);
            heldObjrb.AddForce(moveDirection * PickupForce * Time.fixedDeltaTime);
        }
    }


    void PickupObject(GameObject pickObj)
    {
        if (pickObj.GetComponent<Rigidbody>())
        {
            heldObjrb = pickObj.GetComponent<Rigidbody>();
            heldObjrb.useGravity = false;
            heldObjrb.drag = 10;
            heldObjrb.constraints = RigidbodyConstraints.FreezeRotation;

            heldObjrb.transform.parent = holdArea;
            heldObj = pickObj;
        }
    }

    void DropObject()
    {

            heldObjrb.useGravity = true;
            heldObjrb.drag = 1;
            heldObjrb.constraints = RigidbodyConstraints.None;

            heldObjrb.transform.parent = null;
            heldObj = null;

    }
}
