using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected TypeOfItems ItemType;
    [SerializeField] protected string Name;
    [SerializeField] protected Sprite Sprite;
    [SerializeField] protected int Level;

    [SerializeField] protected Animator ItemAnimator;

    public event UnityAction<bool> Taked;

    public enum TypeOfItems
    {
        heal,
        weapon,
        armor,
    }

    public void TakeItem()
    {
        Taked?.Invoke(true);
    }  

    public void TakeAnimation()
    {
        ItemAnimator.SetTrigger("Take");
    }

    public TypeOfItems GetItemType()
    {
        return ItemType;
    }

    public void SetLevel(int level)
    {
        Level = level;
    }
}