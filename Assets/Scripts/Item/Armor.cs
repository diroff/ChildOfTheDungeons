using UnityEngine;
using UnityEngine.Events;

public class Armor : Item
{
    [SerializeField] private int _health;
    [SerializeField] private TypeArmor _typeArmor;

    public event UnityAction<float> ProtectionChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemType = TypeOfItems.armor;
        CalculateProtection();
    }

    public float CalculateProtection()
    {
        float protection = CalculateValue();
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