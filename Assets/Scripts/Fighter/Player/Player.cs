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
    [SerializeField] private Skills _skills;

    private int _currentExperience = 0;

    public Inventory Inventory => _inventory;
    public WeaponSlot WeaponSlot => _weaponSlot;
    public ArmorSlots ArmorSlots => _armorSlots;
    public Skills Skills => _skills;

    public event UnityAction Leaved;
    public event UnityAction NotLeaved;
    public event UnityAction<float, float> HealthChanged;
    public event UnityAction<int, int> ExperienceChanged;
    public event UnityAction<int> LevelChanged;

    protected override void Start()
    {
        base.Start();
        UpdatePlayerStats();
    }

    public override void TakeDamage(float damage)
    {
        base.TakeDamage(damage);
        
        HealthChanged(CurrentHealth, MaxHealth);
    }

    public void Heal()
    {
        if (PotionChecker())
        {
            if (CurrentHealth >= MaxHealth / 2)
                CurrentHealth = MaxHealth;
            else
                CurrentHealth += (MaxHealth / 2);

            _inventory.SpendPotion();
            HealthChanged(CurrentHealth, MaxHealth);
            FighterAnimator.SetTrigger("Heal");
        }
    }

    public bool PotionChecker()
    {
        return _inventory.IsEnoughPotion();
    }

    public void AddHeal()
    {
        _inventory.AddPotion();
    }

    public void AddExperience(int countExperience)
    {
        _currentExperience += countExperience;
        ExperienceChanged(_currentExperience, _experienceToNextLevel);
        LevelUp();
    }

    public override void CalculateMaxHealth()
    {
        MaxHealth = (BaseMaxHealth + _skills.Endurance.CurrentLevel) * Level;
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
            _currentExperience = 0;
            UpdatePlayerStats();
            FillHealth();
        }
    }

    private void FillHealth()
    {
        CurrentHealth = MaxHealth;
        HealthChanged(CurrentHealth, MaxHealth);
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
        if (IsLeaved())
            Leave();
        else
            NotLeave();
    }

    public void Leave()
    {
        FighterAnimator.SetTrigger("Leave");
        Leaved?.Invoke();
    }

    public void NotLeave()
    {
        FighterAnimator.SetTrigger("NotLeave");
        NotLeaved?.Invoke();
    }

    public float CalculateTotalDamage()
    {
        if (_weaponSlot.IsSomeWeapon())
            return (_skills.Power.CurrentLevel * Level) + _weaponSlot.Weapon.CalculateDamage();
        
        return _skills.Power.CurrentLevel * Level;
    }

    public bool AdditionalChance()
    {
        return (Random.Range(0, 15) + _skills.Agility.CurrentLevel) >= 15;
    }

    public bool IsLeaved()
    {
        return (Random.Range(0, 10) + _skills.Luck.CurrentLevel) >= 7;
    }

    private void UpdatePlayerStats()
    {
        CalculateMaxHealth();
        CalculateTotalDamage();
        Armor = _armorSlots.CalculateArmor();
        HealthChanged(CurrentHealth, MaxHealth);
        ExperienceChanged(_currentExperience, _experienceToNextLevel);
        LevelChanged(Level);
    }
}