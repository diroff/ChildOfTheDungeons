using UnityEngine;
using static UnityEditor.Progress;

public class ArmorSlot : Slot
{
    private Armor _armor;

    public override void AddItem(Item item)
    {
        base.AddItem(item);
        _armor = Item as Armor;
    }

    public override void ShowDescription()
    {
        base.ShowDescription();

        if(SlotIsFilled())
            InfoPanel.SetInfo(_armor.ItemDescription, _armor.CalculateProtection(), _armor.GetLevel());
    }
}