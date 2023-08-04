using UnityEngine;

public class ArmorSlots : MonoBehaviour
{
    [SerializeField] private ArmorSlot _helmSlot;
    [SerializeField] private ArmorSlot _costumeSlot;
    [SerializeField] private ArmorSlot _shoesSlot;

    private Armor _helm;
    private Armor _costume;
    private Armor _shoes;

    public ArmorSlot HelmSlot => _helmSlot;
    public ArmorSlot CostumeSlot => _costumeSlot;
    public ArmorSlot ShoesSlot => _shoesSlot;

    public Armor Helm => _helm;
    public Armor Costume => _costume;
    public Armor Shoes => _shoes;

    public float CalculateArmor()
    {
        float armor = 0;

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
        if (_helm != null)
            Destroy(_helm.gameObject);

        _helm = Instantiate(armor);
        _helmSlot.AddItem(_helm);
        _helm.HideItem();
    }

    private void AddCostume(Armor armor)
    {
        if(_costume != null)
            Destroy(_costume.gameObject);

        _costume = Instantiate(armor);
        _costumeSlot.AddItem(_costume);
        _costume.HideItem();
    }

    private void AddShoes(Armor armor)
    {
        if (_shoes != null)
            Destroy(_shoes.gameObject);

        _shoes = Instantiate(armor);
        _shoesSlot.AddItem(_shoes);
        _shoes.HideItem();
    }
}