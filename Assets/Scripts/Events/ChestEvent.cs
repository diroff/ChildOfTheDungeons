using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using static Key;

public class ChestEvent : Event
{
    [SerializeField] private FreeItem _freeItemEvent;
    [SerializeField] private EventsController _eventsController;
    [SerializeField] private Button _openButton;
    [SerializeField] private Player _player;

    private Chest _chest;
    private Item _item;
    private Key.KeyType _keyType;

    public override void StartEvent()
    {
        base.StartEvent();
        Spawner.SpawnChest();
        _chest = Spawner.GetChest();
        _keyType = _chest.KeyHole.RequriedKey.TypeOfKey;
        SetOpenButtonState();
    }

    private void SetOpenButtonState()
    {
        if(_keyType == Key.KeyType.gold)
        {
            if (_player.Inventory.GoldKeyCount > 0)
                _openButton.interactable = true;
            else
                _openButton.interactable = false;
        }
        else if(_keyType == Key.KeyType.silver)
        {
            if (_player.Inventory.SilverKeyCount > 0)
                _openButton.interactable = true;
            else
                _openButton.interactable = false;
        }
    }

    public void OpenChest()
    {
        _player.Inventory.UseKey(_keyType);
        _item = PrepareItem();
        _eventsController.SetContinue(false);
        _eventsController.SetEvent(_freeItemEvent);
        _freeItemEvent.SpawnItem(_item);

        EndEvent();
        _eventsController.StartEvent();
    }

    public void IgnoreChest()
    {
        EndEvent();
    }

    public Item PrepareItem()
    {
        _chest.TryOpen();
        return _chest.PullItem();
    }

    public override void EndEvent()
    {
        Destroy(_chest.gameObject);
        base.EndEvent();
    }
}
