using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEvent : Event
{
    [SerializeField] private Player _player;
    [SerializeField] private GameObject _takePanel;

    private Item _item;
    private Chest _chest;

    public override void StartEvent()
    {
        base.StartEvent();
        Spawner.SpawnChest();
        _chest = Spawner.GetChest();
    }

    public void OpenChest()
    {
        PrepareItem();
    }

    public void PrepareItem()
    {
        _chest.TryOpen();
        _item = _chest.PullItem();
        Spawner.SpawnChestItem(_item);
        SetPanelState(false);
        Destroy(_chest.gameObject);
        _takePanel.SetActive(true);
    }

    public void TakeItem()
    {
        if (_item.GetItemType() == Item.TypeOfItems.heal)
            _player.AddHeal();
        else
            UseItem();

        EndEvent();
        Destroy(_item.gameObject);
    }

    public void NotTakeItem()
    {
        Destroy(_item.gameObject);
        EndEvent();
    }

    private void UseItem()
    {
        switch (_item.GetItemType())
        {
            case Item.TypeOfItems.weapon:
                _player.UseWeapon(_item.GetComponent<Weapon>());
                break;
            case Item.TypeOfItems.armor:
                _player.AddArmor(_item.GetComponent<Armor>());
                break;
        }
    }

    public void IgnoreChest()
    {
        Destroy(_chest.gameObject);
        EndEvent();
    }

    public override void EndEvent()
    {
        _takePanel.SetActive(false);
        base.EndEvent();
    }
}
