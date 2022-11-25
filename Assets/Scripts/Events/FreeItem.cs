using UnityEngine;

public class FreeItem : Event
{
    private Item _item;

    public override void DoEventSteps()
    {
        base.DoEventSteps();
        Spawner.SpawnItem();
        _item = Spawner.GetItem();
        _item.Taked += IsTaked;
    }

    public override void EndEvent()
    {
        base.EndEvent();
        _item.Taked -= IsTaked;
    }

    private void IsTaked(bool isTaked)
    {
        if (isTaked) 
            EndEvent();
    }
}