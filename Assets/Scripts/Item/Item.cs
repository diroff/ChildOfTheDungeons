using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected TypeOfItems ItemType;
    [SerializeField] protected string Name;

    public event UnityAction<bool> Taked;

    public enum TypeOfItems
    {
        heal,
        helm,
        armor,
        pants,
        bots
    }

    public void TakeItem()
    {
        Taked?.Invoke(true);
    }

    public TypeOfItems GetItemType()
    {
        return ItemType;
    }
}