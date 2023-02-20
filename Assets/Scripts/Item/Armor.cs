using UnityEngine;
using UnityEngine.Events;

public class Armor : Item
{
    [SerializeField] private int _health;

    [SerializeField] private TypeArmor _typeArmor;

    public event UnityAction<int> ProtectionChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemType = TypeOfItems.armor;
        CalculateProtection();
    }

    protected override void UpdateParameters()
    {
        base.UpdateParameters();
        CalculateProtection();
    }

    public int CalculateProtection()
    {
        int protection = Value + (Level / 2);
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