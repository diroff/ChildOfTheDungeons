using UnityEngine;
using UnityEngine.Events;

[RequireComponent(typeof(SpriteRenderer))]
public abstract class Item : MonoBehaviour
{
    [SerializeField] protected string Name;
    [TextArea(1, 2)]
    [SerializeField] protected string Description;

    [SerializeField] protected float Value;
    [SerializeField] protected bool Consumable = false;
    [SerializeField] protected Animator ItemAnimator;
    [SerializeField] protected Sprite Sprite;
    [SerializeField] protected int MinimalLevel = 1;

    protected SpriteRenderer SpriteRenderer;
    protected TypeOfItems ItemType = TypeOfItems.heal;

    public string ItemName => Name;
    public float ItemValue => Value;
    public bool IsConsumable => Consumable;
    public Sprite ItemSprite => Sprite;
    public string ItemDescription => Description;
    public int MinimalItemLevel => MinimalLevel;

    public UnityEvent<bool> Taked;

    protected virtual void OnEnable()
    {
        SpriteRenderer = GetComponent<SpriteRenderer>();
        SpriteRenderer.sprite = Sprite;
    }

    public enum TypeOfItems
    {
        heal,
        weapon,
        armor,
        key
    }

    public float CalculateValue()
    {
        return Value;
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

    public void HideItem()
    {
        SpriteRenderer.enabled = false;
    }
}