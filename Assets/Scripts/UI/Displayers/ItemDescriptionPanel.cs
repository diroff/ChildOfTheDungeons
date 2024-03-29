using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Player _player;
    [SerializeField] private RoomController _roomController;

    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _value;

    [Header("Sprites")]
    [SerializeField] private Image _valueSprite;
    [SerializeField] private Sprite _damageSprite;
    [SerializeField] private Sprite _protectionSprite;

    [Header("Item Values")]
    [SerializeField] private GameObject _valueObject;

    private Item _item;
    private Spawner _spawner;

    private void OnEnable()
    {
        _spawner = _roomController.GetRoomSpawner();
        _item = _spawner.GetItem();
        _description.text = _item.ItemDescription;

        if (_item.IsConsumable)
        {
            SetValueState(false);
            return;
        }

        SetValueState(true);

        _value.text = _item.CalculateValue().ToString();

        if (_item.GetType() == typeof(Armor))
        {
            _value.text += "%";
            ArmorPreparing();
        }
        else if (_item.GetType() == typeof(Weapon))
            WeaponPreparing();
    }

    private void SetValueState(bool isEnable)
    {
        _valueObject.SetActive(isEnable);
    }

    private void WeaponPreparing()
    {
        _valueSprite.sprite = _damageSprite;

        if (!_player.WeaponSlot.IsItemFilled)
            return;

        Weapon weapon = _player.WeaponSlot.Weapon;

        if (_item.CalculateValue() > weapon.CalculateValue())
            _value.text += $" (+{_item.CalculateValue() - weapon.CalculateValue()})";

        else if(_item.ItemValue < weapon.ItemValue)
            _value.text += $" (-{weapon.CalculateValue() - _item.CalculateValue()})";
    }

    private void ArmorPreparing()
    {
        _valueSprite.sprite = _protectionSprite;

        Armor armor = _item.GetComponent<Armor>();

        if (armor.GetTypeArmor() == Armor.TypeArmor.helm)
            HelmPreparing();
        else if (armor.GetTypeArmor() == Armor.TypeArmor.costume)
            CostumePreparing();
        else if (armor.GetTypeArmor() == Armor.TypeArmor.shoes)
            ShoesPreparing();
    }

    private void HelmPreparing()
    {
        if (!_player.ArmorSlots.HelmSlot.IsItemFilled)
            return;

        ArmorComparison(_player.ArmorSlots.Helm);
    }

    private void CostumePreparing()
    {
        if (!_player.ArmorSlots.CostumeSlot.IsItemFilled)
            return;

        ArmorComparison(_player.ArmorSlots.Costume);
    }

    private void ShoesPreparing()
    {
        if (!_player.ArmorSlots.ShoesSlot.IsItemFilled)
            return;

        ArmorComparison(_player.ArmorSlots.Shoes);
    }

    private void ArmorComparison(Armor armor)
    {
        if (_item.CalculateValue() > armor.CalculateValue())
            _value.text += $" (+{_item.CalculateValue() - armor.CalculateValue()})";

        else if (_item.ItemValue < armor.ItemValue)
            _value.text += $" (-{armor.CalculateValue() - _item.CalculateValue()})";
    }
}