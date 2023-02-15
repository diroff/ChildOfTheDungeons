using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Endurance.ValueChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.Skills.Endurance.ValueChanged -= DisplayParameter;
    }
}