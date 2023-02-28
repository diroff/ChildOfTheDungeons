using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class ChestEvent : Event
{
    [SerializeField] private FreeItem _freeItemEvent;
    [SerializeField] private Button _openButton;
    [SerializeField] private SpriteRenderer _keySprite;
    [SerializeField] private Player _player;
    [SerializeField] protected Chest SpecialChest;
    [SerializeField] protected EventsController EventsController;

    [SerializeField] private float _timeBeforeLeave;
    [SerializeField] private float _timeBeforeOpen;

    private Chest _chest;
    private Item _item;
    private Key.KeyType _keyType;

    public override void StartEvent()
    {
        base.StartEvent();
        SpawnChest(_chest);
        _chest = Spawner.GetChest();
        _keyType = _chest.KeyHole.RequriedKey.TypeOfKey;
        SetOpenButtonState();
    }

    protected virtual void SpawnChest(Chest chest = null)
    {
        if (chest == null)
            Spawner.SpawnChest();
        else
            Spawner.SpawnChest(chest);
    }

    public void SetChest(Chest newChest)
    {
        _chest = newChest;
    }

    private void SetOpenButtonState()
    {
        if(_keyType == Key.KeyType.gold)
        {
            if (_player.Inventory.GoldKeyCount > 0)
                _openButton.interactable = true;
            else
                _openButton.interactable = false;
        }
        else if(_keyType == Key.KeyType.silver)
        {
            if (_player.Inventory.SilverKeyCount > 0)
                _openButton.interactable = true;
            else
                _openButton.interactable = false;
        }

        _keySprite.sprite = _chest.KeyHole.RequriedKey.ItemSprite;
    }

    public void OpenChest()
    {
        StartCoroutine(OpenChestCoroutine());
    }

    public void IgnoreChest()
    {
        StartCoroutine(IgnoreChestCoroutine());
    }

    public Item PrepareItem()
    {
        _chest.TryOpen();
        return _chest.PullItem();
    }

    private IEnumerator OpenChestCoroutine()
    {
        SetPanelState(false);
        _chest.Animator.SetTrigger("Open");
        yield return new WaitForSeconds(_timeBeforeOpen);
        _player.Inventory.UseKey(_keyType);
        _item = PrepareItem();
        EventsController.SetContinue(false);
        EventsController.SetEvent(_freeItemEvent);
        _freeItemEvent.SpawnItem(_item);

        EndEvent();
        EventsController.StartEvent();
    }

    private IEnumerator IgnoreChestCoroutine()
    {
        SetPanelState(false);
        _player.Leave();
        yield return new WaitForSeconds(_timeBeforeLeave);
        Destroy(_chest.gameObject);
        EventsController.SetContinue(true);
        EndEvent();
    }

    public override void EndEvent()
    {
        base.EndEvent();
    }
}