using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Enemy : Fighter
{
    [SerializeField] private int _baseExperience;

    [SerializeField] private OppositeParameters _parameters;

    [SerializeField] private List<Item> _lootList;

    [SerializeField] private int _additionalLootChange = 0;

    private int lootChange;
    private Item _lootItem;

    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> LevelChanged;
    public event UnityAction<int> DamageChanged;
    public event UnityAction<int> CostChanged;

    public Item LootItem => _lootItem;

    private void OnEnable()
    {
        _parameters.DisplayParameters(true);
    }

    private void OnDisable()
    {
        _parameters.DisplayParameters(false);
    }

    protected override void Start()
    {
        base.Start();
        EnableShadowShader();
        UpdateParameters();
        SetLootChange();

        if (IsLoot())
            SetLootItem();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        
        if (!Die())
            HealthChanged(CurrentHealth);
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

    public int CalculateTotalDamage()
    {
        return BaseDamage * (Level + 1) / 2;
    }
    
    private void UpdateParameters()
    {
        HealthChanged(CurrentHealth);
        LevelChanged(Level);
        DamageChanged(CalculateTotalDamage());
        CostChanged(CalculateExperienceCost());
    }

    public override void SetLevel(int currentLevel)
    {
        base.SetLevel(currentLevel);
    }

    public override void Dead()
    {
        _parameters.DisplayParameters(false);
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

    private void EnableShadowShader()
    {
        GetComponent<Renderer>().shadowCastingMode = UnityEngine.Rendering.ShadowCastingMode.On;
        GetComponent<Renderer>().receiveShadows = true;
    }
}