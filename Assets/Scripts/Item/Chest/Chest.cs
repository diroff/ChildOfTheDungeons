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

    private void ChooseItem()
    {
        _item = _keyhole.ChooseRandomItem();
    }

    public void TryOpen()
    {
        _keyhole.Open();
    }

    public Item PullItem()
    {
        return _item;
    }
}
