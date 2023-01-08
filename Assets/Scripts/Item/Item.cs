using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected TypeOfItems ItemType;
    [SerializeField] protected string Name;
    [SerializeField] protected Sprite Sprite;
    [SerializeField] protected int Level;

    [SerializeField] protected Animator ItemAnimator;
    
    [SerializeField] private OppositeParameters _parameters;

    public event UnityAction<bool> Taked;
    public event UnityAction<int> LevelChanged;

    protected virtual void OnEnable()
    {
        _parameters.DisplayParameters(true);
        LevelChanged?.Invoke(Level);
    }

    protected virtual void OnDisable()
    {
        _parameters.DisplayParameters(false);
    }

    protected virtual void UpdateParameters()
    {
        LevelChanged?.Invoke(Level);
    }

    public enum TypeOfItems
    {
        heal,
        weapon,
        armor,
    }

    public void TakeItem()
    {
        _parameters.DisplayParameters(false);
        Taked?.Invoke(true);
    }  

    public void TakeAnimation()
    {
        _parameters.DisplayParameters(false);
        ItemAnimator.SetTrigger("Take");
    }

    public TypeOfItems GetItemType()
    {
        return ItemType;
    }

    public void SetLevel(int level)
    {
        Level = level;
        UpdateParameters();
    }

    public int GetLevel()
    {
        return Level;
    }
}