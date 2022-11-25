using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class Player : Fighter
{
    [SerializeField] private List<HealItem> _potions = new List<HealItem>();

    [SerializeField] private Transform _armorSlot;
    [SerializeField] private Transform _weaponSlot;

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

            _potions.RemoveAt(_potions.Count-1);

            Debug.Log($"Потрачена хилка. Теперь их {_potions.Count}");
        }
    }

    public bool PotionChecker()
    {
        bool isSomething = _potions.Count > 0;
        return isSomething;
    }

    public void AddHeal()
    {
        _potions.Add(new HealItem());
        Debug.Log($"Добавлена хилка. Теперь их {_potions.Count}");
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