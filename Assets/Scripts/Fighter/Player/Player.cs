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

    public event UnityAction Leaved;

    public void Heal()                         
    {                                          
        if (PotionChecker())
        {
            if (CurrentHealth >= MaxHealth / 2)
                CurrentHealth = MaxHealth;
            else
                CurrentHealth += MaxHealth / 2;

            _potionCount--;

            Debug.Log($"Потрачена хилка. Теперь их {_potionCount}");
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
        Debug.Log($"Добавлена хилка. Теперь их {_potionCount}");
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