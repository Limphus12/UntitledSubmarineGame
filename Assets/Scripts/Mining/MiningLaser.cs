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


    [Header("SFX")]
    [SerializeField] private AudioSource audioSource;
    [SerializeField] private AudioClip startMineClip, mineClip, endMineClip;

    private bool isHitting, isMining, wasMining;

    private void Update()
    {
        Inputs();
        SFX();
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
            isHitting = true;

            IMineable mineable = hit.transform.GetComponent<IMineable>();

            if (hitParticles)
            {
                GameObject particles = Instantiate(hitParticles, hit.point, Quaternion.Euler(hit.normal));
                Destroy(particles, 1f);
            }

            if (mineable != null)
            {
                if (mineable.CanMine())
                {
                    mineable.Mine();
                }
            }
        }

        else isHitting = false;
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

    private void SFX()
    {
        if (!audioSource) return;



        if (!isMining)
        {
            //if we were mining last frame, but not mining now, play the end mining sound
            if (wasMining)
            {
                if (audioSource.isPlaying) audioSource.Stop();

                audioSource.loop = false; audioSource.clip = endMineClip; audioSource.Play();
            }
        }

        else if (isMining)
        {
            //if we are mining and we was not mining beforehand, play the start mining sound
            if (!wasMining)
            {
                if (audioSource.isPlaying) audioSource.Stop();

                audioSource.loop = false; audioSource.clip = startMineClip; audioSource.Play();
            }

            //if we are mining and we were mining beforehand, make sure the start clip plays fully before moving onto the mining loop
            else if (wasMining)
            {
                if (isHitting)
                {
                    if (audioSource.isPlaying) return;

                    audioSource.loop = true; audioSource.clip = mineClip; audioSource.Play();
                }

                else
                {
                    if (audioSource.isPlaying) audioSource.Stop();

                    audioSource.loop = false; audioSource.clip = endMineClip; audioSource.Play();
                }
            }
        }

        wasMining = isMining;
    }
}
