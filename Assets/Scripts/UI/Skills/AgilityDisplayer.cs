using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Agility.ValueChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.Skills.Agility.ValueChanged -= DisplayParameter;
    }
}