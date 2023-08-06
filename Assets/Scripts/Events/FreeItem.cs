using System.Collections;
using UnityEngine;

public class FreeItem : Event
{
    [SerializeField] protected EventsController EventsController;
    [SerializeField] private ProgressionController _progression;
    [SerializeField] private Continue _continue;

    [SerializeField] private float _takeCouldown = 1.0f;

    [Header("Info Panels")]
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _infoButton;

    [Header("Special item")]
    [SerializeField] protected Item SpecialItem;

    private Item _item;

    public override void StartEvent()
    {
        base.StartEvent();

        if (Spawner.gameObject == null)
            Spawner = RoomController.GetRoomSpawner();

        if (SpecialItem != null)
            SpawnItem(SpecialItem);

        if (_item == null)
            SpawnItem();

        _infoButton.SetActive(true);
    }

    public void SetItem(Item item)
    {
        SpecialItem = item;
    }

    public void AddItem()
    {
        StartCoroutine(AddItemCoroutine());
    }

    private IEnumerator AddItemCoroutine()
    {
        SetPanelState(false);
        SetInfoPanelState(false);

        _item.TakeAnimation();
        yield return new WaitForSeconds(_takeCouldown);
        
        TakeItem();
    }

    private void UseItem()
    {
        switch (_item.GetItemType())
        {
            case Item.TypeOfItems.weapon:
                Player.UseWeapon(_item.GetComponent<Weapon>());
                break;
            case Item.TypeOfItems.armor:
                Player.AddArmor(_item.GetComponent<Armor>());
                break;
            case Item.TypeOfItems.key:
                Player.Inventory.AddKey(_item.GetComponent<Key>().TypeOfKey);
                break;
        }
    }

    private void TakeItem()
    {
        _item.TakeItem();

        if (_item.GetItemType() == Item.TypeOfItems.heal)
            Player.AddHeal();
        else
            UseItem();

        Destroy(_item.gameObject);
    }

    public void LeaveItem()
    {
        EventsController.SetContinue(true);
        SetPanelState(false);
        EndEvent();
        Destroy(_item);

        _item.TakeItem();
    }

    private void IsTaked(bool isTaked)
    {
        if (isTaked) 
            EndEvent();
    }

    public void SpawnItem(Item item = null)
    {
        Spawner = RoomController.GetRoomSpawner();
        Spawner.SpawnItem(item);
        _item = Spawner.GetItem();
        _item.Taked.AddListener(IsTaked);
    }

    private void SetInfoPanelState(bool isEnable)
    {
        _infoPanel.SetActive(isEnable);
        _infoButton.SetActive(isEnable);
    }

    public override void EndEvent()
    {
        _infoButton.SetActive(false);
        _infoPanel.SetActive(false);
        SpecialItem = null;
        base.EndEvent();
    }
}