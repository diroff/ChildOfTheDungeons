using UnityEngine;
using UnityEngine.Events;

public class Enemy : Fighter
{
    [SerializeField] private int _baseExperience;

    public event UnityAction<int> HealthChanged;

    protected override void Start()
    {
        base.Start();
        HealthChanged(CurrentHealth);
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        if (!Die())
        {
            HealthChanged(CurrentHealth);
        }
    }

    public void TryAttack(Player player)
    {
        if (!Die())
        {
            Attack();
            player.TakeDamage(CalculateTotalDamage());
        }
    }

    public int CalculateExperienceCost()
    {
        return (_baseExperience * (Level+2)/3);
    }

    public int CalculateTotalDamage()
    {
        return BaseDamage * (Level + 1) / 2;
    }
}