using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{
    [SerializeField] private Transform _armorSlot;
    [SerializeField] private Transform _weaponSlot;

    [SerializeField] private int _experienceToNextLevel = 10;

    private int _potionCount;
    private int _currentExperience = 0;

    private Armor _currentArmor;
    private Weapon _currentWeapon;

    public int PotionCount => _potionCount;

    public event UnityAction Leaved;
    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> DamageChanged;
    public event UnityAction<int> PotionCountChanged;
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
            PotionCountChanged(_potionCount);
        }
    }

    public bool PotionChecker()
    {
        return _potionCount > 0;
    }

    public void AddHeal()
    {
        _potionCount++;
        PotionCountChanged(_potionCount);
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

    public void UseArmor(Armor newArmor)
    {
        if (_currentArmor != null)
            Destroy(_armorSlot.GetComponentInChildren<Armor>().gameObject);

        _currentArmor = Instantiate(newArmor, _armorSlot);
        CalculateArmor();
    }

    public void UseWeapon(Weapon newWeapon)
    {
        if (_currentWeapon != null)
            Destroy(_weaponSlot.GetComponentInChildren<Weapon>().gameObject);

        _currentWeapon = Instantiate(newWeapon, _weaponSlot);
        DamageChanged(CalculateTotalDamage());
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
        if (_currentWeapon != null)
            return (BaseDamage * Level) + _currentWeapon.CalculateDamage();
        
        return baseDamage * Level;
    }

    public void CalculateArmor()
    {
        if (_currentArmor != null)
            Armor = _currentArmor.CalculateProtection();

        Armor = 0;
    }

    private void UpdatePlayerStats()
    {
        CalculateMaxHealth();
        CalculateTotalDamage();
        CalculateArmor();
        HealthChanged(CurrentHealth);
        DamageChanged(CalculateTotalDamage());
        PotionCountChanged(_potionCount);
        ExperienceChanged(_currentExperience, _experienceToNextLevel);
        LevelChanged(Level);
    }
}