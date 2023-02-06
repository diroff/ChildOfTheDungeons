using UnityEngine;
using UnityEngine.Events;

public abstract class Item : MonoBehaviour
{
    [SerializeField] protected string Name;
    
    [SerializeField] protected Animator ItemAnimator;
    
    [SerializeField] private OppositeParameters _parameters;

    public Sprite ItemSprite => Sprite;

    protected TypeOfItems ItemType = TypeOfItems.heal;
    protected int Level = 1;
    protected Sprite Sprite;

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
        Taked?.Invoke(true);
    }  

    public void TakeAnimation()
    {
        _parameters.DisplayParameters(false);
        ItemAnimator.SetTrigger("Take");
    }

    public void HideUI()
    {
        _parameters.DisplayParameters(false);
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