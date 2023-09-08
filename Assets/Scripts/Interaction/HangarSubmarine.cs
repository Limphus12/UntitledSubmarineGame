using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HangarSubmarine : MonoBehaviour, IInteractable
{
    public void Interact()
    {
        GameManager.LoadIntoGame();
    }
}