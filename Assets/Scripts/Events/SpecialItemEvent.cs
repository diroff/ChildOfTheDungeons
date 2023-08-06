using UnityEngine;

public class SpecialItemEvent : FreeItem
{
    [SerializeField] private FreeItem _freeItemEvent;

    public override void StartEvent()
    {
        EventsController.SetContinue(false);
        EventsController.SetEvent(_freeItemEvent);

        _freeItemEvent.SetItem(SpecialItem);
        EndEvent();
       _freeItemEvent.StartEvent();
    }
}