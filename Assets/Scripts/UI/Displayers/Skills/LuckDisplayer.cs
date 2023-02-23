using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class LuckDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Skills.Luck.ValueChanged.AddListener(DisplayParameter);
    }
}