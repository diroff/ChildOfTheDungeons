using System.Collections.Generic;
using UnityEngine;

public class Keyhole : MonoBehaviour
{
    [SerializeField] private List<Item> _items;

    [SerializeField] private Key _requriedKey;

    private bool _isOpen = false;

    public bool IsOpen => _isOpen;
    public Key RequriedKey => _requriedKey;

    public Item ChooseRandomItem()
    {
        return _items[Random.Range(0, _items.Count)];
    }

    public void Open()
    {
        _isOpen = true;
    }

}
