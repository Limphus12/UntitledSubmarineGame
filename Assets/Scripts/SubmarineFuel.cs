using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class SubmarineFuel : MonoBehaviour
{
    [SerializeField] private float maxFuel, fuelConsumption;

    [SerializeField] private Rigidbody submarineRB;
    [SerializeField] private SubmarineController submarineController;

    [SerializeField] private SliderUI slider;

    [SerializeField] private UnityEvent NoFuelEvent;

    private float currentFuel;
    public bool HasFuel { get; private set; }

    private void Awake()
    {
        HasFuel = true; currentFuel = maxFuel;

        if (slider) slider.SetMaxValue(maxFuel);
    }

    private void FixedUpdate()
    {
        CheckSpeed(); UI();
    }

    private void UI()
    {
        if (!slider) return;

        slider.SetValue(currentFuel);
    }

    private void CheckSpeed()
    {
        if (!submarineRB) return;

        //if (submarineRB.velocity.magnitude > 1f)
        //{
        //    UseFuel();
        //}

        if (submarineController.GetCurrentThrust > 0.1f || (submarineController.GetSubmarineInput.magnitude > 0f && submarineRB.velocity.magnitude > 2.5f))
        {
            UseFuel();
        }
    }

    private void UseFuel()
    {
        currentFuel -= fuelConsumption * Time.fixedDeltaTime;

        if (currentFuel <= 0) NoFuel();
    }

    private void NoFuel()
    {
        HasFuel = false; NoFuelEvent?.Invoke();
    }
}
