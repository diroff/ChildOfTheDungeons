using System.Collections;
using UnityEngine;

public class FreeItem : Event
{
    [SerializeField] private Player _player;

    [SerializeField] private ProgressionController _progression;

    [SerializeField] private float _takeCouldown = 1.0f;

    private Item _item;

    public override void StartEvent()
    {
        base.StartEvent();
        SpawnItem();
    }

    public void AddItem()
    {
        StartCoroutine(AddItemCoroutine());
    }

    public void LeaveItem()
    {
        StartCoroutine(LeaveItemCoroutine());
    }

    private IEnumerator AddItemCoroutine()
    {
        SetPanelState(false);

        _item.TakeAnimation();
        yield return new WaitForSeconds(_takeCouldown);

        TakeItem();
    }

    private IEnumerator LeaveItemCoroutine()
    {
        SetPanelState(false);
        _player.Leave();
        yield return new WaitForSeconds(_takeCouldown);
        
        NotTakeItem();
    }

    private void UseItem()
    {
        switch (_item.GetItemType())
        {
            case Item.TypeOfItems.weapon:
                _player.UseWeapon(_item.GetComponent<Weapon>());
                break;
            case Item.TypeOfItems.armor:
                _player.UseArmor(_item.GetComponent<Armor>());
                break;
        }
    }

    private void TakeItem()
    {
        _item.TakeItem();

        if (_item.GetItemType() == Item.TypeOfItems.heal)
            _player.AddHeal();
        else
            UseItem();

        Destroy(_item.gameObject);
    }

    private void NotTakeItem()
    {
        _item.TakeItem();

        Destroy(_item.gameObject);
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

    private void SpawnItem()
    {
        Spawner.SpawnItem();
        _item = Spawner.GetItem();
        SetItemLevel();
        _item.Taked += IsTaked;
    }

    private void SetItemLevel()
    {
        if (_item.GetItemType() != Item.TypeOfItems.heal)
            _item.SetLevel(_progression.SetLevel());
    }
}