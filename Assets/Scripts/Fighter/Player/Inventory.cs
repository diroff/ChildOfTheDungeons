using UnityEngine;
using UnityEngine.Events;

public class Inventory : MonoBehaviour
{
    [SerializeField] private int _silverKeyCount = 1;
    [SerializeField] private int _goldKeyCount = 1;
    [SerializeField] private int _potionCount = 1;

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

    public void AddKey(Key.KeyType _keyType)
    {
        if (_keyType == Key.KeyType.silver)
            _silverKeyCount++;
        else if (_keyType == Key.KeyType.gold)
            _goldKeyCount++;

        SilverKeyChanged?.Invoke(_silverKeyCount);
        GoldKeyChanged?.Invoke(_goldKeyCount);
    }

    public void UseKey(Key.KeyType _keyType)
    {
        if (_keyType == Key.KeyType.silver)
            _silverKeyCount--;
        else if (_keyType == Key.KeyType.gold)
            _goldKeyCount--;

        SilverKeyChanged?.Invoke(_silverKeyCount);
        GoldKeyChanged?.Invoke(_goldKeyCount);
    }

    public void SpendPotion()
    {
        _potionCount--;
        PotionCountChanged?.Invoke(_potionCount);
    }

    public void AddPotion()
    {
        _potionCount++;
        PotionCountChanged?.Invoke(_potionCount);
    }

    public bool IsEnoughPotion()
    {
        return _potionCount > 0;
    }
}