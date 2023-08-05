using UnityEngine;

public class Chest : MonoBehaviour
{
    [SerializeField] private GameObject _chest;
    [SerializeField] private Keyhole _keyhole;
    [SerializeField] private Animator _animator;
    
    private Item _item;

    public Keyhole KeyHole => _keyhole;
    public Animator Animator => _animator;

    private void Start()
    {
        ChooseItem();
    }

    public void ChooseItem()
    {
        if(_item == null)
            _item = _keyhole.ChooseRandomItem();
    }

    public void TryOpen()
    {
        _keyhole.Open();
    }

    public void SetItem(Item item)
    {
        _item = item;
    }

    public Item PullItem()
    {
        return _item;
    }
}