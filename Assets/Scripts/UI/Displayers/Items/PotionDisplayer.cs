using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PotionDisplayer : Displayer
{
    private void OnEnable()
    {
        Player.Inventory.PotionCountChanged += DisplayParameter;
    }

    private void OnDisable()
    {
        Player.Inventory.PotionCountChanged -= DisplayParameter;
    }
}