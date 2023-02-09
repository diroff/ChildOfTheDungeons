using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{
    [Header("Items")]
    [SerializeField] private ArmorSlots _armorSlots;
    [SerializeField] private WeaponSlot _weaponSlot;

    [SerializeField] private Inventory _inventory;

    [Space]
    [SerializeField] private int _experienceToNextLevel = 10;

    private int _potionCount;
    private int _currentExperience = 0;

    public int PotionCount => _potionCount;

    public Inventory Inventory => _inventory;

    public event UnityAction Leaved;
    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> DamageChanged;
    public event UnityAction<int, int> ExperienceChanged;
    public event UnityAction<int> LevelChanged;

    protected override void Start()
    {
        base.Start();
        UpdatePlayerStats();
    }

    public override void TakeDamage(int damage)
    {
        base.TakeDamage(damage);
        HealthChanged(CurrentHealth);
    }

    public void Heal()
    {
        if (PotionChecker())
        {
            if (CurrentHealth >= MaxHealth / 2)
                CurrentHealth = MaxHealth;
            else
                CurrentHealth += MaxHealth / 2;

            _potionCount--;
            HealthChanged(CurrentHealth);
        }
    }

    public bool PotionChecker()
    {
        return _potionCount > 0;
    }

    public void AddHeal()
    {
        _potionCount++;
    }

    public void AddExperience(int countExperience)
    {
        _currentExperience += countExperience;
        ExperienceChanged(_currentExperience, _experienceToNextLevel);
        LevelUp();
    }

    private bool IsEnoughExperience()
    {
        return _currentExperience >= _experienceToNextLevel;
    }

    public void LevelUp()
    {
        if (IsEnoughExperience())
        {
            Level++;
            _experienceToNextLevel *= (Level + 5) / 2;
            UpdatePlayerStats();
            FillHealth();
        }
    }

    private void FillHealth()
    {
        CurrentHealth = MaxHealth;
        HealthChanged(CurrentHealth);
    }

    public void AddArmor(Armor armor)
    {
        _armorSlots.AddItem(armor);
        UpdatePlayerStats();
    }

    public void UseWeapon(Weapon weapon)
    {
        _weaponSlot.AddItem(weapon);
    }

    public void Run()
    {
        FighterAnimator.SetTrigger("Run");
    }

    public override void Dead()
    {
        base.Dead();
    }

    public void TryToLeave()
    {
        Leave();
    }

    public void Leave()
    {
        FighterAnimator.SetTrigger("Leave");
        Leaved?.Invoke();
    }

    public int CalculateTotalDamage()
    {
        if (_weaponSlot.IsSomeWeapon())
            return (BaseDamage * Level) + _weaponSlot.Weapon.CalculateDamage();
        
        return BaseDamage * Level;
    }

    private void UpdatePlayerStats()
    {
        CalculateMaxHealth();
        CalculateTotalDamage();
        Armor = _armorSlots.CalculateArmor();
        HealthChanged(CurrentHealth);
        DamageChanged(BaseDamage * Level);
        ExperienceChanged(_currentExperience, _experienceToNextLevel);
        LevelChanged(Level);

        Debug.Log($"Броня: {Armor}");
    }
}