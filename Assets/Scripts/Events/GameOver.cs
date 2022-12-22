using UnityEngine;

public class GameOver : Event
{
    public override void DoEventSteps()
    {
        Debug.Log("”ра, конец игры");
        base.DoEventSteps();
    }

    public override void EndEvent()
    {
        base.EndEvent();
    }
}