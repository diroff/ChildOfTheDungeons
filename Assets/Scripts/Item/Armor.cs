using UnityEngine;
using UnityEngine.Events;

public class Armor : Item
{
    [SerializeField] private int _health;
    [SerializeField] private int _protection;

    [SerializeField] private TypeArmor _typeArmor;

    public event UnityAction<int> ProtectionChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemType = TypeOfItems.armor;
        Sprite = GetComponent<SpriteRenderer>().sprite;
        CalculateProtection();
    }

    protected override void UpdateParameters()
    {
        base.UpdateParameters();
        CalculateProtection();
    }

    public int CalculateProtection()
    {
        int protection = _protection + (Level / 2);
        ProtectionChanged?.Invoke(protection);
        return protection;
    }

    public TypeArmor GetTypeArmor()
    {
        return _typeArmor;
    }

    public enum TypeArmor
    {
        helm,
        costume,
        shoes
    }
}