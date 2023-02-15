using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    [SerializeField] private KeyType _typeOfKey;

    public KeyType TypeOfKey => _typeOfKey;

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemType = TypeOfItems.key;
    }

    public enum KeyType
    {
        silver,
        gold
    }
}