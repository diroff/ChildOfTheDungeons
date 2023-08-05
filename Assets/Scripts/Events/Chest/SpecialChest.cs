using UnityEngine;

public class SpecialChest : ChestEvent
{
    [SerializeField] protected ChestEvent ChestEvent;

    protected Chest Chest;

    public override void StartEvent()
    {
        Chest = SpecialChest;
        EventsController.SetContinue(false);
        EventsController.SetEvent(ChestEvent);
        ChestEvent.SetChest(Chest);

        if (_hasSpecialLoot)
            ChestEvent.SetSpecialLoot(_specialLoot);

        ChestEvent.StartEvent();
        EndEvent();
    }
}