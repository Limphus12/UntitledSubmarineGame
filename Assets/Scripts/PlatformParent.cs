using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformParent : MonoBehaviour
{
    private void OnCollisionEnter(Collision collision)
    {
        collision.transform.SetParent(transform);
    }

    private void OnCollisionExit(Collision collision)
    {
        collision.transform.SetParent(null);
    }
    
    private void OnTriggerEnter(Collider other)
    {
        //other.transform.SetParent(transform);
    }

    private void OnTriggerExit(Collider other)
    {
        //other.transform.SetParent(null);
    }

    public void MoveChildren(Vector3 targetPos, float speed)
    {
        Transform[] children = transform.GetComponentsInChildren<Transform>();

        foreach (Transform child in children)
        {
            child.localPosition = Vector3.MoveTowards(child.localPosition, targetPos, speed * Time.deltaTime);
        }
    }
}
