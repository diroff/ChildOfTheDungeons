using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _silverKeyCount;
    [SerializeField] private int _goldKeyCount;
    [SerializeField] private int _potionCount;

    public event UnityAction<int> SilverKeyChanged;
    public event UnityAction<int> GoldKeyChanged;
    public event UnityAction<int> PotionCountChanged;

    public int PotionCount => _potionCount;
    public int SilverKeyCount => _silverKeyCount;
    public int GoldKeyCount => _goldKeyCount;

    private void Start()
    {
        SilverKeyChanged?.Invoke(_silverKeyCount);
        GoldKeyChanged?.Invoke(_goldKeyCount);
        PotionCountChanged?.Invoke(_potionCount);
    }

    public void AddKey(Key.KeyType _keyType, int count = 1)
    {
        if (_keyType == Key.KeyType.silver)
            _silverKeyCount += count;
        else if (_keyType == Key.KeyType.gold)
            _goldKeyCount += count;

        SilverKeyChanged?.Invoke(_silverKeyCount);
        GoldKeyChanged?.Invoke(_goldKeyCount);
    }

    public void UseKey(Key.KeyType _keyType, int count = 1)
    {
        if (_keyType == Key.KeyType.silver)
            _silverKeyCount -= count;
        else if (_keyType == Key.KeyType.gold)
            _goldKeyCount -= count;

        SilverKeyChanged?.Invoke(_silverKeyCount);
        GoldKeyChanged?.Invoke(_goldKeyCount);
    }

    public void SpendPotion(int count = 1)
    {
        _potionCount -= count;
        PotionCountChanged?.Invoke(_potionCount);
    }

    public void AddPotion(int count = 1)
    {
        _potionCount += count;
        PotionCountChanged?.Invoke(_potionCount);
    }

    public bool IsEnoughPotion()
    {
        return _potionCount > 0;
    }
}