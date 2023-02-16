using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConsumableItemsDescription : ItemDescriptionSlot
{
    [SerializeField] protected Item SlotItem;

    private void OnEnable()
    {
        AddItem();
    }

    private void AddItem()
    {
        Item item = Instantiate(SlotItem);
        Item = SlotItem;
        IsFilled = true;
        InfoPanel.ShowInfo(false);
        Destroy(item.gameObject);
    }
}