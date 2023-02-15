using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptionSlot : Slot
{
    [SerializeField] private Item _slotItem;

    private void OnEnable()
    {
        AddItem();
    }

    private void AddItem()
    {
        Item = _slotItem;
        IsFilled = true;
        InfoPanel.ShowInfo(false);
    }
}