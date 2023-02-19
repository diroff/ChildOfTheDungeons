using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyInfoSlot : Slot
{
    private void OnEnable()
    {
        IsFilled = true;
    }

    public override void ShowDescription()
    {
        base.ShowDescription();
        gameObject.SetActive(false);
    }
}
