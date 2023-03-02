using UnityEngine;

public class Keyhole : MonoBehaviour
{
    [SerializeField] private ItemList _items;
    [SerializeField] private Key _requriedKey;

    private bool _isOpen = false;

    public bool IsOpen => _isOpen;
    public Key RequriedKey => _requriedKey;

    public Item ChooseRandomItem()
    {
        return _items.TakeItem();
    }

    public void Open()
    {
        _isOpen = true;
    }
}