using UnityEngine;

public class Key : Item
{
    [SerializeField] private KeyType _typeOfKey;
    [SerializeField] private Sprite _icon;

    public KeyType TypeOfKey => _typeOfKey;
    public Sprite Icon => _icon;

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