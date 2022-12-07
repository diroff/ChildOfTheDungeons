using UnityEngine;

public class FreeItem : Event
{
    [SerializeField] private Player _player;

    private Item _item;

    public override void DoEventSteps()
    {
        base.DoEventSteps();
        Spawner.SpawnItem();
        _item = Spawner.GetItem();
        SetItemLevel();
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

    private void SetItemLevel()
    {
        if (_item.GetItemType() != Item.TypeOfItems.heal)
        {
            _item.SetLevel(Random.Range(_player.GetLevel(), _player.GetLevel() + 2));
        }
    }
}