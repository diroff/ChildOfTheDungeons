using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : Displayer
{
    [SerializeField] private Slider _healthSlider;

    private void OnEnable()
    {
        Player.HealthChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.HealthChanged -= DisplayParameter;
    }

    private void DisplayParameter(int currentValue, int maxValue)
    {
        TextField.text = $"{currentValue}";
        _healthSlider.value = (float)currentValue / maxValue;
    }
}
