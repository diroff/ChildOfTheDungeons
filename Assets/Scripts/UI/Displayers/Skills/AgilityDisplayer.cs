using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AgilityDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Agility.ValueChanged.AddListener(DisplayParameter);
    }
}