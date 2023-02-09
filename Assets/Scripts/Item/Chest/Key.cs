using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    [SerializeField] private KeyType _typeOfKey;

    public KeyType TypeOfKey => _typeOfKey;

    private void Awake()
    {
        ItemType = TypeOfItems.key;
    }

    public enum KeyType
    {
        silver,
        gold
    }
}