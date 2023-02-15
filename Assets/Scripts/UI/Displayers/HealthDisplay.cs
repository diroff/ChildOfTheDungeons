using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthDisplay : Displayer
{
    private void OnEnable()
    {
        Player.HealthChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.HealthChanged -= DisplayParameter;
    }
}
