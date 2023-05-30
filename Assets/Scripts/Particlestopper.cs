using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class Particlestopper : MonoBehaviour
{

    private void Awake()
    {
        ParticleSystem particleSystem = GetComponent<ParticleSystem>();

        if (particleSystem) particleSystem.Play();
         particleSystem.Stop();
    }

}
