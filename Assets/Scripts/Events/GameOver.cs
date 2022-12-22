using UnityEngine;

public class GameOver : Event
{
    public override void StartEvent()
    {
        Debug.Log("”ра, конец игры");
        base.StartEvent();
    }

    public override void EndEvent()
    {
        base.EndEvent();
    }
}