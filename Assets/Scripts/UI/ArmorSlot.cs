using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArmorSlot : MonoBehaviour
{
    [SerializeField] private SpriteRenderer _itemSprite;
    [SerializeField] private InfoPanel _infoPanel;

    private Armor _armor;
    private bool _isFilled;

    private void UpdateSprite(Sprite sprite)
    {
        _itemSprite.sprite = sprite;
    }

    public void AddItem(Armor armor)
    {
        _armor = armor;
        _isFilled = true;
        _infoPanel.ShowInfo(false);
        UpdateSprite(_armor.ItemSprite);
    }

    public void ShowDescription()
    {
        if(!_isFilled)
            return;

        if(_infoPanel.gameObject.activeSelf)
            _infoPanel.gameObject.SetActive(false);

        _infoPanel.ShowInfo(true);
        _infoPanel.SetInfo(_armor.ItemDescription, _armor.CalculateProtection(), _armor.GetLevel());
    }
}