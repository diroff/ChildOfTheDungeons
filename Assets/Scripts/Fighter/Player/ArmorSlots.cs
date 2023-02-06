using System.Collections.Generic;
using UnityEngine;

public class ArmorSlots : MonoBehaviour
{
    [SerializeField] private Slot _helmSlot;
    [SerializeField] private Slot _costumeSlot;
    [SerializeField] private Slot _shoesSlot;

    private Armor _helm;
    private Armor _costume;
    private Armor _shoes;

    public int CalculateArmor()
    {
        int armor = 0;

        if (_helm != null)
            armor += _helm.CalculateProtection();
        if (_costume != null)
            armor += _costume.CalculateProtection();
        if (_shoes != null)
            armor += _shoes.CalculateProtection();

        return armor;
    }

    public void AddItem(Armor armor)
    {
        if (armor.GetTypeArmor() == Armor.TypeArmor.costume)
            AddCostume(armor);
        if (armor.GetTypeArmor() == Armor.TypeArmor.helm)
            AddHelm(armor);
        if (armor.GetTypeArmor() == Armor.TypeArmor.shoes)
            AddShoes(armor);
    }

    private void AddHelm(Armor armor)
    {
        _helm = armor;
        _helmSlot.UpdateSprite(_helm.ItemSprite);
    }

    private void AddCostume(Armor armor)
    {
        _costume = armor;
        _costumeSlot.UpdateSprite(_costume.ItemSprite);
    }

    private void AddShoes(Armor armor)
    {
        _shoes = armor;
        _shoesSlot.UpdateSprite(_shoes.ItemSprite);
    }
}