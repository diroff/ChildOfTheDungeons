using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : ItemDescriptionSlot
{
    [SerializeField] private Transform _weaponHand;
    
    private Weapon _weapon;

    public Weapon Weapon => _weapon;

    public override void AddItem(Item item)
    {
        if (IsSomeWeapon())
            Destroy(_weaponHand.GetComponentInChildren<Weapon>().gameObject);

        base.AddItem(item);
        _weapon = item as Weapon;

        AddWeaponInHand();
    }

    public override void ShowDescription()
    {
        base.ShowDescription();
        if(SlotIsFilled())
            InfoPanel.SetInfo(_weapon.ItemDescription, _weapon.CalculateDamage(), _weapon.GetLevel(), false);
    }

    public bool IsSomeWeapon()
    {
        return IsFilled;
    }

    private void AddWeaponInHand()
    {
        Instantiate(_weapon, _weaponHand);
    }
}
