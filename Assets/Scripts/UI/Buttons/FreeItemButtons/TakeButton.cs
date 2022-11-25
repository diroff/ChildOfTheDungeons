using UnityEngine;
using static UnityEditor.Progress;

public class TakeButton : ButtonAction
{
    [SerializeField] private Spawner _spawner;
    [SerializeField] private Player _player;

    public override void OnClickAction()
    {
        AddItem();
        Destroy(TakeItem().gameObject);
    }

    private Item TakeItem()
    {
        return _spawner.GetItem();
    }

    private void AddItem()
    {
        var item = TakeItem();
        item.TakeItem();

        if (item.GetItemType() == Item.TypeOfItems.heal)
            _player.AddHeal();
        else
            UseItem();
    }

    private void UseItem()
    {
        var itemType = TakeItem().GetItemType();

        switch (itemType)
        {
            case Item.TypeOfItems.weapon:
                _player.UseWeapon(TakeItem().GetComponent<Weapon>());
                break;
            case Item.TypeOfItems.armor:
                _player.UseArmor(TakeItem().GetComponent<Armor>());
                break;
        }
    }
}