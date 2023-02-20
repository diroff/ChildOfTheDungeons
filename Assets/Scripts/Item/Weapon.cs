using UnityEngine;
using UnityEngine.Events;

public class Weapon : Item
{
    [SerializeField] private int _health;

    public event UnityAction<int> DamageChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemType = TypeOfItems.weapon;
        CalculateDamage();
    }

    protected override void UpdateParameters()
    {
        base.UpdateParameters();
        CalculateDamage();
    }

    public int CalculateDamage()
    {
        int damage = CalculateValue();
        DamageChanged?.Invoke(damage);
        return damage;
    }
}