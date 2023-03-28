using System.Collections;
using UnityEngine;

public class FreeItem : Event
{
    [SerializeField] private ProgressionController _progression;
    [SerializeField] private float _takeCouldown = 1.0f;

    [Header("Info Panels")]
    [SerializeField] private GameObject _infoPanel;
    [SerializeField] private GameObject _infoButton;

    private Item _item;

    public override void StartEvent()
    {
        base.StartEvent();

        if (Spawner.gameObject == null)
            Spawner = RoomController.GetRoomSpawner();

        if (_item == null)
            SpawnItem();

        _infoButton.SetActive(true);
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
        SetInfoPanelState(false);

        _item.TakeAnimation();
        yield return new WaitForSeconds(_takeCouldown);
        
        TakeItem();
    }

    private IEnumerator LeaveItemCoroutine()
    {
        SetPanelState(false);
        SetInfoPanelState(false);
        Player.Leave();
        yield return new WaitForSeconds(_takeCouldown);

        DestroySpawnerObjects();
        NotTakeItem();
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

    private void NotTakeItem()
    {
        _item.TakeItem();

        Destroy(_item.gameObject);
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

    private void DestroySpawnerObjects()
    {
        if (Spawner.GetEnemy() != null)
            Destroy(Spawner.GetEnemy().gameObject);

        if (Spawner.GetChest() != null)
            Destroy(Spawner.GetChest().gameObject);
    }

    private void SetInfoPanelState(bool isEnable)
    {
        _infoPanel.SetActive(isEnable);
        _infoButton.SetActive(isEnable);
    }
}