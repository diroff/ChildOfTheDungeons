using UnityEngine;

public class SpecialFight : Fight
{
    [SerializeField] private Fight _mainFight;

    public override void StartEvent()
    {
        SetEnableEvent(true);
        CreatingEnemy();

        EventsController.SetContinue(false);
        EventsController.SetEvent(_mainFight);
        EndEvent();
        _mainFight.StartEvent();
    }
}