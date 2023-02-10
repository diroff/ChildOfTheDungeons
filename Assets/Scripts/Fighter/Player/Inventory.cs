using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _silverKeyCount = 1;
    [SerializeField] private int _goldKeyCount = 1;

    private int _potionCount;

    public int PotionCount => _potionCount;

    public int SilverKeyCount => _silverKeyCount;
    public int GoldKeyCount => _goldKeyCount;

    public void AddKey(Key.KeyType _keyType)
    {
        if (_keyType == Key.KeyType.silver)
            _silverKeyCount++;
        else if (_keyType == Key.KeyType.gold)
            _goldKeyCount++;
    }

    public void UseKey(Key.KeyType _keyType)
    {
        if (_keyType == Key.KeyType.silver)
            _silverKeyCount--;
        else if (_keyType == Key.KeyType.gold)
            _goldKeyCount--;
    }

    public void SpendPotion()
    {
        _potionCount--;
    }

    public void AddPotion()
    {
        _potionCount++;
    }

    public bool IsEnoughPotion()
    {
        return _potionCount > 0;
    }
}
