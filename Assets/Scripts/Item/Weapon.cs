using UnityEngine;
using UnityEngine.Events;

public class Weapon : Item
{
    [SerializeField] private int _health;

    public event UnityAction<float> DamageChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        ItemType = TypeOfItems.weapon;
        CalculateDamage();
    }

    public float CalculateDamage()
    {
        float damage = CalculateValue();
        DamageChanged?.Invoke(damage);
        return damage;
    }
}