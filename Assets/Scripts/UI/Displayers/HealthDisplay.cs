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

    private void DisplayParameter(float currentValue, float maxValue)
    {
        TextField.text = string.Format("{0:f0}", currentValue);
        _healthSlider.value = (float)currentValue / maxValue;
    }
}
