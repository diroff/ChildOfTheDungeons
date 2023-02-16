using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ItemDescriptionSlot : Slot
{
    protected Item Item;

    public virtual void AddItem(Item item)
    {
        Item = item;
        IsFilled = true;
        InfoPanel.ShowInfo(false);
        UpdateSprite(Item.ItemSprite);
    }

    public override void ShowDescription()
    {
        base.ShowDescription();

        if (Item != null)
            InfoPanel.SetInfo(Item.ItemDescription);
    }     
}