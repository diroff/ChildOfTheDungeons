using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PowerDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Power.ValueChanged.AddListener(DisplayParameter);
    }
}