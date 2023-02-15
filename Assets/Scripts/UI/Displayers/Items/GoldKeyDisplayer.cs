using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GoldKeyDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Inventory.GoldKeyChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.Inventory.GoldKeyChanged -= DisplayParameter;
    }
}