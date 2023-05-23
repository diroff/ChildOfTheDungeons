using UnityEngine;

public class FreeItemSlot : ItemDescriptionSlot
{
    [SerializeField] private Spawner _spawner;

    public override void ShowDescription()
    {
        base.ShowDescription();
        AddItem(_spawner.GetItem());
    }
}