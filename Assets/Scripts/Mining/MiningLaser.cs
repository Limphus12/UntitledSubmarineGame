using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiningLaser : MonoBehaviour
{
    [Header("Laser")]
    [SerializeField] private Transform playerCamera;
    [SerializeField] private float maxMiningDistance;

    [Header("VFX")]
    [SerializeField] private Transform firePoint;
    [SerializeField] private LineRenderer miningLaser;
    [SerializeField] private GameObject hitParticles;

    private bool isMining;

    private void Update()
    {
        Inputs();
    }

    private void LateUpdate()
    {
        VFX();
    }

    private void Inputs()
    {
        isMining = Input.GetMouseButton(0);

        if (isMining) Mine();
    }

    private void Mine()
    {
        if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxMiningDistance))
        {
            IMineable mineable = hit.transform.GetComponent<IMineable>();

            if (hitParticles)
            {
                GameObject particles = Instantiate(hitParticles, hit.point, Quaternion.Euler(hit.normal));
                Destroy(particles, 1f);
            }

            if (mineable != null) { if (mineable.CanMine()) mineable.Mine(); };
        }
    }

    private void VFX()
    {
        if (!miningLaser) return;

        if (!isMining)
        {
            miningLaser.SetPosition(0, firePoint.position);
            miningLaser.SetPosition(1, firePoint.position);
        }

        else if (isMining)
        {
            miningLaser.SetPosition(0, firePoint.position);

            if (Physics.Raycast(playerCamera.position, playerCamera.forward, out RaycastHit hit, maxMiningDistance))
            {
                miningLaser.SetPosition(1, hit.point);
            }

            else miningLaser.SetPosition(1, firePoint.position);
        }
    }
}
