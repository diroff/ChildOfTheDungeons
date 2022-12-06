using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{
    [SerializeField] private Transform _armorSlot;
    [SerializeField] private Transform _weaponSlot;

    private int _potionCount;

    private Armor _currentArmor;
    private Weapon _currentWeapon;

    public int PotionCount => _potionCount;

    public event UnityAction Leaved;
    public event UnityAction<int> HealthChanged;
    public event UnityAction<int> DamageChanged;
    public event UnityAction<int> PotionCountChanged;

    protected override void Start()
    {
        base.Start();
        HealthChanged(CurrentHealth);
        DamageChanged(CalculateTotalDamage());
        PotionCountChanged(_potionCount);
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
        bool isSomething = _potionCount > 0;
        return isSomething;
    }

    public void AddHeal()
    {
        _potionCount++;
        PotionCountChanged(_potionCount);
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
        Leaved?.Invoke();
    }

    public int CalculateTotalDamage()
    {
        if (_currentWeapon != null)
            return BaseDamage + _currentWeapon.CalculateDamage();
        
        return baseDamage;
    }

    public void CalculateArmor()
    {
        if (_currentArmor != null)
            Armor = _currentArmor.CalculateProtection();

        Armor = 0;
    }
}