using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Fighter
{
    [Header("Additional parameters")]
    [SerializeField] private int _baseExperience;
    [SerializeField] private int _minimalLevel = 1;
    [SerializeField] private bool _isBoss = false;

    [Header("Loot")]
    [SerializeField] private bool _hasLoot = true;
    [SerializeField] private ItemList _lootList;

    protected Sprite StartSprite;
    protected SpriteRenderer SpriteRenderer;

    private Item _lootItem;

    public UnityEvent<float, float> HealthChanged;
    public UnityEvent<int> LevelChanged;
    public UnityEvent<float> DamageChanged;

    public Item LootItem => _lootItem;
    public int MinimalLevel => _minimalLevel;
    public bool IsBoss => _isBoss;

    protected override void Start()
    {
        base.Start();
        UpdateParameters();

        SpriteRenderer = GetComponent<SpriteRenderer>();
        StartSprite = SpriteRenderer.sprite;

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
        return _baseExperience;
    }

    public float CalculateTotalDamage()
    {
        return BaseDamage;
    }
    
    public void UpdateParameters()
    {
        HealthChanged.Invoke(CurrentHealth, MaxHealth);
        LevelChanged.Invoke(Level);
        DamageChanged.Invoke(CalculateTotalDamage());
    }

    public void SetStartSprite()
    {
        SpriteRenderer.sprite = StartSprite;
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
        _lootItem = _lootList.TakeItem();
    }

    public bool IsLoot()
    {
        return _hasLoot;
    }
}