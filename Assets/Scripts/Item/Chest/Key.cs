using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Key : Item
{
    [SerializeField] private KeyType _key;

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