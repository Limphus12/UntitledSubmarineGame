using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using com.limphus.utilities;

public class InteractionScript : MonoBehaviour
{
    [SerializeField] private float interactionRange;
    [SerializeField] private KeyCode interactionKey = KeyCode.E;

    [SerializeField] private Transform raycastPoint;

    public event EventHandler<OnInteractEventArgs> OnInteractionCheck;

    public class OnInteractEventArgs : EventArgs { public bool i; public GameObject j; }

    private void Update()
    {
        CheckInteractables();
    }

    private void CheckInteractables()
    {
        //seperate raycast for the hangar sub, because we wanna interact with it from further away
        //if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit subHit, 25f))
        //{
        //    IInteractable interactable = subHit.transform.GetComponent<IInteractable>();
        //
        //    if (interactable != null)
        //    {
        //        OnInteractionCheck?.Invoke(this, new Events.OnBoolChangedEventArgs { i = true });
        //
        //        if (subHit.transform.GetComponent<HangarSubmarine>() && Input.GetKeyDown(interactionKey))
        //        {
        //            interactable.Interact();
        //        }
        //    }
        //
        //    else if (interactable == null)
        //    {
        //        OnInteractionCheck?.Invoke(this, new Events.OnBoolChangedEventArgs { i = false });
        //    }
        //}

        //else OnInteractionCheck?.Invoke(this, new Events.OnBoolChangedEventArgs { i = false });

        //regular checks for other interactables
        if (Physics.Raycast(raycastPoint.position, raycastPoint.forward, out RaycastHit hit, interactionRange))
        {
            IInteractable interactable = hit.transform.GetComponent<IInteractable>();

            if (interactable != null)
            {
                OnInteractionCheck?.Invoke(this, new OnInteractEventArgs { i = true, j = hit.transform.gameObject });

                if (Input.GetKeyDown(interactionKey))
                {
                    interactable.Interact();
                }
            }

            else if (interactable == null)
            {
                OnInteractionCheck?.Invoke(this, new OnInteractEventArgs { i = false });
            }
        }

        else OnInteractionCheck?.Invoke(this, new OnInteractEventArgs { i = false });
    }
}
