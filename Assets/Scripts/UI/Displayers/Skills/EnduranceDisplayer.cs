using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnduranceDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Endurance.ValueChanged.AddListener(DisplayParameter);
    }
}