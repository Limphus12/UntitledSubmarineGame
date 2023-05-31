using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private Transform raycastPoint;

    private void Update()
    {
        CheckInteractables();
    }

    private void CheckInteractables()
    {
        if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit hit, interactionRange))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable != null && Input.GetKeyDown(interactionKey))
            {
                interactable.Interact();
            }
        }
    }
}
