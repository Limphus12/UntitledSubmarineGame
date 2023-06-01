using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class SliderUI : MonoBehaviour
{
    private Slider slider;

    private void Awake()
    {
        if (!slider) slider = GetComponent<Slider>();
    }

    public void SetMaxValue(float amount) => slider.maxValue = amount;
    public void SetMaxValue(int amount) => slider.maxValue = amount;

    public void SetValue(float amount) => slider.value = amount;
    public void SetValue(int amount) => slider.value = amount;
}