using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Fighter
{
    [Header("Additional parameters")]
    [SerializeField] private int _baseExperience;
    [SerializeField] private List<Item> _lootList;
    [SerializeField] private int _additionalLootChange = 0;
    [SerializeField] private int _minimalLevel = 1;

    private int lootChange;
    private Item _lootItem;

    public UnityEvent<float, float> HealthChanged;
    public UnityEvent<int> LevelChanged;
    public UnityEvent<float> DamageChanged;

    public Item LootItem => _lootItem;
    public int MinimalLevel => _minimalLevel;

    protected override void Start()
    {
        base.Start();
        UpdateParameters();
        SetLootChange();

        if (IsLoot())
            SetLootItem();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        if (!Die())
            HealthChanged.Invoke(CurrentHealth, MaxHealth);
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
        return (_baseExperience * (Level + 2) / 3);
    }

    public float CalculateTotalDamage()
    {
        return BaseDamage * (Level + 1) / 2;
    }
    
    public void UpdateParameters()
    {
        HealthChanged.Invoke(CurrentHealth, MaxHealth);
        LevelChanged.Invoke(Level);
        DamageChanged.Invoke(CalculateTotalDamage());
    }

    public override void SetLevel(int currentLevel)
    {
        base.SetLevel(currentLevel);
    }

    public override void Dead()
    {
        base.Dead();
    }

    private void SetLootItem()
    {
        _lootItem = _lootList[Random.Range(0, _lootList.Count)];
    }

    public bool IsLoot()
    {
        return lootChange > 35;
    }

    private void SetLootChange()
    {
        lootChange = Random.Range(0, 100) + _additionalLootChange;
    }
}