using System.Collections.Generic;
using UnityEngine;

public class ArmorSlots : MonoBehaviour
{
    [SerializeField] private ArmorSlot _helmSlot;
    [SerializeField] private ArmorSlot _costumeSlot;
    [SerializeField] private ArmorSlot _shoesSlot;

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
        _helmSlot.AddItem(_helm);
    }

    private void AddCostume(Armor armor)
    {
        _costume = armor;
        _costumeSlot.AddItem(_costume);
    }

    private void AddShoes(Armor armor)
    {
        _shoes = armor;
        _shoesSlot.AddItem(_shoes);
    }
}