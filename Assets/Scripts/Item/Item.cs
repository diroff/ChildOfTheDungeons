using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] protected string Name;
    [TextArea(1, 2)]
    [SerializeField] protected string Description;

    [SerializeField] protected int Value;
    [SerializeField] protected bool Consumable = false;
    [SerializeField] protected Animator ItemAnimator;
    [SerializeField] protected Sprite Sprite;

    protected SpriteRenderer SpriteRenderer;
    protected TypeOfItems ItemType = TypeOfItems.heal;
    protected int Level = 1;

    public int ItemValue => Value;
    public bool IsConsumable => Consumable;
    public Sprite ItemSprite => Sprite;
    public string ItemDescription => Description;

    public UnityEvent<bool> Taked;
    public UnityEvent<int> LevelChanged;

    protected virtual void OnEnable()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        LevelChanged.Invoke(Level);
        SpriteRenderer.sprite = Sprite;
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
        key
    }

    public int CalculateValue()
    {
        return Value + (Level / 2);
    }

    public void TakeItem()
    {
        Taked.Invoke(true);
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
        UpdateParameters();
    }

    public int GetLevel()
    {
        return Level;
    }
}