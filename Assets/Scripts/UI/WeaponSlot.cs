using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WeaponSlot : Slot
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

        _weapon = Instantiate(_weapon, _weaponHand);
        _weapon.HideUI();
    }

    public override void ShowDescription()
    {
        base.ShowDescription();
        if(SlotIsFilled())
            InfoPanel.SetInfo(_weapon.ItemDescription, _weapon.CalculateDamage(), _weapon.GetLevel());
    }

    public bool IsSomeWeapon()
    {
        return _weapon != null;
    }
}
