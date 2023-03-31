using UnityEngine;

public class ConsumableItemsDescription : ItemDescriptionSlot
{
    [SerializeField] protected Item SlotItem;

    private void OnEnable()
    {
        AddItem(SlotItem);
    }
}