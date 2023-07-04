using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    [SerializeField] private Transform playerCamera;


    private bool isMining; private Vector3 initialPosition;

    private void Update()
    {
        Inputs();
    }

    private void Inputs()
    {
        isMining = Input.GetMouseButton(0);

        if (isMining) Mine();
    }

    private void Mine()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, Mathf.Infinity))
        {
            IMineable mineable = hit.transform.GetComponent<IMineable>();

            if (mineable != null) { if (mineable.CanMine()) mineable.Mine(); };
        }
    }

}
