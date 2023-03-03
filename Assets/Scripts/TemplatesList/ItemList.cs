using System.Collections.Generic;
using System.Data;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private List<Item> _itemTemplates;
    [SerializeField] private ProgressionController _progression;
    [SerializeField] private int _additionalItemLevel = 0;

    private List<Item> _availableItems = new List<Item>();

    private Item _lastItem;
    private int _lastLevel = 0;

    private void Awake()
    {
        if (_progression == null)
            _progression = FindObjectOfType<ProgressionController>();

        UpdateItemList(0);
    }

    private void OnEnable()
    {
        _progression.Player.LevelChanged.AddListener(UpdateItemList);
        UpdateItemList(_progression.Player.GetLevel());
    }

    public Item TakeItem()
    {
        Item item;
        item = _availableItems[ItemNumber()];

        while (_progression.LastItem == item || _lastItem == item) 
        {
            if (_availableItems.Count <= 1)
                break;

            item = _availableItems[ItemNumber()];
        }

        _lastItem = item;
        _progression.SetLastItem(item);

        return item;
    }

    private void UpdateItemList(int level)
    {
        if (_lastLevel == level)
            return;

        if (_availableItems.Count > 0)
            _availableItems.Clear();

        foreach (Item item in _itemTemplates)
        {
            if(item.MinimalItemLevel == _progression.Player.GetLevel() + _additionalItemLevel || item.IsConsumable)
                _availableItems.Add(item);
        }

        _lastLevel = level;
    }

    private int ItemNumber()
    {
        int itemNumber;
        itemNumber = Random.Range(0, _availableItems.Count);
        return itemNumber;
    }
}