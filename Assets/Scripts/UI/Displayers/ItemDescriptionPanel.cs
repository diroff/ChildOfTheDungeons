using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemDescriptionPanel : MonoBehaviour
{
    [SerializeField] private Spawner _spawner;
    [Header("Text Fields")]
    [SerializeField] private TextMeshProUGUI _description;
    [SerializeField] private TextMeshProUGUI _value;
    [SerializeField] private TextMeshProUGUI _level;
    [Header("Sprites")]
    [SerializeField] private Image _valueSprite;
    [SerializeField] private Sprite _damageSprite;
    [SerializeField] private Sprite _protectionSprite;
    [Header("Item Values")]
    [SerializeField] private GameObject _valueObject;
    [SerializeField] private GameObject _levelObject;

    private Item item;

    private void OnEnable()
    {
        item = _spawner.GetItem();
        _description.text = item.ItemDescription;

        if (item.IsConsumable)
        {
            SetValuesState(false);
            return;
        }

        SetValuesState(true);

        _value.text = item.ItemValue.ToString();
        _level.text = item.GetLevel().ToString();

        if (item.GetType() == typeof(Armor))
            _valueSprite.sprite = _protectionSprite;
        else if (item.GetType() == typeof(Weapon))
            _valueSprite.sprite = _damageSprite;
    }

    private void SetValuesState(bool isEnable)
    {
        _valueObject.SetActive(isEnable);
        _levelObject.SetActive(isEnable);
    }
}
