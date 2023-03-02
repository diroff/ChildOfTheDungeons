using System.Collections.Generic;
using UnityEngine;

public class ItemList : MonoBehaviour
{
    [SerializeField] private List<Item> _itemTemplates;
    [SerializeField] private ProgressionController _progression;

    private List<Item> _availableItems = new List<Item>();

    private Item _lastItem;

    private void Awake()
    {
        UpdateItemList(0);
    }

    private void OnEnable()
    {
        _progression.Player.LevelChanged.AddListener(UpdateItemList);
    }

    public Item TakeItem()
    {
        Item item;
        item = _availableItems[ItemNumber()];

        while(_lastItem == item)
            item = _availableItems[ItemNumber()];

        _lastItem = item;
        return item;
    }

    private void UpdateItemList(int level)
    {
        foreach (Item item in _itemTemplates)
        {
            if(item.MinimalItemLevel == _progression.Player.GetLevel())
                _availableItems.Add(item);
        }
    }

    private int ItemNumber()
    {
        int itemNumber;
        itemNumber = Random.Range(0, _availableItems.Count);
        return itemNumber;
    }
}