using UnityEngine;

public class GameOver : Event
{
    public override void StartEvent()
    {
        Debug.Log("���, ����� ����");
        base.StartEvent();
    }

    public override void EndEvent()
    {
        base.EndEvent();
    }
}