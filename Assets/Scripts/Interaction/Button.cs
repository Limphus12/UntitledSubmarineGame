using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Button : MonoBehaviour, IInteractable
{
    [SerializeField] private UnityEvent buttonEvent;

    public void Interact()
    {
        buttonEvent?.Invoke();
    }
}
