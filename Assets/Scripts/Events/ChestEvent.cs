using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChestEvent : Event
{
    [SerializeField] private FreeItem _freeItemEvent;
    [SerializeField] private EventsController _eventsController;

    private Chest _chest;
    private Item _item;

    public override void StartEvent()
    {
        base.StartEvent();
        Spawner.SpawnChest();
        _chest = Spawner.GetChest();
    }

    public void OpenChest()
    {
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
