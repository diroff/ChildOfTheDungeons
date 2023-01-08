using UnityEngine;
using UnityEngine.Events;

public class Weapon : Item
{
    [SerializeField] private int _health;
    [SerializeField] private int _damage;

    public event UnityAction<int> DamageChanged;

    protected override void OnEnable()
    {
        base.OnEnable();
        CalculateDamage();
    }

    protected override void UpdateParameters()
    {
        base.UpdateParameters();
        CalculateDamage();
    }

    public int CalculateDamage()
    {
        int damage = _damage + (Level / 2);
        DamageChanged?.Invoke(damage);
        return damage;
    }
}